using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject celebrationVFX;
    public int jumpScore = 10;
    public int doubleJumpScore = 20;
    public float scoreDelayTime = 1f;
    public string TOTAL_SCORE_KEY = "TotalScore"; 
    public int score { get; set; }
    public int totalScore { get; set; }
    public GPGManager gpgManager;

    public int celebrationMultiplier = 100;
    private int celebrationCount = 1;

    private bool celebrate = false;


    private void OnEnable()
    {
        EventManager.OnJump += OnJump;
        EventManager.OnDoubleJump += OnDoubleJump;
        EventManager.OnGameStart += OnGameStart;
        EventManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnJump -= OnJump;
        EventManager.OnDoubleJump -= OnDoubleJump;
        EventManager.OnGameStart -= OnGameStart;
        EventManager.OnGameOver -= OnGameOver;
    }

    public void Add(int deltaScore)
    {
        if (celebrationMultiplier * celebrationCount > score && celebrationMultiplier * celebrationCount <= score + deltaScore)
        {
            celebrationCount++;
            celebrate = true;
        }
        score += deltaScore;

        Invoke("AddScoreAfterDelay", scoreDelayTime);
    }

    public void Deduct(int deltaScore)
    {
        score -= deltaScore;
        scoreText.text = "" + score;
    }

    private void OnGameStart()
    {
        score = 0;
        scoreText.text = "" + score;
        celebrationCount = 1;
    }

    private void OnJump()
    {
        Add(jumpScore);
    }

    private void OnDoubleJump()
    {
        Add(doubleJumpScore);
    }

    private void AddScoreAfterDelay()
    {
        scoreText.text = "" + score;

        if (celebrate)
        {
            if (celebrationVFX)
            {
                celebrationVFX.SetActive(false);
                celebrationVFX.SetActive(true);
            }

            celebrate = false;
        }
    }

    private void OnGameOver()
    {
        totalScore = PlayerPrefs.GetInt(TOTAL_SCORE_KEY, 0);
        totalScore += score;
        PlayerPrefs.SetInt(TOTAL_SCORE_KEY, totalScore);

        gpgManager.UpdateUserScore(score);
    }
}
