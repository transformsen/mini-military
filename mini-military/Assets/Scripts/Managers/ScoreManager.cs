using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour {

 	public RectTransform scrodeCard;
	public static bool isPlayerDeath = false;


    void Update()
    {
        
		if (Input.GetKeyDown(KeyCode.Escape)){
			Debug.Log("Pause Goes Here");
		}
		
		if(/*Input.GetKeyDown(KeyCode.Tab)*/isPlayerDeath){
			scrodeCard.gameObject.SetActive(true);
		}else{
			scrodeCard.gameObject.SetActive(false);
		}
		
    }	
	
}
