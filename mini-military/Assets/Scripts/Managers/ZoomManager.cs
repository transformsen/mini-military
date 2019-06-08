using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomManager : MonoBehaviour
{
   public Text currentZoomText;	
	static public GameObject playerGO;

    
    // Update is called once per frame
    void Update()
    {
        if(playerGO != null){	
			PlayerFPMovement player = playerGO.GetComponent<PlayerFPMovement>();
			currentZoomText.text = player.cam_currentZoom + "X";
		}
    }
}
