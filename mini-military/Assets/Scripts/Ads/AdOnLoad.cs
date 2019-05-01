using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdOnLoad : MonoBehaviour
{
    public string placementId = "rewardedVideo";
  //private string gameId = "1234567";	
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


	
    // Start is called before the first frame update
    void Start()
    {
		
		Advertisement.Initialize (gameId, testMode);
		if (Advertisement.IsReady("rewardedVideo"))
		{
			
		}
        
    }

}
