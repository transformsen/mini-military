using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class UnityAdsButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
	public AvatarSelection avatarSelection;
   
    //private string gameId = "1234567";
	
	private bool blink = false;
	private int counter = 0;
	private int blinkSpeed = 10;
    public bool testMode = false;
	
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
        
    }

    public void ShowAd()
    {
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
			avatarSelection.Confirm();
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
