using DigitalRubyShared;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;            // The speed that the player will move at.
    public GameObject weapon;           // The Weapon object for attaching to player.
    GameObject weaponObj;

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    GameObject rightHandContainer;      // Right hand container to hold weapon
    WeaponSwitch weaponSwitch;          // Reference to Weapon Switch Script

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

        weaponObj.transform.GetChild(7).gameObject.SetActive(true);

        weaponSwitch = weaponObj.GetComponent<WeaponSwitch>();

        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        
    }

   

    void FixedUpdate()
    {
        
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


    void AttachWeapon()
    {
        weapon.transform.SetParent(rightHandContainer.transform);               
    }

    public void DetachWeapon()
    {
        weaponObj.GetComponent<WeaponSwitch>().enableTrigger = true;
        rightHandContainer.transform.DetachChildren();      
        Destroy(weaponObj, 2f);   
    }

    public void ShootAnim(bool enable)
    {
        anim.SetBool("isShooting", enable);
    }

    public void SwitchWeapon(string weaponName)
    {
        weaponSwitch.SwitchWeapon(weaponName);
    }
}
