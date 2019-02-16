using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRootShoot : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!isLocalPlayer){
			return;
		}
        if (Input.GetButton("Fire2")){
			
			Transform w = FindActiveweapon();
			PlayerShooting wshoot = w.Find("BarrelEnd").GetComponent<PlayerShooting>();
			wshoot.PShoot();
		}
    }
	
	Transform FindActiveweapon(){
		GameObject rightHandContainer = transform.
                                Find("Bip001").
                                Find("Bip001 Pelvis").
                                Find("Bip001 Spine").
                                Find("Bip001 R Clavicle").
                                Find("Bip001 R UpperArm").
                                Find("Bip001 R Forearm").
                                Find("Bip001 R Hand").
                                Find("R_hand_container").gameObject;
								
		Transform weaponObjTrans = rightHandContainer.transform.                                
                                Find("GunSpwnPoint").
                                Find("Weapons").gameObject.transform;
		Transform activeTans = null;						
		foreach (Transform w in weaponObjTrans){
			
            if (w.gameObject.activeSelf == true)
            {
              activeTans = w;
			  break;
            }            
        }
		return activeTans; 		
	}
}
