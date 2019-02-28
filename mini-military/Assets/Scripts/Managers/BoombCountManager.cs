using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoombCountManager : MonoBehaviour
{
	public Text boombCountText;
	
	static public GameObject playerGO;
	
   

    // Update is called once per frame
    void Update()
    {
        if(playerGO != null){	
			PlayerBombAttack player = playerGO.GetComponent<PlayerBombAttack>();
			boombCountText.text = (player.numberOfBombs > 0) ? "" + player.numberOfBombs : "";
		}
    }
}
