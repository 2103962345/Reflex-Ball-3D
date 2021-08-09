using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AppodealAdsManager : MonoBehaviour, IRewardedVideoAdListener
{
    string appKey = "2b6090ec6337d24ff5acfdb4aa51ae7f00f85e9461604ef2";
    int adsCounter;

    void Start()
    {
        adsCounter = 0;
        Appodeal.disableLocationPermissionCheck();
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL);
        Appodeal.initialize(appKey, Appodeal.BANNER_BOTTOM);
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO);
        Appodeal.setRewardedVideoCallbacks(this);

        if (Appodeal.isLoaded(Appodeal.BANNER_BOTTOM))
        {
            Appodeal.show(Appodeal.BANNER_BOTTOM);
            Debug.Log("çalýþtý banner reklam");
        }
    }
    public void adsShow()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && adsCounter %2 == 0)
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
            adsCounter++;
            Debug.Log("çalýþtý");
        }
       
    }
    public void RewardedVideoShow()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            onRewardedVideoFinished(100, "coin");
        }
    }

    public void onRewardedVideoLoaded(bool precache)
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoFailedToLoad()
    {
        Appodeal.initialize(appKey, Appodeal.REWARDED_VIDEO);
    }

    public void onRewardedVideoShowFailed()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
    }

    public void onRewardedVideoShown()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        PlayerPrefs.SetInt("totalcoin", PlayerPrefs.GetInt("totalcoin", 0) + (int)(amount));
    }

    public void onRewardedVideoClosed(bool finished)
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoExpired()
    {
        throw new System.NotImplementedException();
    }

    public void onRewardedVideoClicked()
    {
        throw new System.NotImplementedException();
    }
}
