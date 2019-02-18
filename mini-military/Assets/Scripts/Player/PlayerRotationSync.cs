using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRotationSync : NetworkBehaviour
{
    [SyncVar]
    public Quaternion syncRotation;

    [SerializeField] Transform myTranform;
    [SerializeField] float lerpRate = 15f;

    void LateUpdate()
    {
        TransmitRotation();
        LerpRoration();
    }

    void LerpRoration(){
        if(!isLocalPlayer){           
            myTranform.rotation =  Quaternion.Lerp(myTranform.rotation , syncRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationToServer(Quaternion pos){
        syncRotation = pos;
    }

    [Client]
    void TransmitRotation(){
        if(isLocalPlayer){
            CmdProvideRotationToServer(myTranform.rotation );   
        }
    }
}
