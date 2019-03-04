using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	public Text counterText;
	public RectTransform scrodeCardPanel;
	public float timeLeft = 30.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
		if(timeLeft > 0){
			counterText.text = ""+timeLeft;
		}else{
             GameManager.GameOver();
			 counterText.text = "";
         }
    }
}
