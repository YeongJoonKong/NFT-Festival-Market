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

    //public GameObject avatar1;

    private void Awake()
    {
        //instance = this;
        pv = GetComponent<PhotonView>();

        //avatar1.SetActive(false);
        //Vector3 pos = Vector3.zero + Vector3.up * 1.5f; //
        //PhotonNetwork.Instantiate("PlayerNetWork", pos, Quaternion.identity, 0);
        PhotonNetwork.IsMessageQueueRunning = true;
        List<Transform> points = new List<Transform>();
        GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(points);
        int idx = Random.Range(1, points.Count);

        Vector3 molepos = GameObject.Find("SpawnPoint_mole").GetComponent<Transform>().position;
        Vector3 sharkpos = GameObject.Find("SpawnPoint_Shark").GetComponent<Transform>().position;
        Vector3 birdpos = GameObject.Find("SpawnPoint_Bird").GetComponent<Transform>().position;
        Vector3 parapos = GameObject.Find("SpawnPoint_Parachute").GetComponent<Transform>().position;

        



        Vector3 pos = points[idx].position; //

        //PhotonNetwork.Instantiate("PlayerNetWork", pos, Quaternion.identity, 0);
        if (Playdata.instance == null)
        {
            StartCoroutine(WaitForLoadNextScene());
        }
        else
        {
            SpawnPlayer();
        }

        IEnumerator WaitForLoadNextScene()
        {
            if (Playdata.instance == null)
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(WaitForLoadNextScene());
            }
            else
            {
                SpawnPlayer();
            }
        }

        void SpawnPlayer()
        {
            if (Playdata.instance.spawnPointData == 0)
            {
                PhotonNetwork.Instantiate("PlayerNetWork", pos, Quaternion.identity, 0);
            }
            else if (Playdata.instance.spawnPointData == 1)
            {
                PhotonNetwork.Instantiate("PlayerNetWork", molepos, Quaternion.identity, 0);
            }
            else if (Playdata.instance.spawnPointData == 2)
            {
                PhotonNetwork.Instantiate("PlayerNetWork", sharkpos, Quaternion.identity, 0);
            }
            else if(Playdata.instance.spawnPointData == 3)
            {
                PhotonNetwork.Instantiate("PlayerNetWork", birdpos, Quaternion.identity, 0);
            }
            else if(Playdata.instance.spawnPointData == 4)
            {
                PhotonNetwork.Instantiate("PlayerNetWork", parapos, Quaternion.identity, 0);
            }
        }


    }
    // Start is called before the first frame update
    void Start()
    {



        
    }

    

    

    //clients object cleanup ȣ��Ǵ� �ݹ�
    public override void OnLeftRoom()
    {
       
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        //string msg = $"<color=#00ff00>[{newPlayer.NickName}]</color>���� �����ϼ̽��ϴ�.";
        //ChatMessage(msg);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
       
        //string msg = $"<color=#ff0000>[{otherPlayer.NickName}]</color>���� �����ϼ̽��ϴ�.";
        //ChatMessage(msg);

    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //if (pv.Owner.ActorNumber == newMasterClient.ActorNumber)
        //{
        //    Debug.Log("����°�");
        //    //this.SendMessage($"<color=#ffff00>[{pv.Owner.NickName}]</color>���� ������ �ƽ��ϴ�");
        //}
    }
}
