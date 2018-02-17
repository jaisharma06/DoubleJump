using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject gameplayUI;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject pausedText;
    [SerializeField]
    private GameObject startGameUI;
    [SerializeField]
    private SpriteRenderer ground;

    [HideInInspector]
    public bool isGameOver = true;
    [HideInInspector]
    public bool isGamePaused = false;
    [HideInInspector]
    public bool detectTouch = true;

    private Camera mainCamera;

    private AudioManager am;

    void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
        EventManager.OnNoEnemyLeft += OnNoEnemyLeft;
        EventManager.OnInvertColor += InvertColor;

        mainCamera = Camera.main;
        am = AudioManager.instance;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isGameOver)
        {
            return;
        }
        var enemy = other.GetComponent<Blade>();
        if (enemy)
        {
            score++;
            scoreText.text = score.ToString();
        }
    }

    void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver;
        EventManager.OnNoEnemyLeft -= OnNoEnemyLeft;
        EventManager.OnInvertColor -= InvertColor;
    }

    public void StartGame()
    {
        score = 0;
        isGameOver = false;
        scoreText.text = score.ToString();
        EventManager.CallOnGameStart();
        gameplayUI.SetActive(true);
        startGameUI.SetActive(false);
        am.PlayBackgroundMusic(true);
    }

    private void OnGameOver()
    {
        isGameOver = true;
        detectTouch = false;
        am.PlayBackgroundMusic(false);
    }

    private void OnNoEnemyLeft()
    {
        gameplayUI.SetActive(false);
        detectTouch = true;
        startGameUI.SetActive(true);
    }

    private void InvertColor()
    {
        if (mainCamera.backgroundColor == Color.black)
        {
            mainCamera.backgroundColor = Color.white;
        }
        else
        {
            mainCamera.backgroundColor = Color.black;
        }

        ground.material.color = (ground.material.color == Color.black) ? Color.white : Color.black;
    }

    public void OnGamePaused(bool flag)
    {
        if (isGameOver)
            return;
        isGamePaused = flag;
        pauseButton.SetActive(!flag);
        pausedText.SetActive(flag);
        Time.timeScale = (flag) ? 0 : 1;
    }
}
