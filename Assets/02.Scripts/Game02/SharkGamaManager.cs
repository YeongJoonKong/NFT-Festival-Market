using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SharkGamaManager : MonoBehaviourPunCallbacks
{
    enum State
    {
        START,
        PLAY,
        GAME_OVER,
    }

    State state;

    float timer;
    public static float SharkTime;
    public float sharkTimeLimit = 40;
    public float WatingTime = 5.0f;

    public TextMeshProUGUI RemainingSharkTimeText;

    SharkManager sharkManager;

    // Start is called before the first frame update
    void Start()
    {
        this.state = State.START;
        this.timer = 0;
        sharkManager = GameObject.Find("SharkGameManager").GetComponent<SharkManager>();
        RemainingSharkTimeText.text = "Time : 0";

    }


    // Update is called once per frame
    void Update()
    {
        if(state == State.START)
        {
            sharkManager.StartGenerate();
            this.state = State.PLAY;
        }
        else if(state == State.PLAY)
        {
            this.timer += Time.deltaTime;

            SharkTime = timer / sharkTimeLimit;
            
            if(timer >= sharkTimeLimit)
            {
                this.state = State.GAME_OVER;
                
                this.sharkManager.StopGenerate();
                
                this.timer = 0;
            }
            RemainingSharkTimeText.text = "Time : " + ((int)(sharkTimeLimit - timer));
        }
        else if(state == State.GAME_OVER)
        {
            timer += Time.deltaTime;
            if(timer > WatingTime)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;


        PhotonNetwork.CreateRoom("room", ro);
    }
    public override void OnCreatedRoom()
    {

    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Map_01");
    }
}
