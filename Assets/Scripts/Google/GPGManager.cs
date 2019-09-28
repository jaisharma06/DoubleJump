using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class GPGManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
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

    }

    private void OnLoginFailure()
    {

    }
}
