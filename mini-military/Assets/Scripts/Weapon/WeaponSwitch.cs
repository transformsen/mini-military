using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    // Use this for initialization
    string weaponName = "ScuFigun";
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetKey(KeyCode.A))
        {
            weaponName = "w_rifle";
        }
        else if (Input.GetKey(KeyCode.S))
        {
            weaponName = "ScuFigun";
        }
        SwitchWeapon(weaponName);


    }

    void SwitchWeapon(string weaponName)
    {
        if(weaponName != null)
        {
            foreach (Transform w in transform)
            {                
                if (weaponName.Equals(w.name))
                {                    
                    w.gameObject.SetActive(true);
                }
                else
                {
                    w.gameObject.SetActive(false);
                }

            }
        }        
    }
}
