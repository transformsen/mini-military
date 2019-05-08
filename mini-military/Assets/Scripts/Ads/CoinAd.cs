using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class CoinAd : MonoBehaviour
{
    public string placementId = "rewardedVideo";
    
	public int freeCoins = 10;
    public bool testMode = false;
	public Button freeCoinButton;
	public GameObject warningScreen;
	public Text coinsLeft;
	
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

    void Start()
    {
        
		Advertisement.Initialize (gameId, testMode);
		
    }

    void Update()
    {
        int LastCoinsCollected = PlayerPrefs.GetInt("LastCoinsCollected");
		if(System.DateTime.Now.Day != LastCoinsCollected){
			freeCoinButton.gameObject.SetActive(true);
		}else{
			freeCoinButton.gameObject.SetActive(true);
		}
		int coins = PlayerPrefs.GetInt("Coins");
		coinsLeft.text = "coins: "+coins;
    }
	
	public void Close(){
		warningScreen.gameObject.SetActive(false);
	}

    public void ShowAd()
    {
		int collectedDay = System.DateTime.Now.Day;
		PlayerPrefs.SetInt("LastCoinsCollected", collectedDay);
       if (Advertisement.IsReady("rewardedVideo"))
		{
		  ShowOptions options = new ShowOptions();
		  options.resultCallback = HandleShowResult;
		  Advertisement.Show("rewardedVideo", options);
		}
    }

    
    private void HandleShowResult(ShowResult result){
		switch (result){
		  case ShowResult.Finished:
			int coins = PlayerPrefs.GetInt("Coins");
			if(coins < 0){
				coins = 0;
			}			
			coins = coins + freeCoins;
			PlayerPrefs.SetInt("Coins", coins);
			break;
		  case ShowResult.Skipped:
			Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
			break;
		  case ShowResult.Failed:
			Debug.LogError("Video failed to show");
			break;
		}
	}
}
