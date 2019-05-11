using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ExtraPowersAd : MonoBehaviour
{
	public string placementId = "rewardedVideo";
  //private string gameId = "1234567";	
	public bool testMode = false;
	
	public GameObject loadingScreen;
	public GameObject coinScreen;
	public GameObject extraPowerScreen;
	public GameObject warningScreen;
	public Text coinsLeft;
	
    public Slider loadingSlider;
	public static string weapomConstName = "CollectToo";
	public static string exPowerConstName = "Exp";
	
    #if UNITY_IOS
      private string gameId = "3102447";
    #elif UNITY_ANDROID
       private string gameId = "3102446";
    #elif UNITY_EDITOR_WIN
       private string gameId = "1234567";
    #elif UNITY_STANDALONE_WIN
       private string gameId = "1234567";
    #else
       private string gameId = "1234567";
    #endif


	
    // Start is called before the first frame update
    void Start()
    {
		loadingScreen.SetActive(false);
		warningScreen.SetActive(false);
		Advertisement.Initialize (gameId, testMode);
		string boostMode = PlayerPrefs.GetString("BoostMode");        
		if("CO".Equals(boostMode)){
			coinScreen.SetActive(true);
			extraPowerScreen.SetActive(false);
		}else{
			coinScreen.SetActive(false);
			extraPowerScreen.SetActive(true);
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        SoldOut("X95");
		SoldOut("M4A1");
		SoldOut("AWP");
		
		Unlocked("Zoom");
		Unlocked("Bullets");
		Unlocked("Health");
		Unlocked("FirstAid");
		Unlocked("Bomb");
		
		int coins = PlayerPrefs.GetInt("Coins");
		coinsLeft.text = "coins: "+coins;
		
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
	
	public void buyX95(){
		ReduceCoin(5, "SX95");
	}
	
	public void buyM4A1(){
		ReduceCoin(5, "SM4A1");
	}
	
	public void buyAWP(){
		ReduceCoin(5, "SAWP");
	}
	
	private void ReduceCoin(int coinsPay, string weapon){
		int coins = PlayerPrefs.GetInt("Coins");
		Debug.Log("coins="+coins+" coinsPay="+coinsPay);
		if(coins < coinsPay){
			warningScreen.SetActive(true);
		}else{
			coins = coins - coinsPay;
			PlayerPrefs.SetInt("Coins", coins);
			PlayerPrefs.SetInt(weapomConstName+weapon, 1);			
		}
	}
	
	private void SoldOut(string weapon){
		bool isSold = false;
		if(PlayerPrefs.GetInt(weapomConstName+"S"+weapon) == 1){
			isSold = true;
		}
		coinScreen.transform.Find(weapon).transform.Find("Sold").gameObject.SetActive(isSold);
		coinScreen.transform.Find(weapon).transform.Find("Button").gameObject.SetActive(!isSold);
	}
	
	private void Unlocked(string powerName){
		bool isUnloakced = false;
		if(PlayerPrefs.GetInt(exPowerConstName+powerName) == 1){
			isUnloakced = true;
		}
		extraPowerScreen.transform.Find(powerName).transform.Find("Unlocked").gameObject.SetActive(isUnloakced);
		extraPowerScreen.transform.Find(powerName).transform.Find("Button").gameObject.SetActive(!isUnloakced);
	}
	
	public void Back(){
		StartCoroutine(Load("StartScene"));
		PurchaseBannerAds.HideBanner();
	}
	
	private void HandleShowResultZoom(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			PlayerPrefs.SetInt(exPowerConstName+"Zoom", 1);
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
			PlayerPrefs.SetInt(exPowerConstName+"Bullets", 1);
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
			PlayerPrefs.SetInt(exPowerConstName+"Health", 1);
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
			PlayerPrefs.SetInt(exPowerConstName+"FirstAid", 1);
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
			PlayerPrefs.SetInt(exPowerConstName+"Bomb", 1);
			break;
		  case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		  case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
	
	private IEnumerator Load(string senceName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(senceName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {           
            loadingSlider.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingSlider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
