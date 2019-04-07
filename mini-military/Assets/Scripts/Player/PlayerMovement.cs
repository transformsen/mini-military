using DigitalRubyShared;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class PlayerMovement : NetworkBehaviour {
    public float speed = 5f;            // The speed that the player will move at.

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    

    private Transform m_Cam;
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    float m_TurnAmount;
    float m_ForwardAmount;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        						
								
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        
    }

    void Start(){
         if (isLocalPlayer)
        {
            CameraFollow.target = transform;
			transform. Find("HUDCanvas").gameObject.SetActive(true);

        }
         string gameType = PlayerPrefs.GetString("GameType");
         if("DM".Equals(gameType)){
		   if(LobbyManager.s_Singleton._playerNumber > 3){
              speed = 2.6f;
              m_MovingTurnSpeed = 180;
              m_StationaryTurnSpeed = 90;
           }else{
               speed = 4.8f;
              m_MovingTurnSpeed = 360;
              m_StationaryTurnSpeed = 180;
           }
	   }
    }

   

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }
        // Store the input axes.
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        if (m_Cam != null)
        {
            //Camara control for supporting turn
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }

        // Move the player around the scene.
        if (m_Move.magnitude > 1f) m_Move.Normalize();
        m_Move = transform.InverseTransformDirection(m_Move);
      
        m_TurnAmount = Mathf.Atan2(m_Move.x, m_Move.z);
        ApplyExtraTurnRotation();
        Move(h, v);

        // Animate the player.
        Animating(h, v);

       
    }


    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void Move(float h, float v)
    {
        
        Vector3 camF = m_Cam.forward;
        Vector3 camR = m_Cam.right;
        camF.y = 0;
        camR.y = 0;

        camR.Normalize();
        camF.Normalize();

        Vector3 movement = camR * h + camF * v;
        
        if (movement != Vector3.zero)
        {
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

    }


    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool running = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.        
        anim.SetBool("isRunning", running);
    }


}
