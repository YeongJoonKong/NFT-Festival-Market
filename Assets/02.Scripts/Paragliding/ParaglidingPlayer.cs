using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ParaglidingPlayer : MonoBehaviourPunCallbacks
{
   bool checkphoton;
    public float speed = 5.0f;
    public float fallingForce = 0.3f;
    public float restartTimer = 3.0f;

    public TextMeshProUGUI infoText;

    private bool isGameOver;
    private bool won;

    //public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "���� ���ʿ� �ִ� �ɿ� ������ ������ �ϼ���!";
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == false)
        {
            if (transform.position.y >= 224f)
            {
                transform.position = new Vector3(transform.position.x, 224f, transform.position.z);
            }
            Vector3 direction = new Vector3(Camera.main.transform.forward.x, -fallingForce, Camera.main.transform.forward.z);
            transform.position += direction.normalized * speed * Time.deltaTime;
           
        }
        else
        {
            restartTimer -= Time.deltaTime;

            if(restartTimer < 0)
            {
                if (isGameOver && !checkphoton) {
                    // checkphoton = true;
                    RoomOptions ro = new RoomOptions();
                    ro.IsOpen = true;
                    ro.IsVisible = true;
                    ro.MaxPlayers = 20;
                    
                    PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: ro);
                    PhotonNetwork.JoinRandomRoom();
                    
                }
            }
            else
            {
                if(won == true)
                {
                    infoText.text = "���߾��! �����߾��!";
                    infoText.text += "\n������ �ٽ� ���ư��ϴ�." + Mathf.Ceil(restartTimer);
                }
                else
                {
                    infoText.text = "�ƽ��׿� ������ �ٽ��ѹ� �غ�����!";
                    infoText.text += "\n������ �ٽ� ���ư��ϴ�." + Mathf.Ceil(restartTimer);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            won = true;
        }
        else
        {
            won = false;
        }
        isGameOver = true;
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
