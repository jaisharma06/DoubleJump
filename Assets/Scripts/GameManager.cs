using System.Collections;
using System.Collections.Generic;
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
	private GameObject startGameUI;

    public bool isGameOver = true;
    public bool detectTouch = true;

    void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
		EventManager.OnNoEnemyLeft += OnNoEnemyLeft;
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
    }

    public void StartGame()
    {
        score = 0;
        isGameOver = false;
        scoreText.text = score.ToString();
        EventManager.CallOnGameStart();
		gameplayUI.SetActive(true);
		startGameUI.SetActive(false);
    }

    private void OnGameOver()
    {
        isGameOver = true;
        detectTouch = false;
    }

	private void OnNoEnemyLeft()
	{
		gameplayUI.SetActive(false);
        detectTouch = true;
		startGameUI.SetActive(true);
	}
}
