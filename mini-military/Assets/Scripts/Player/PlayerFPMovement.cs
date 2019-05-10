using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFPMovement : NetworkBehaviour
{
    public float f_speed = 4.5f;            // The speed that the player will move at.
	public float b_speed = 1.5f;            // The speed that the player will move at.
	public float s_speed = 2f;            // The speed that the player will move at.
	
	public float cam_delta_y = 1f;
    public float cam_delta_z = 2f;
	public float cam_init_y = 3f;
    public float cam_init_z = 5f;
    public int cam_maxZoom = 3;
		
	public Camera cam;
	public FixedTouchField touchField;
	
	public float XSensitivity = .5f;
	public float YSensitivity = .5f;
	public float smoothTime = 10f;

    public AudioClip zoomClip;                                 // The audio clip to play when the player dies.
    public Material localPlayerMaterialRef;
    public GameObject miniMapId;
	
	public AudioClip[] footStep;                                 // The audio clip to play when the player dies.
	

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	   
	private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
	 
	private Vector2 lookAxis;
	public int cam_currentZoom = 1;
	private bool isForward = false;
	private bool isBackward = false;
	private bool isSide = false;
	private float speed = 5f;
    private PlayerFire PlayerFire;
    AudioSource zoomSound;
	 
	
    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        						
		zoomSound = GetComponent<AudioSource>();
        
    }

    void Start(){
        if (isLocalPlayer)
        {
            MiniMapCamaraFollow.target = transform;
			transform. Find("HUDCanvas").gameObject.SetActive(true);
            PlayerFire = GetComponent<PlayerFire>();
            miniMapId.GetComponent<Renderer>().material = localPlayerMaterialRef;
            Init(transform, cam.transform);
		    ZoomManager.playerGO = gameObject;

            if (!cam.enabled){
                cam.enabled = true;
            }
        }else{
            cam.enabled = false;
        }
        
    }
	

   

    void FixedUpdate()
    {
		if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
		lookAxis = FixedTouchField.TouchDist;
       
        // Store the input axes.
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
				
        Move(h, v);
		LookRotation(transform, cam.transform);

        // Animate the player.
        Animating(h, v);
		
		if(transform.position.y < -0.2 || transform.position.y > 0.2){
			Debug.Log("Goign below or Up");
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		}
       
    }
	
	void Update()
    {
          if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        
		cam_maxZoom = PlayerFire.maxZoom;

        if (CrossPlatformInputManager.GetButtonDown("Jump") ){
            zoomSound.clip = zoomClip;
            zoomSound.Play();
            if(cam_currentZoom <= cam_maxZoom){
				Vector3 newPos = new Vector3();
				newPos.Set(0, cam_delta_y, -cam_delta_z);
				cam.transform.localPosition  = cam.transform.localPosition  + newPos;
				cam_currentZoom = cam_currentZoom + 1;
			}else{
				Vector3 newPos = new Vector3();
				newPos.Set(0, cam_init_y, -cam_init_z);
				cam.transform.localPosition = newPos;
				cam_currentZoom = 1;
			}			
        }
        if(cam_currentZoom > cam_maxZoom){
            Vector3 newPos = new Vector3();
            newPos.Set(0, cam_init_y, -cam_init_z);
            cam.transform.localPosition = newPos;
            cam_currentZoom = 1;
        }

	}
		
		
		
		
	public void Init(Transform character, Transform camera)
    {
		m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;
	}

    
    void Move(float h, float v)
    {
        if(v > 0){
			isForward = true;
			isBackward = false;
			isSide = false;
			speed = f_speed;
		}else if(v < 0){
			isForward = false;
			isBackward = true;
			isSide = false;
			speed = b_speed;
		}else if(h != 0){
			isForward = false;
			isBackward = false;
			isSide = true;
			speed = s_speed;
		}else{
			isForward = false;
			isBackward = false;
			isSide = false;
			speed = 0;
		}
		
		
        Vector3 movement = v * Vector3.forward + h * Vector3.right;
		if (movement.magnitude > 1f) movement.Normalize();
        
        if (movement != Vector3.zero)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
		
    }
	
	
	public void LookRotation(Transform character, Transform camera)
    {
        float yRot = lookAxis.x * XSensitivity;
        float xRot = lookAxis.y * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
        
		character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
        camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);    
    }
		
		

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        //bool f_running = isForward;
		//bool b_running = isBackward || isSide;
		
        // Tell the animator whether or not the player is walking.        
        //anim.SetBool("isRunning", f_running);
		//anim.SetBool("isRunningBack", b_running);
        bool running = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.        
        anim.SetBool("isRunning", running);
		
    }
	
	void Step(){
		Debug.Log("Step");
		AudioClip clip = footStep[Random.Range(0, footStep.Length)];
		zoomSound.clip = clip;
        zoomSound.Play();
	}

}
