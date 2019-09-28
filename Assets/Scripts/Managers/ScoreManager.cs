using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int jumpScore = 10;
    public int doubleJumpScore = 20;
    public int score { get; set; }

    private void OnEnable()
    {
        EventManager.OnJump += OnJump;
        EventManager.OnDoubleJump += OnDoubleJump;
        EventManager.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        EventManager.OnJump -= OnJump;
        EventManager.OnDoubleJump -= OnDoubleJump;
        EventManager.OnGameStart -= OnGameStart;
    }

    public void Add(int deltaScore)
    {
        score += deltaScore;
        scoreText.text = "" + score;
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

    }

    private void OnJump()
    {
        Add(jumpScore);
    }

    private void OnDoubleJump()
    {
        Add(doubleJumpScore);
    }
}
