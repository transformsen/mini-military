using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour {

               // The position that that camera will be following.
    public float y = 3f;
    public float z = 10f;
    public int maxZoom = 1;
    public Text zoomXText;
    public FixedTouchField touchField;
    public float caramaAngleSpeed = 1f;
    public int currentZoom = 1;

    Vector3 offset;                     // The initial offset from the target.
    AudioSource zoomSound;
    float camaraAngle;
    GameObject player;
    static public Transform target;
	static public GameObject targetPower;

    void Start()
    {
        //TODO: How will this Work in Multi player Networtking?
        //player = GameObject.FindGameObjectWithTag("Player");
        //target = player.transform;
        //offset = transform.position - target.position;
        zoomSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Jump") ){
            zoomCalCulation();
            zoomSound.Play();
        }

        if(zoomXText != null)
        {
            zoomXText.text = currentZoom + "X";
        }
		
		if(targetPower != null){
			PlayerFire power = targetPower.GetComponent<PlayerFire>();
			maxZoom = power.maxZoom;
		}
		
		if(currentZoom > maxZoom){
			resetZoom();
		}
        

        camaraAngle += touchField.TouchDist.x * caramaAngleSpeed;

        if(target != null){
            transform.position = target.position + Quaternion.AngleAxis(camaraAngle, Vector3.up) * new Vector3(0, y, z);
            transform.rotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position, Vector3.up);  
        }
        
    }

    public void zoomCalCulation()
    {
        if(currentZoom < maxZoom)
        {
            currentZoom = currentZoom + 1;
            y = y + 7f;
            z = z + 7f;
        }
        else 
        {
			resetZoom();
            //currentZoom = 1;
            //y = y - 14f;
            //z = z - 14f;
        }
        
    }
	
	public void resetZoom(){
		y = 3f;
		z = 10f;		
		currentZoom = 1;
	}
}
