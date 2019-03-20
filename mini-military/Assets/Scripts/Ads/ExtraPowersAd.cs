using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtraPowersAd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ExtraZoom(){
		PlayerPrefs.SetInt("ExtraZoom", 1);
	}
	
	public void ExtraBullet(){
		PlayerPrefs.SetInt("ExtraBullet", 1);
	}
	
	public void ExtraHealth(){
		PlayerPrefs.SetInt("ExtraHealth", 1);
	}
	
	public void ExtraFirstAid(){
		PlayerPrefs.SetInt("ExtraFirstAid", 1);
	}
	
	public void ExtraBomb(){
		PlayerPrefs.SetInt("ExtraBomb1", 1);
	}
	
	public void Back(){
		SceneManager.LoadScene("StartScene");
	}
}
