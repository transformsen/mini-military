using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour {

    AudioSource uiAudio;
	
	public Button coinButton;	
	public Button adButton;
	public AudioClip clickClip; 
	
	public GameObject loadingScreen;
    public Slider loadingSlider;
	public int coinsPay = 1000;
	public GameObject warningScreen;
	public Text coinsLeft;
	
    public GameObject[] playerAvatarsPrefab;
	private GameObject[] avatars;
    private int index = 0;

	void Awake() {
		uiAudio = GetComponent<AudioSource>();
		loadingScreen.SetActive(false);
		warningScreen.SetActive(false);
        index = PlayerPrefs.GetInt("SelectedAvatar");
		avatars = new GameObject[playerAvatarsPrefab.Length];
		 
        for(int i=0; i<playerAvatarsPrefab.Length; i++)
        {
            GameObject p_a = Instantiate(playerAvatarsPrefab[i], new Vector3(0, 0, 0), Quaternion.Euler(0,50,0));
			p_a.SetActive(false);
			avatars[i] = p_a;
        }
        avatars[index].SetActive(true);
	}
	
	void Update(){
		EnableCoinButton(index);
		int coins = PlayerPrefs.GetInt("Coins");
		coinsLeft.text = "coins: "+coins;
	}
	

    public void ToggleLeft()
    {
        avatars[index].SetActive(false);
        index--;
        if(index < 0)
        {
            index = avatars.Length - 1;
        }
        avatars[index].SetActive(true);		       
		playClickSound();
    }

    public void ToggleRight()
    {
        avatars[index].SetActive(false);
        index++;
        if (index >= avatars.Length)
        {
            index = 0;
        }
        avatars[index].SetActive(true);
		playClickSound();
    }
	
	public void EnableCoinButton(int avatarIndex){
		if(avatarIndex == 4){
			coinButton.gameObject.SetActive(true);
			adButton.gameObject.SetActive(false);
		}else{
			coinButton.gameObject.SetActive(false);
			adButton.gameObject.SetActive(true);
		}
	}
	
	public void pickByCoin(){
		int coins = PlayerPrefs.GetInt("Coins");
		if(coins < coinsPay){
			warningScreen.SetActive(true);
		}else{
			coins = coins - coinsPay;
			PlayerPrefs.SetInt("Coins", coins);
			Confirm();
		}
		
	}

    public void Confirm()
    {
		playClickSound();
		Debug.Log("Picked Avatar=" + index);
        PlayerPrefs.SetInt("SelectedAvatar", index);
        StartCoroutine(Load("StartScene"));
    }
	
	public void Back(){
		playClickSound();
        PurchaseBannerAds.HideBanner();
		StartCoroutine(Load("StartScene"));
	}
	
	
    void playClickSound() {
		uiAudio.clip = clickClip;
		uiAudio.Play();	
    }
	
	private IEnumerator Load(string senceName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(senceName);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {           
            loadingSlider.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingSlider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }

    }

}
