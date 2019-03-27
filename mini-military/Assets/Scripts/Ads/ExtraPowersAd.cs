﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class ExtraPowersAd : MonoBehaviour
{
	public string placementId = "rewardedVideo";
    private string gameId = "1234567";	
	bool testMode = true;
	
    //#if UNITY_IOS
    //   private string gameId = "1234567";
    //#elif UNITY_ANDROID
    //    private string gameId = "7654321";
    //#endif

	
    // Start is called before the first frame update
    void Start()
    {
		Advertisement.Initialize (gameId, testMode);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ExtraZoom(){
		if (Advertisement.IsReady("rewardedVideo"))
		{
		  ShowOptions options = new ShowOptions();
		  options.resultCallback = HandleShowResultZoom;
		  Advertisement.Show("rewardedVideo", options);
		}
		
	}
	
	public void ExtraBullet(){
		if (Advertisement.IsReady("rewardedVideo"))
		{
		  ShowOptions options = new ShowOptions();
		  options.resultCallback  = HandleShowResultBullet;
		  Advertisement.Show("rewardedVideo", options);
		}		
	}
	
	public void ExtraHealth(){
		if (Advertisement.IsReady("rewardedVideo"))
		{
		  ShowOptions options = new ShowOptions();
		  options.resultCallback = HandleShowResultHealth;
		  Advertisement.Show("rewardedVideo", options);
		}			
	}
	
	public void ExtraFirstAid(){
		if (Advertisement.IsReady("rewardedVideo"))
		{
		 ShowOptions options = new ShowOptions();
		  options.resultCallback  = HandleShowResultFirstAid;
		  Advertisement.Show("rewardedVideo", options);
		}		
	}
	
	public void ExtraBomb(){
		if (Advertisement.IsReady("rewardedVideo"))
		{
		  ShowOptions options = new ShowOptions();
		  options.resultCallback = HandleShowResultBomb;
		  Advertisement.Show("rewardedVideo", options);
		}			
	}
	
	public void Back(){
		SceneManager.LoadScene("StartScene");
	}
	
	private void HandleShowResultZoom(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			PlayerPrefs.SetInt("ExtraZoom", 1);
			break;
		  case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		  case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	
	private void HandleShowResultBullet(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			PlayerPrefs.SetInt("ExtraBullet", 1);
			break;
		  case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		  case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	
	private void HandleShowResultHealth(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			PlayerPrefs.SetInt("ExtraHealth", 1);
			break;
		  case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		  case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	
	private void HandleShowResultFirstAid(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			PlayerPrefs.SetInt("ExtraFirstAid", 1);
			break;
		  case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		  case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	
	private void HandleShowResultBomb(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			PlayerPrefs.SetInt("ExtraBomb1", 1);
			break;
		  case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		  case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}
