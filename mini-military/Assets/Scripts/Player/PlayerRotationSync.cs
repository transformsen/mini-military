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
            myTranform.localRotation =  Quaternion.Lerp(myTranform.localRotation , syncRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideRotationToServer(Quaternion pos){
        syncRotation = pos;
    }

    [Client]
    void TransmitRotation(){
        if(isLocalPlayer){
            CmdProvideRotationToServer(myTranform.localRotation );   
        }
    }
}
