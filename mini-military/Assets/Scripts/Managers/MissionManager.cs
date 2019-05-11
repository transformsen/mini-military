using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
	public Toggle mission1Toggle;
	public Toggle mission2Toggle;
	public Toggle mission3Toggle;
	public Toggle mission4Toggle;
	public Toggle mission5Toggle;
	public GameObject loadingScreen;
	public Slider loadingSlider;
	
    void Start(){
		PlayerPrefs.SetInt("MissionSeen", 1);
	}
	
    // Update is called once per frame
    void Update()
    {
		string exPowerConstName = ExtraPowersAd.exPowerConstName;
		
		if((PlayerPrefs.GetInt(exPowerConstName+"Bomb") == 1 )&&
			(PlayerPrefs.GetInt(exPowerConstName+"FirstAid") == 1) &&
			(PlayerPrefs.GetInt(exPowerConstName+"Health") == 1) &&
			(PlayerPrefs.GetInt(exPowerConstName+"Bullets") == 1) &&
			(PlayerPrefs.GetInt(exPowerConstName+"Zoom") == 1)){
			
			mission1Toggle.isOn = true;			
		}
		
		if(PlayerPrefs.GetInt(ExtraPowersAd.weapomConstName+"SX95") == 1 &&
			PlayerPrefs.GetInt(ExtraPowersAd.weapomConstName+"SM4A1") == 1 &&
			PlayerPrefs.GetInt(ExtraPowersAd.weapomConstName+"SAWP") == 1){
				mission2Toggle.isOn = true;	
		}
        
		
		
		if(PlayerPrefs.GetInt("Mission3") == 1){
			mission3Toggle.isOn = true;
		}
		if(PlayerPrefs.GetInt("SelectedAvatar") == 4){
			mission4Toggle.isOn = true;
		}
		if(PlayerPrefs.GetInt("Mission5") == 1){
			mission5Toggle.isOn = true;
		}		
    }
	
	public void Booster()
    {
        PlayerPrefs.SetString("BoostMode", "EX");
		StartCoroutine(Load("PurchaseScene"));
    }
	
	public void Gun()
    {
       PlayerPrefs.SetString("BoostMode", "CO");
		StartCoroutine(Load("PurchaseScene"));
    }
	
	public void Avatar()
    {
        StartCoroutine(Load("AvatarSelectionScene"));
    }
	
	
	
	public void Play()
    {
        PlayerPrefs.SetString("GameType", "SL");
		StartCoroutine(Load("GameScene"));
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
