using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem
{
    public string playerName;
	public int score;
	public int death;
	
	public ScoreItem(string playerName, int score, int death){
		this.playerName = playerName;
		this.score = score;
		this.death = death;
	}
}
