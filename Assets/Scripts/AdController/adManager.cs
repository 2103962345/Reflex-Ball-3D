using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class adManager : MonoBehaviour
{
    private InterstitialAd ads;
    public string AndroidAdsVideo;
    string adID;

    // Start is called before the first frame update
    void Start()
    {
        requestAds();
    }

    void requestAds()
    {
#if UNITY_ANDROID
            adID = AndroidAdsVideo;
#else
            adID = "Unkown Platform";
#endif

        ads = new InterstitialAd(adID);

        ads.OnAdLoaded += isloaded;
        ads.OnAdFailedToLoad += wrongisloaded;
        ads.OnAdOpening += open;
        ads.OnAdClosed += close;
       
        AdRequest request = new AdRequest.Builder().Build();
        ads.LoadAd(request);

    }

    public void adShow()
    {
        if (ads.IsLoaded())
        {
            ads.Show();
        }
        else
        {
            requestAds();
        }
    }

    public void isloaded(object sender, EventArgs args)
    {

        Debug.Log("Reklam yüklendi\n");

    }
    public void wrongisloaded(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Reklam yüklenemedi\n");
        requestAds();

    }
    public void open(object sender, EventArgs args)
    {
        Debug.Log("Reklam Açýldý\n");

    }
    public void close(object sender, EventArgs args)
    {
        Debug.Log("Reklam kapatýldý\n");
        requestAds();

    }

}
