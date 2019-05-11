using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateOpener : MonoBehaviour
{
	public int maxKills = 2;
	public int maxPlayer = 3;
	public GameObject gate;
	public GameObject gateText;
	public bool isGateOpen = false;
	public GameObject floatingTextPrefab;
	
    // Update is called once per frame
	void Start(){
		gate.SetActive(true);
		gateText.SetActive(true);		
	}
	
    void Update()
    {
		ArrayList players = GameManager.GetPlayers();
		string gameType = PlayerPrefs.GetString("GameType");
		
		if(/*"SL".Equals(gameType)*/true){
			GameObject pl = null;
			foreach(GameObject p in players){
				if(p != null){
					pl = p;
					break;
				}
			}
			if(pl != null){
				if(PlayerPrefs.GetInt("Mission3") == 1){
					gate.SetActive(false);
					gateText.SetActive(false);
				}else{
					int score = pl.GetComponent<PlayerFire>().score;				
					if(score >= maxKills){
						gate.SetActive(false);
						gateText.SetActive(false);					
					}else{
						gate.SetActive(true);
						gateText.SetActive(true);
					}
					if(!isGateOpen && (score >= maxKills)){
						isGateOpen = true;
						showPopup();
					}
				}				
			}			
		}else{
			int totalPlayer = 0;
			foreach(GameObject p in players){
				if(p != null){
					totalPlayer++;
				}
			}
			if(totalPlayer >= maxPlayer){
				gate.SetActive(false);
				gateText.SetActive(false);
			}else{
				gate.SetActive(true);
				gateText.SetActive(true);
			}
		}
		
    }
	
	void showPopup(){
		PlayerPrefs.SetInt("Mission3", 1);
		GameObject floatingTextCanvas = Instantiate(floatingTextPrefab);
		GameObject floatingText = floatingTextCanvas.transform.GetChild(0).gameObject;
		floatingText.GetComponent<Text>().text = "Mission-3 Completed! Gate Open!";
	}
}
