using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

 	public RectTransform scrodeCard;
	public static bool isPlayerDeath = false;

	void Start(){
		scrodeCard.gameObject.SetActive(false);
	}
	
    void Update()
    {
        
		if(isPlayerDeath){
			scrodeCard.gameObject.SetActive(true);
		}else{
			scrodeCard.gameObject.SetActive(false);
		}
		
    }
	
	public void Quit()
    {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
    }

	
}
