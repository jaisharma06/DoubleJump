using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI highestScore;
    public TextMeshProUGUI currentScore;

    public ScoreManager scoreManager;

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        currentScore.text = "" + scoreManager.score;
        highestScore.text = "" + scoreManager.totalScore;
    }
}
