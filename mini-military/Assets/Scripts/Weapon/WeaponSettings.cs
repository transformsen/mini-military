using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponSettings
{
   public GameObject barrelPreFab;
   
   public int damagePerShot = 20;                  // The damage inflicted by each bullet.
   public float timeBetweenBullets = 0.15f;        // The time between each shot.
   public float range = 100f;                      // The distance the gun can fire.
   
   public int totalNumberOfBullets = 25;
      
   public Sprite imageforWeanpon;
   
   public int maxZoom = 1;
}
