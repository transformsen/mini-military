using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

    public Texture2D image;
    public int size;
    public float maxAngle;
    public float minAngle;

    float lookHeight;
    public Camera cam;

    public void LookHeight(float value)
    {
        lookHeight += value;

        if(lookHeight > maxAngle || lookHeight < minAngle)
        {
            lookHeight -= value;
        }
    }

    public void Awake()
    {
      
           
        
    }

    private void OnGUI()
    {
        
        Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y-lookHeight, size, size), image);
    }
}
