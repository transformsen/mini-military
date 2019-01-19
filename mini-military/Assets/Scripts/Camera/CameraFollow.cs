﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    public float rotationSpeed = 4f;    // Spped of the camera to rotate.

    [Range(0.1f, 1.0f)]
    public float smoothFactor = 0.5f;

    Vector3 offset;                     // The initial offset from the target.

    public FixedTouchField touchField;

    float camaraAngle;
    public float caramaAngleSpeed = 1f;

    void Start()
    {
        // Calculate the initial offset.
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        offset = transform.position - target.position;
        
    }

    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        //Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        camaraAngle += touchField.TouchDist.x * caramaAngleSpeed;

        transform.position = target.position + Quaternion.AngleAxis(camaraAngle, Vector3.up) * new Vector3(0, 5, 6);
        transform.rotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position, Vector3.up);
    }

    //private void LateUpdate()
    //{
    //    offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up) * offset;        
    //    Vector3 newPosition = target.position + offset;
    //    transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

    //    transform.LookAt(target);       
    //}
}
