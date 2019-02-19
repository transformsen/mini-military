using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunStatusManager : MonoBehaviour
{
	
	static public GameObject playerGO;
	
	public Text totalNumberOfBulletsText;
    public Text numberOfBulletsLeftText;
    public Text realoadingInText;
    public Image weapon2D;
	
	public Image gunStatusContainerImage;	
	

    // Update is called once per frame
    void Update()
    {   
		if(playerGO != null){	
			gunStatusContainerImage.gameObject.SetActive(true);
			PlayerFire player = playerGO.GetComponent<PlayerFire>();
			totalNumberOfBulletsText.text = "" + player.totalNumberOfBullets;
			numberOfBulletsLeftText.text = "" + player.numberOfBulletsLeft;
			weapon2D.sprite = player.imageforWeanpon;
			realoadingInText.text = player.realoadingInText;
			
			
		}else{
			gunStatusContainerImage.gameObject.SetActive(false);
		}
    }
}
