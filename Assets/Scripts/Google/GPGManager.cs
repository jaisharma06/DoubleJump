using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GPGManager : MonoBehaviour
{
    public Text text;

    // Use this for initialization
    void Start()
    {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();


        text.text = "starting GPG";
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                OnLoginSuccess();
            }
            else
            {
                OnLoginFailure();
            }
        });
    }

    private void OnLoginSuccess()
    {
        text.text="success";

        PlayGamesPlatform.Instance.SetDefaultLeaderboardForUI(GPGSIds.leaderboard_single_top_score);
    }

    private void OnLoginFailure()
    {
        text.text="failure";
    }

    public void ShowLeaderboards()
    {
        text.text="showing leadersboard";
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_single_top_score);
        }
        else
        {

        }
    }

    public void UpdateUserScore(int score)
    {
        text.text="pushing latest score";
        Social.ReportScore(score, GPGSIds.leaderboard_single_top_score, OnLeaderboardUpdate);
    }

    public void OnLeaderboardUpdate(bool success)
    {
text.text="latest score pushed";
    }
}
