using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GPGManager : MonoBehaviour
{
#if UNITY_ANDROID
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

        PlayGamesPlatform.Instance.SetDefaultLeaderboardForUI(GPGSIds.leaderboard_top_score);
    }

    private void OnLoginFailure()
    {
        text.text="failure";
    }

    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {

        }
    }

    public void UpdateUserScore(int score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_top_score, OnLeaderboardUpdate);
    }

    public void OnLeaderboardUpdate(bool success)
    {

    }
#endif
}
