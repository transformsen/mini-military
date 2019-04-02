using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PurchaseBannerAds : MonoBehaviour
{
    public string bannerPlacement = "banner";
    public bool testMode = true;

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


    void Start () {
        Advertisement.Initialize (gameId, testMode);
        StartCoroutine (ShowBannerWhenReady ());
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady ("banner")) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.Show (bannerPlacement);
    }

    public static void HideBanner(){
        Advertisement.Banner.Hide (false);
    }
}
