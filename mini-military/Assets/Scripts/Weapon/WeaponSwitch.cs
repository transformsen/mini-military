using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    
    void OnTriggerEnter(Collider collision)
    {
        GameObject hit = collision.gameObject;
        PlayerFire playerFire = hit.GetComponent<PlayerFire>();
		
        if (playerFire != null)
        {
			string activeWeapon = GetActiveWeaponName();
			playerFire.WeaponSwitch(activeWeapon);
			Destroy(gameObject);
			
		}
    }

    string GetActiveWeaponName()
    {
        string weapon_Name = null;
        foreach (Transform w in transform)
        {
            if (w.gameObject.activeSelf == true)
            {
                weapon_Name = w.name;
                break;
            }            
        }
        return weapon_Name;
    }


    public void resetSelection()
    {
        foreach (Transform w in transform)
        {
			w.gameObject.SetActive(false);
            
		}
    }
}
