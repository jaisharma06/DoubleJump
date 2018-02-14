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

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
		EventManager.OnNoEnemyLeft += OnNoEnemyLeft;
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
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

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
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
    }

	private void OnNoEnemyLeft()
	{
		gameplayUI.SetActive(false);
		startGameUI.SetActive(true);
	}
}
