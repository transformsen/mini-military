using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour {

    public static int score;        // The player's score.


    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Kills: " + score;
		if (Input.GetKeyDown(KeyCode.Escape)){
			Debug.Log("GameManager.GetPlayers()"+GameManager.GetPlayers().Count);
			ArrayList players = GameManager.GetPlayers();
			foreach(GameObject p in players){
				int pscore = p.GetComponent<PlayerFire>().score;
				NetworkIdentity m_Identity = p.GetComponent<NetworkIdentity>();				
				Debug.Log("netId = "+m_Identity.netId+"score="+pscore);
			}
		}
		
    }
}
