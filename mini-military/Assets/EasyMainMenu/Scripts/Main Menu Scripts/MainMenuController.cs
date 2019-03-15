using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    Animator anim;
	AudioSource uiAudio;
	

    public string newGameSceneName;
    public int quickSaveSlotID;

    [Header("Options Panel")]
    public GameObject MainOptionsPanel;
    public GameObject StartGameOptionsPanel;
    public GameObject GamePanel;
    public GameObject ControlsPanel;
    public GameObject GfxPanel;
    public GameObject LoadGamePanel;
		                           
	public GameObject loadingScreen;
    public Slider loadingSlider;
	
	public AudioClip hoverClip;                                 
	public AudioClip clickClip;  

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
		uiAudio = GetComponent<AudioSource>();
        //new key
        PlayerPrefs.SetInt("quickSaveSlot", quickSaveSlotID);
    }

    #region Open Different panels

    public void openOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(true);
        StartGameOptionsPanel.SetActive(false);
		loadingScreen.SetActive(false);
		loadingSlider.value = 0;

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
       
    }

    public void openStartGameOptions()
    {
        //enable respective panel
        MainOptionsPanel.SetActive(false);
        StartGameOptionsPanel.SetActive(true);
		loadingScreen.SetActive(false);
		loadingSlider.value = 0;

        //play anim for opening main options panel
        anim.Play("buttonTweenAnims_on");

        //play click sfx
        playClickSound();

        //enable BLUR
        //Camera.main.GetComponent<Animator>().Play("BlurOn");
        
    }

    public void openOptions_Game()
    {
        //enable respective panel
        GamePanel.SetActive(true);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);
		loadingScreen.SetActive(false);
		loadingSlider.value = 0;

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }
    public void openOptions_Controls()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(true);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(false);
		loadingScreen.SetActive(false);
		loadingSlider.value = 0;

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }
    public void openOptions_Gfx()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(true);
        LoadGamePanel.SetActive(false);
		loadingScreen.SetActive(false);
		loadingSlider.value = 0;

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }

    public void openContinue_Load()
    {
        //enable respective panel
        GamePanel.SetActive(false);
        ControlsPanel.SetActive(false);
        GfxPanel.SetActive(false);
        LoadGamePanel.SetActive(true);
		loadingScreen.SetActive(false);
		loadingSlider.value = 0;

        //play anim for opening game options panel
        anim.Play("OptTweenAnim_on");

        //play click sfx
        playClickSound();

    }
	
	public void goToAvatarSelection(){
		StartCoroutine(Load("AvatarSelectionScene"));
	}
	
	public void DeathMatch(){
		playGame("LobbyScene");
	}
	
	public void Survival(){
		playGame("GameScene");
	}

    public void playGame(string gameType)
    {
		StartCoroutine(Load(gameType));
    }
    #endregion

    #region Back Buttons

    public void back_options()
    {
        //simply play anim for CLOSING main options panel
        anim.Play("buttonTweenAnims_off");

        //disable BLUR
       // Camera.main.GetComponent<Animator>().Play("BlurOff");

        //play click sfx
        playClickSound();
    }

    public void back_options_panels()
    {
        //simply play anim for CLOSING main options panel
        anim.Play("OptTweenAnim_off");
        
        //play click sfx
        playClickSound();

    }

    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    
	#endregion

    #region Sounds
    public void playHoverClip()
    {
		uiAudio.clip = hoverClip;
		uiAudio.Play();	
    }

    void playClickSound() {
		uiAudio.clip = clickClip;
		uiAudio.Play();	
    }


    #endregion
	
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
