using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class rewarAdMManager : MonoBehaviour
{
    private RewardedAd rewardAD;
    public string rewardAdNumber;
    string rewardAdID;

    private void Start()
    {
        requestRewardAd();
    }

    void requestRewardAd()
    {
        #if UNITY_ANDROID
                rewardAdID = rewardAdNumber;
        #else
                    adID = "Unkown Platform";
        #endif

        rewardAD = new RewardedAd(rewardAdID);

        rewardAD.OnAdLoaded += isloaded;
        rewardAD.OnAdFailedToLoad += wrongisloaded;
        rewardAD.OnAdOpening += open;
        rewardAD.OnAdFailedToShow += isopen;
        rewardAD.OnUserEarnedReward += earnedReward;
        rewardAD.OnAdClosed += close;

        AdRequest request = new AdRequest.Builder().Build();
        rewardAD.LoadAd(request);
    }

    public void rewardAdShow()
    {
        if (rewardAD.IsLoaded())
        {
            rewardAD.Show();
        }
        else
        {
            requestRewardAd();
        }
    }

    public void isloaded(object sender, EventArgs args)
    {
        Debug.Log("Reklam yüklendi\n");
    }
    public void wrongisloaded(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Reklam yüklenemedi\n");
        requestRewardAd();
    }
    public void open(object sender, EventArgs args)
    {
        Debug.Log("Reklam Açýldý\n");
    }
    public void close(object sender, EventArgs args)
    {
        Debug.Log("Reklam kapatýldý\n");
    }
    public void isopen(object sender , AdErrorEventArgs args)
    {
        Debug.Log("Reklam açýlamadý\n");
    }
    public void earnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        PlayerPrefs.SetInt("totalcoin", PlayerPrefs.GetInt("totalcoin", 0) + (int)(amount));
        Debug.Log("ödül adý" + type + " miktar : " + amount + "\n");
    }

}
