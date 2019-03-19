using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class TimeManager : NetworkBehaviour
{
	public Text counterText;
	
	[SyncVar] public float timeLeft = 300.0f;
	
	[SyncVar] public bool masterTimer = false; //Is this the master timer?
 
     TimeManager serverTimer;
	
	bool timeUp = false;
	
    // Start is called before the first frame update
    void Start()
    {
		
		 timeUp = false;
		 
         if(isServer){ // For the host to do: use the timer and control the time.				 
			serverTimer = this;
			masterTimer = true;            
         }else { //For all the boring old clients to do: get the host's timer.
			Debug.Log("Clients Local Player");
             TimeManager[] timers = FindObjectsOfType<TimeManager>();
             for(int i =0; i<timers.Length; i++){
                 if(timers[i].masterTimer){
                     serverTimer = timers [i];
                 }
             }
         }
    }

    // Update is called once per frame
    void Update()
    {
		
		if(masterTimer){ //Only the MASTER timer controls the time
			timeLeft -= Time.deltaTime;
		}
		
		if(true){ //EVERYBODY updates their own time accordingly.
             if (serverTimer) {
                 timeLeft = serverTimer.timeLeft;
             } else { //Maybe we don't have it yet?
                 TimeManager[] timers = FindObjectsOfType<TimeManager>();
                 for(int i =0; i<timers.Length; i++){
                     if(timers[i].masterTimer){
                         serverTimer = timers [i];
                     }
                 }
             }
		}
		
		int minutes = (int)Math.Floor(timeLeft/60);
		int seconds = (int)Math.Floor(timeLeft%60);
		
		if(timeLeft > 0){
			string minutesDisplay = (minutes > 9) ? ""+minutes : "0"+minutes;
			string secondsDisplay = (seconds > 9) ? ""+seconds : "0"+seconds;
			counterText.text = minutesDisplay+":"+secondsDisplay;
		}else if(!timeUp){
			 timeUp = true;
             GameManager.GameOver();
			 counterText.text = "";
         }
    }
}
