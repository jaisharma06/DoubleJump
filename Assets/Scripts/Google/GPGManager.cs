using GooglePlayGames;
using UnityEngine.SocialPlatforms;
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
    }

    private void OnLoginFailure()
    {
        text.text="failure";
    }
}
