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
	public Text myScore;
	
	public int mileStoneScore = 3;

	public int mileStone = 2;
	public GameObject floatingTextPrefab;
	public Color floatingTextColor;

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
			myScore.text = "KILLS: "+player.score;
			if(player.score + mileStoneScore >= mileStoneScore * mileStone){
				mileStone ++;
				showPopup();
			}
			
		}else{
			gunStatusContainerImage.gameObject.SetActive(false);
		}
    }

	void showPopup(){		
		GameObject floatingTextCanvas = Instantiate(floatingTextPrefab);
		GameObject floatingText = floatingTextCanvas.transform.GetChild(0).gameObject;
		floatingText.GetComponent<Text>().text = "Completed Milestone " + (mileStone-1);
		floatingText.GetComponent<Text>().color = floatingTextColor;
	}
}
