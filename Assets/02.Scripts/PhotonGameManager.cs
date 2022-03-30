using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonGameManager : MonoBehaviourPunCallbacks
{
    //public static PhotonGameManager instance;
    



    [System.NonSerialized]
    public PhotonView pv;
    public GameObject CameaRig;

    private void Awake()
    {
        //instance = this;
        pv = GetComponent<PhotonView>();

        Vector3 pos = Vector3.zero + Vector3.up * 1.5f; //
        PhotonNetwork.Instantiate("Player_OVR", pos, Quaternion.identity, 0);
        
    }
    // Start is called before the first frame update
    void Start()
    {



        
    }


    

    //clients object cleanup È£ÃâµÇ´Â ÄÝ¹é
    public override void OnLeftRoom()
    {
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        //string msg = $"<color=#00ff00>[{newPlayer.NickName}]</color>´ÔÀÌ ÀÔÀåÇÏ¼Ì½À´Ï´Ù.";
        //ChatMessage(msg);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
       
        //string msg = $"<color=#ff0000>[{otherPlayer.NickName}]</color>´ÔÀÌ ÅðÀåÇÏ¼Ì½À´Ï´Ù.";
        //ChatMessage(msg);

    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //if (pv.Owner.ActorNumber == newMasterClient.ActorNumber)
        //{
        //    Debug.Log("¹æÀå½Â°è");
        //    //this.SendMessage($"<color=#ffff00>[{pv.Owner.NickName}]</color>´ÔÀÌ ¹æÀåÀÌ µÆ½À´Ï´Ù");
        //}
    }
}
