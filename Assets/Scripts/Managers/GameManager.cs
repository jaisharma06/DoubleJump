using System;
using TMPro;
using UnityEngine;

[Serializable]
public enum GameState
{
    START = 0,
    GAME = 1,
    PAUSED = 2,
    OVER = 3
}

public class GameManager : MonoBehaviour
{
    //public int score = 0;
    //[SerializeField]
    //private TextMeshProUGUI scoreText;
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
    [SerializeField]
    private GameObject endGameUI;

    [HideInInspector]
    public bool isGameOver = true;
    [HideInInspector]
    public bool isGamePaused = false;
    [HideInInspector]
    public bool detectTouch = true;

    public GameState state;

    private Camera mainCamera;

    private AudioManager am;

    void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
        EventManager.OnNoEnemyLeft += OnNoEnemyLeft;
        EventManager.OnInvertColor += InvertColor;

        mainCamera = Camera.main;
        am = AudioManager.instance;

        state = GameState.START;
    }

    void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver;
        EventManager.OnNoEnemyLeft -= OnNoEnemyLeft;
        EventManager.OnInvertColor -= InvertColor;
    }

    public void StartGame()
    {
        //score = 0;
        isGameOver = false;
        //scoreText.text = score.ToString();
        state = GameState.GAME;
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
        state = GameState.OVER;
    }

    private void OnNoEnemyLeft()
    {
        gameplayUI.SetActive(false);
        detectTouch = true;
        //startGameUI.SetActive(true);
        endGameUI.SetActive(true);
    }

    public void ShowStartGameUI()
    {
        startGameUI.SetActive(true);
        endGameUI.SetActive(false);
        state = GameState.START;
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
        state = (flag) ? GameState.PAUSED : GameState.START;
        pauseButton.SetActive(!flag);
        pausedText.SetActive(flag);
        Time.timeScale = (flag) ? 0 : 1;
    }
}
