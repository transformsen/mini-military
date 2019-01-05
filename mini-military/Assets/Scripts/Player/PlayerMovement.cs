using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;            // The speed that the player will move at.
    public float rotationSpeed = 5f;            // The speed that the player will move at.
    public GameObject weapon;           // The Weapon object for attaching to player.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    GameObject rightHandContainer;      // Right hand container to hold weapon
    RotateGestureRecognizer rotateGesture;

    Joystick joystick;

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

        //SetupGesture();

        joystick = FindObjectOfType<Joystick>();
    }


    void FixedUpdate()
    {
        // Store the input axes.
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");

        float h = joystick.Horizontal;
        float v = joystick.Vertical;


        // Move the player around the scene.
        Move(h, v);

        // Turn the player to face the mouse cursor.
        //Turning();

        // Animate the player.
        Animating(h, v);

        //Attaching weapon
        AttachWeapon();
    }

    void Move(float h, float v)
    {
        //// Set the movement vector based on the axis input.
        //movement.Set(h, 0f, v);

        //// Normalise the movement vector and make it proportional to the speed per second.
        //movement = movement.normalized * speed * Time.deltaTime;

        //// Move the player to it's current position plus the movement.
        //playerRigidbody.MovePosition(transform.position + movement);

        //https://unity3d.com/learn/tutorials/topics/scripting/translate-and-rotate

        Vector3 movement = new Vector3(-h, 0.0f, -v);
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

    }

    void SetupGesture()
    {
        rotateGesture = new RotateGestureRecognizer();
        rotateGesture.Updated += RotateGestureCallBack;
        FingersScript.Instance.AddGesture(rotateGesture);
    }

    void RotateGestureCallBack(GestureRecognizer gesture, ICollection<GestureTouch> touches)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            transform.RotateAround(transform.position, Vector3.up, rotateGesture.RotationDegreesDelta * rotationSpeed);
        }
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
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
        //Enable required object
        //weapon.transform.Find("ScuFigun").gameObject.SetActive(true);

        //Assignning weapon to right hand container
        weapon.transform.SetParent(rightHandContainer.transform);        
    }

    public void DetachWeapon()
    {
        //TODO: drop position needs to fixed on the ground
        //Vector3 rightHandlePosition = rightHandContainer.transform.position;
        //Vector3 droppedWeaponPosition = rightHandlePosition;
        //weapon.transform.position = droppedWeaponPosition;

        //Detach weapon from its parent hand object.
        rightHandContainer.transform.DetachChildren(); 
        
    }

    public void ShootAnim(bool enable)
    {
        anim.SetBool("isShooting", enable);
    }
}
