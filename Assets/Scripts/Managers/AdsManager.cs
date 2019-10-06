using UnityEngine;
using UnityEngine.Advertisements;
//using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
public class AdsManager : MonoBehaviour
{
    public bool testMode = false;
    public InputHandler inputHandler;
    public int skipChances = 3;
    private string gameId = "3316022";
    private string rewardAdsId = "video";
    private string videoAdId = "video";

    //private Button button;

    private int currentChanceCount = 0;


    private void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver;
    }

    private void Start()
    {
        //button = GetComponent<Button>();

        //button.interactable = Advertisement.IsReady(rewardAdsId);

        //if (button)
        //{
           //button.onClick.AddListener(ShowRewardAds);
        //}
        Advertisement.Initialize (gameId, testMode);
    }

    private void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver;
    }

    private void ShowRewardAds()
    {
        if (Advertisement.IsReady(rewardAdsId))
        {
            Advertisement.Show(rewardAdsId);
        }
    }

    private void ShowVideoAds()
    {
        if (Advertisement.IsReady(videoAdId))
        {
            Advertisement.Show(videoAdId);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (rewardAdsId == placementId)
        {
            // Advertisement.Show(rewardAdsId);
        }
        else if (placementId == videoAdId)
        {
            // Advertisement.Show(videoAdId);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        inputHandler.isInputActive = false;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {

        }
        else if (showResult == ShowResult.Skipped)
        {

        }
        else if (showResult == ShowResult.Failed)
        {

        }

        inputHandler.isInputActive = true;
    }

    private void OnGameOver()
    {
        currentChanceCount++;
        if (skipChances == currentChanceCount)
        {
            ShowVideoAds();
            currentChanceCount = 0;
        }
    }
}