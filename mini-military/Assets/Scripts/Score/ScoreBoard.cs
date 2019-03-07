using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
	
   [SerializeField]
   GameObject playerScoreBoardItemPrefab;
	
   [SerializeField]
   Transform playerScoreList;
   
   void OnEnable(){
	   StartCoroutine(UpdateScoreBoard());	
	   if(GameManager.isGameOver){
			StartCoroutine(GoToLobby());
	   }
	}
   
   IEnumerator UpdateScoreBoard(){
		yield return new WaitForSeconds(3f);
		 ArrayList players = GameManager.GetPlayers();
		foreach(GameObject p in players){
			
			//if (p.activeSelf == true){
				string name = p.GetComponent<PlayerHealth>().playerName;
				int score = p.GetComponent<PlayerFire>().score;
				int death = p.GetComponent<PlayerHealth>().death;
			
				GameObject playerScoreBoardItem = Instantiate(playerScoreBoardItemPrefab, playerScoreList);
				PlayerScoreBoardItem item = playerScoreBoardItem.GetComponent<PlayerScoreBoardItem>();
				if(item != null){
					item.SetUp(name, score, death);
				}
			//}				
		}
   }
   
   void OnDisable(){
	   foreach(Transform child in playerScoreList){
		   Destroy(child.gameObject);
	   }
   }
   
   IEnumerator GoToLobby(){
	   yield return new WaitForSeconds(8f);
	   SceneManager.LoadScene("LobbyScene");
   }
}
