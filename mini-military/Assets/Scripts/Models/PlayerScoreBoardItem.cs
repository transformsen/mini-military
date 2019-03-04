using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBoardItem : MonoBehaviour
{
	[SerializeField]
    public Text playerNameText;
    [SerializeField]
	public Text killsText;
	[SerializeField]
	public Text deathsText;
	
	public void SetUp(string playerName, int kills, int deaths){
		playerNameText.text = playerName;
		killsText.text = ""+kills;
		deathsText.text = ""+deaths;		
	}
}
