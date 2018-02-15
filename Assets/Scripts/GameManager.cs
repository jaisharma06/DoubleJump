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
    [SerializeField]
    private SpriteRenderer ground;

    [HideInInspector]
    public bool isGameOver = true;
    [HideInInspector]
    public bool detectTouch = true;

    private Camera mainCamera;

    void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
		EventManager.OnNoEnemyLeft += OnNoEnemyLeft;
        EventManager.OnInvertColor += InvertColor;

        mainCamera = Camera.main;
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
}
