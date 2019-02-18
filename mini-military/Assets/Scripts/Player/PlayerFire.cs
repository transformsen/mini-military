using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerFire : NetworkBehaviour
{
	public WeaponSettingsDictionary weaponBareel;
	public string activeWeaponName = "Pistel";
	public GameObject crossHiarPrefab;
	
	GameObject barrelPreFab;       
    float timeBetweenBullets = 0.15f;        		// The time between each shot.
    int totalNumberOfBullets = 25;      
    Sprite imageforWeanpon;
	int numberOfBulletsLeft = 0;              		// Number bullets per load
	float range = 100f;                      // The distance the gun can fire. 
	
	GameObject rightHandContainer;      			// Right hand container to hold weapon
	GameObject weaponObj;
	GameObject crossHair;
	
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	Animator anim;                      			// Reference to the animator component.
	
	System.DateTime reloadStartTime = System.DateTime.Now;
	float timer;                                    // A timer to determine when to fire.
	
    void Awake()
    {
		// Set up references.
		shootableMask = LayerMask.GetMask("Shootable");
        anim = GetComponent<Animator>();
		
        rightHandContainer = transform.
                                Find("Bip001").
                                Find("Bip001 Pelvis").
                                Find("Bip001 Spine").
                                Find("Bip001 R Clavicle").
                                Find("Bip001 R UpperArm").
                                Find("Bip001 R Forearm").
                                Find("Bip001 R Hand").
                                Find("R_hand_container").gameObject;

        weaponObj = rightHandContainer.transform.                                
                                Find("GunSpwnPoint").
                                Find("Weapons").gameObject;
		
		SetPower(activeWeaponName);
		
		InstanitateCrossHair();
    }

    // Update is called once per frame
    void Update()
    {
		if(isServer){
			RpcPositionCrossHair();
		}
		
		if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire2 button is being press and it's time to fire...
        if (Input.GetButtonDown("Fire2") && timer >= timeBetweenBullets)
        {
			Debug.Log("Fire");
            ShootAnim(true);
            // ... shoot the gun only if it has bullets.
            if (numberOfBulletsLeft  > 0)
            {
                CmdShoot();
            }
        }else{
			ShootAnim(false);
		}
		
    }
	
	[Command]
	void CmdShoot(){
		
		
		Transform barrelEnd = weaponObj.transform.                                
                                Find(activeWeaponName).
                                Find("BarrelEnd").gameObject.transform;
		
		GameObject fireObject = (GameObject)Instantiate(
           barrelPreFab,
           barrelEnd.position,
           barrelEnd.rotation);
        
		NetworkServer.Spawn(fireObject);
		
		//Bullets wasted
        numberOfBulletsLeft--;

        // Reset the timer.
        timer = 0f;		
	}
	
	public void ToggleCrossHair(bool enabled)
    {
        if (crossHair != null)
        {
            crossHair.SetActive(enabled);
        }
    }

	
	public void InstanitateCrossHair(){
		Debug.Log("RpcInstanitateCrossHair");
		if(crossHiarPrefab!= null)
        {
            
            if(crossHair == null)
            {
                crossHair = Instantiate(crossHiarPrefab);
            }
            ToggleCrossHair(false);
        }
	}

    //TODO: find the better way. Instanitate everytime Or Active FAlse. 
	[ClientRpc]
    public void RpcPositionCrossHair()
    {
		Debug.Log("RpcPositionCrossHair");
		Transform barrelEnd = weaponObj.transform.                                
                                Find(activeWeaponName).
                                Find("BarrelEnd").gameObject.transform;
								
        Ray ray = new Ray(barrelEnd.position, barrelEnd.forward);
        RaycastHit hit;
        Transform spawn = barrelEnd;
        Vector3 spwnPosition = spawn.position;
        Vector3 dir = ray.GetPoint(range);
        if (Physics.Raycast(ray, out hit, range, shootableMask))
        {
            if (crossHair != null)
            {
                ToggleCrossHair(true);
                crossHair.transform.position = hit.point;
                crossHair.transform.LookAt(Camera.main.transform);
            }
        }
        else
        {
            ToggleCrossHair(false);
        }

    }
	
	public void ShootAnim(bool enable)
    {
		if(anim != null){
			anim.SetBool("isShooting", enable);
		}
        
    }
	
	void SetPower(string weaponName){
		
		numberOfBulletsLeft = weaponBareel[weaponName].totalNumberOfBullets;
		barrelPreFab = weaponBareel[weaponName].barrelPreFab;                
		timeBetweenBullets = weaponBareel[weaponName].timeBetweenBullets;   
		totalNumberOfBullets = weaponBareel[weaponName].totalNumberOfBullets;      
		imageforWeanpon = weaponBareel[weaponName].imageforWeanpon;
		range = weaponBareel[weaponName].range;
	}
	
	
	public void WeaponSwitch(string weaponName)
    {
	   activeWeaponName = weaponName;
	   SetPower(activeWeaponName);
       foreach (Transform w in weaponObj.transform)
       {                
                if (weaponName.Equals(w.name)){
                    w.gameObject.SetActive(true);
				}
                else{   
					w.gameObject.SetActive(false);
                }
        }        
    }
}
