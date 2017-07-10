using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class AdmobManager : MonoBehaviour {
    private const string AD_UNIT_ID = "pub-3605548999061027";
    private const string INTERSTITIAL_ID = "ca-app-pub-3605548999061027/8207600197";
    bool rewardBasedEventHandlersSet = false;
    RewardBasedVideoAd rewardBasedVideo;
    public string initialVideoRequest;
    int coinsc2;


    void Start() {
        if (ProtectedPrefs.HasKey("Coins"))
        {
            coinsc2 = ProtectedPrefs.GetInt("Coins");
        }
        else
        {
            coinsc2 = 0;
        }
        RequestRewardBasedVideo(initialVideoRequest);
        if (!rewardBasedEventHandlersSet)
        { // Reward based video instance is a singleton. Register handlers once to
          // avoid duplicate events.
          // Ad event fired when the rewarded video ad
          // has been received.
          /*  rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // has failed to load.
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // is opened.
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            // has started playing.
            rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;*/
          // has rewarded the user.
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // is closed.
            /*  rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
              // is leaving the application.
              rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;*/

            rewardBasedEventHandlersSet = true;
        }
    }

 

    private void OnDisable()
    {
        if (rewardBasedEventHandlersSet)
        {
            rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
            rewardBasedEventHandlersSet = false;
        }
    }


    public void RequestRewardBasedVideo(string adUnitID)
    {
        /*
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "pub-3605548999061027";
#elif UNITY_IPHONE
        string adUnitId = "pub-3605548999061027";
#else
        string adUnitId = "unexpected_platform";
#endif*/

        rewardBasedVideo = RewardBasedVideoAd.Instance;

        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitID);
    }

    public void ShowVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        int convertedAmount = System.Convert.ToInt32(amount);
        if (type == "Coins")
        {
            if (ProtectedPrefs.HasKey("Coins"))
            {
                ProtectedPrefs.SetInt("Coins", coinsc2 + convertedAmount);
            }
            else
            {
                coinsc2 = convertedAmount;
                ProtectedPrefs.SetInt("Coins", convertedAmount);
            }
        }
        ProtectedPrefs.Save();


        print("User rewarded with: " + amount.ToString() + " " + type);
    }

   
}
