﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent(typeof(Button))]
public class UnityAdsButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    public Button confirmButton;
    private Button adButton;
    private string gameId = "1234567";

    //#if UNITY_IOS
    //   private string gameId = "1234567";
    //#elif UNITY_ANDROID
    //    private string gameId = "7654321";
    //#endif

    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
        confirmButton.enabled = true;
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        Debug.Log("Results show " + result.ToString());
        
        if (result == ShowResult.Finished)
        {
            confirmButton.enabled = true;
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
