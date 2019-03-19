using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PurchaseBannerAds : MonoBehaviour
{
    public string bannerPlacement = "banner";
    public bool testMode = true;

#if UNITY_IOS
    public const string gameID = "1234567";
#elif UNITY_ANDROID
    public const string gameID = "1234568";
#elif UNITY_EDITOR
    public const string gameID = "1111111";
#endif

    void Start () {
        Advertisement.Initialize (gameID, testMode);
        StartCoroutine (ShowBannerWhenReady ());
    }

    IEnumerator ShowBannerWhenReady () {
        while (!Advertisement.IsReady ("banner")) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Banner.Show (bannerPlacement);
    }
}
