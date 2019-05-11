using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class PlayerFire : NetworkBehaviour
{
	public WeaponSettingsDictionary weaponBareel;
	
	[SyncVar(hook = "OnActiveWeaponName")]
	public string activeWeaponName = "Pistel";
	public GameObject crossHiarPrefab;
	
	GameObject barrelPreFab;   
	
    public float timeBetweenBullets = 0.15f;        		// The time between each shot.
    public int totalNumberOfBullets = 25;      
    public Sprite imageforWeanpon;
	[SyncVar]
	public int numberOfBulletsLeft = 0;              		// Number bullets per load
	public string realoadingInText;
	float range = 100f;                      // The distance the gun can fire. 
	int reloadIntervel = 30;                        // Time Intervel between each reload;
	
	GameObject rightHandContainer;      			// Right hand container to hold weapon
	GameObject weaponObj;
	GameObject crossHair;
    public GameObject staticCrossHairPistel;
    public GameObject staticCrossHairAk47;
    public GameObject staticCrossHairFT;
    public GameObject staticCrossHairMM4;
    public GameObject staticCrossHairSX;
    public GameObject staticCrossHairAWP;
	
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	Animator anim;                      			// Reference to the animator component.
	
	System.DateTime reloadStartTime = System.DateTime.Now;
	float timer;                                    // A timer to determine when to fire.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	[SyncVar]
	public int score = 0;
	
	public GameObject floatingTextPrefab;
    public Camera cam;

	
	[SerializeField] public int maxZoom = 1;
	
	
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
	
	void Start(){
        if (isLocalPlayer)
        {
            GunStatusManager.playerGO = gameObject;
        }
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
        if (CrossPlatformInputManager.GetButton("Fire2") && timer >= timeBetweenBullets)
        {
			Debug.Log("Fire");
            ShootAnim(true);
            // ... shoot the gun only if it has bullets.
            if (numberOfBulletsLeft  > 0)
            {
                CmdShoot();
				//Bullets wasted
				numberOfBulletsLeft--;
            }
			// Reset the timer.
			timer = 0f;	
        }else{
			ShootAnim(false);
		}
		
		
		reloadGun();
		
    }
	
	[Command]
	void CmdShoot(){
		if (!isClient) //avoid to create bullet twice (here & in Rpc call) on hosting client
            CreateBullet();
		RpcShoot();
	}
	
	[ClientRpc]
    public void RpcShoot()
    {
		CreateBullet();
    }
	
	void CreateBullet(){
		Transform barrelEnd = weaponObj.transform.                                
                                Find(activeWeaponName).
                                Find("BarrelEnd").gameObject.transform;
		
		GameObject fireObject = (GameObject)Instantiate(
           barrelPreFab,
           barrelEnd.position,
           barrelEnd.rotation, barrelEnd);
		fireObject.GetComponent<Bullets>().myparent = gameObject;
		fireObject.GetComponent<Bullets>().Fire();
	}
	
	
	public void ToggleCrossHair(bool enabled)
    {
        if (crossHair != null)
        {
            crossHair.SetActive(enabled);
    
            staticCrossHairPistel.SetActive(!enabled);
            staticCrossHairAk47.SetActive(!enabled);
            staticCrossHairFT.SetActive(!enabled);
            staticCrossHairMM4.SetActive(!enabled);
            staticCrossHairSX.SetActive(!enabled);
            staticCrossHairAWP.SetActive(!enabled);
        }
    }

	
	public void InstanitateCrossHair(){
		
		if(crossHiarPrefab!= null)
        {
            
            if(crossHair == null)
            {
                crossHair = Instantiate(crossHiarPrefab);
            }
            ToggleCrossHair(false);
        }
	}

   
	[ClientRpc]
    public void RpcPositionCrossHair()
    {
		
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
                crossHair.transform.LookAt(cam.transform);
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
		
		barrelPreFab = weaponBareel[weaponName].barrelPreFab;                
		timeBetweenBullets = weaponBareel[weaponName].timeBetweenBullets;   
		totalNumberOfBullets = weaponBareel[weaponName].totalNumberOfBullets;
		if(PlayerPrefs.GetInt("ExtraBullet") == 1 ){
			totalNumberOfBullets = totalNumberOfBullets+5;
		}
		numberOfBulletsLeft	= totalNumberOfBullets;	
		imageforWeanpon = weaponBareel[weaponName].imageforWeanpon;
		range = weaponBareel[weaponName].range;		
		maxZoom = weaponBareel[weaponName].maxZoom;
		
		if(maxZoom > 1 && PlayerPrefs.GetInt("ExtraZoom") == 1 ){
			maxZoom = maxZoom+1;
		}
	}
	
	
	public void WeaponSwitch(string weaponName)
    {
	   Debug.Log("WeaponSwitch, weaponName="+weaponName);
	   activeWeaponName = weaponName;
	   SetPower(activeWeaponName);             
    }

    [ClientRpc]
    public void RpcWeaponSwitch()
    {
	   
	   activeWeaponName = "Pistel";	               
    }

    [ClientRpc]
    public void RpcSetPower(string weaponName){
		
		barrelPreFab = weaponBareel[weaponName].barrelPreFab;                
		timeBetweenBullets = weaponBareel[weaponName].timeBetweenBullets;   
		totalNumberOfBullets = weaponBareel[weaponName].totalNumberOfBullets;
		if(PlayerPrefs.GetInt(ExtraPowersAd.exPowerConstName+"Bullets") == 1 ){
			totalNumberOfBullets = totalNumberOfBullets+5;
		}
		numberOfBulletsLeft	= totalNumberOfBullets;	
		imageforWeanpon = weaponBareel[weaponName].imageforWeanpon;
		range = weaponBareel[weaponName].range;		
		maxZoom = weaponBareel[weaponName].maxZoom;
		
		if(maxZoom > 1 && PlayerPrefs.GetInt(ExtraPowersAd.exPowerConstName+"Zoom") == 1 ){
			maxZoom = maxZoom+1;
		}
	}
	
	void OnActiveWeaponName(string weaponName){
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
	
	public void reloadGun()
    {
        System.DateTime currentTime = System.DateTime.Now;
        int timeDiffBetweenReloads = (currentTime - reloadStartTime).Seconds;
        if(numberOfBulletsLeft <= 0)
        {
            realoadingInText = "RELOADING IN " + (reloadIntervel - timeDiffBetweenReloads);
            
        }
        
        if (timeDiffBetweenReloads > reloadIntervel)
        {
            numberOfBulletsLeft = totalNumberOfBullets;
            reloadStartTime = currentTime;
            realoadingInText = "";
        }        
    }
	
	[Server]
    public void AddScore(int power, Color color)
    {
		score+=power;
		//showPopup(power, color);
        //RpcAddScore();
       //RpcShowPopupForc(power, color);		
    }
 
    [ClientRpc]
    void RpcAddScore()
    {
        if(isLocalPlayer)
        {
            score+=1;
        }
    }
	
    [ClientRpc]
    void RpcShowPopupForc(int power, Color color){
        showPopup(power, color);
    }

	void showPopup(int power, Color color){
		if(isLocalPlayer)
        {
			GameObject floatingTextCanvas = Instantiate(floatingTextPrefab);
			GameObject floatingText = floatingTextCanvas.transform.GetChild(0).gameObject;
			floatingText.GetComponent<Text>().text = "+"+power;
			floatingText.GetComponent<Text>().color = color;
		}
	}
}
