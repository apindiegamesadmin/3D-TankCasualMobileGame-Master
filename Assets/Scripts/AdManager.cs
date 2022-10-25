using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;


public class AdManager : MonoBehaviour
{
    [Header("Banner Ads")]
    [SerializeField] UnityEvent OnLoadedBannerAd;
    [SerializeField] UnityEvent OnFailedToLoadBannerAd;
    [SerializeField] UnityEvent OnOpenBannerAd;
    [SerializeField] UnityEvent OnCloseBannerAd;

    [Header("Interstitial Ads")]
    [SerializeField] UnityEvent OnLoadedInAd;
    [SerializeField] UnityEvent OnFailedToLoadInAd;
    [SerializeField] UnityEvent OnShowInAd;
    [SerializeField] UnityEvent OnFailedToShowInAd;
    [SerializeField] UnityEvent OnCloseInAd;

    [Header("Reward Ads")]
    [SerializeField] UnityEvent OnLoadedRewardAd;
    [SerializeField] UnityEvent OnFailedToLoadRewardAd;
    [SerializeField] UnityEvent OnOpenRewardAd;
    [SerializeField] UnityEvent OnFailedToShowRewardAd;
    [SerializeField] UnityEvent OnEarnedReward;
    [SerializeField] UnityEvent OnCloseRewardAd;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    public static AdManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        RequestInterstitial();
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#else
            string adUnitId = "unexpected_platform";
#endif

        BannerDestory();

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnLoadedBannerAd));
        };
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnFailedToLoadBannerAd));
        };
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnOpenBannerAd));
        };
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnCloseBannerAd));
        };


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }
    public void BannerDestory()
    {
        if(bannerView != null)
        {
            bannerView.Destroy();
        }
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#else
        string adUnitId = "unexpected_platform";
#endif
        InterstitialDestroy();

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        /*// Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnLoadedInAd));
        };
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnFailedToLoadInAd));
        };
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnOpenInAd));
        };*/
        // Called when an ad is failed to shown.
        this.interstitial.OnAdFailedToShow += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnFailedToShowInAd));
        };
         // Called when the ad is closed.
         this.interstitial.OnAdClosed += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnCloseInAd));
        };


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }
    public void ShowInterstitial()
    {
        StartCoroutine(DelayShowInterstial());
    }
    public void InterstitialDestroy()
    {
        if(interstitial != null)
        {
            this.interstitial.Destroy();
        }
    }

    public void RequestRewardedAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
            adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnLoadedRewardAd));
        };
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnFailedToLoadRewardAd));
        };
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnOpenRewardAd));
        };
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnFailedToShowRewardAd));
        };
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnEarnedReward));
        };
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += (sender, args) =>
        {
            StartCoroutine(DelayBeforeEvent(OnCloseRewardAd));
        };

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }
    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    IEnumerator DelayBeforeEvent(UnityEvent eventName)
    {
        yield return new WaitForEndOfFrame();
        eventName?.Invoke();
    }

    IEnumerator DelayShowInterstial()
    {
        yield return new WaitForSeconds(0.3f);
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
}
