using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent(typeof(Button))]
public class UnityAdsButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    public Button confirmButton;
    private Button adButton;
    //private string gameId = "1234567";
	
	private bool blink = false;
	private int counter = 0;
	private int blinkSpeed = 10;
	
	Animator anim;

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
        adButton = GetComponent<Button>();
		anim = GetComponent<Animator>();
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
		
		if(counter > blinkSpeed)
		 {
			 blink = !blink;
			 counter = 0;
		 }
		 
		 counter++;
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
		confirmButton.enabled = true;
        
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
	
	void OnGUI()
	{
		  if(blink)
			 //GetComponent<Image>().color = Color.red;
			anim.SetBool("fadeIn", true);
		  else 
			anim.SetBool("fadeIn", false);
			 //GetComponent<Image>().color = Color.green;
	}
}
