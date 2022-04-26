using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    //���ӹ���
    private readonly string gameVersion = "1.0";
    //������ 
    public string userID = "MADhyunsoo";
    // UserID , Room Name InputField
    public TMP_InputField usrID_IF;
    public TMP_InputField roomName_IF;


    private void Awake()
    {
        //������������
        PhotonNetwork.GameVersion = gameVersion;
        //��������
        PhotonNetwork.NickName = userID;

        //������ ���� �ε������� �ڵ����� ���� ȣ��Ǵ� ���
        PhotonNetwork.AutomaticallySyncScene = false;

        //������������ �ȵǾ��ִٸ�
        if (!PhotonNetwork.IsConnected)
        {
            //���伭������
            PhotonNetwork.ConnectUsingSettings();

        }
    }
    private void Start()
    {
        // ������ �ε�
        userID = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0, 1000):0000}");
        //usrID_IF.text = userID;
        //������ ����
        PhotonNetwork.NickName = userID;
    }
    #region PUN_CALLBACK
    //���� ���� (Ŭ����) �����ϸ� ȣ���Ǵ� �ݹ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("������");
        // �κ�����
        PhotonNetwork.JoinLobby();
    }
    // lobby �� �������� �� ȣ���ϴ� �ݹ�
    public override void OnJoinedLobby()
    {
        Debug.Log("������");
        //  PhotonNetwork.JoinRandomRoom();
    }
    //�������� ���������� ȣ��Ǵ� �ݹ� 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode},message={message}");
        //��Ӽ�����
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        //����� �Լ�
        OnMakeeRoomButtonClick();
    }
    //������� ȣ�� �ݹ��Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("�����");
    }
    //�뿡 �������� ȣ��Ǵ� �ݹ��Լ�
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");

        //��Ʋ�ʵ� ������ ��ȯ
        //if (PhotonNetwork.IsMasterClient == true)
        //{
            //  ���濡�� ���ε���
            PhotonNetwork.LoadLevel("Map_01");
            //PhotonNetwork.LevelLoadingProgress

        //}



        // ���ӸŴ��� ��ŸƮ�� �ѱ�

        //List<Transform> points = new List<Transform>();
        //GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(points);
        ////1���� �θ���ü�� �� 
        //int idx = Random.Range(1, points.Count);

        //Vector3 pos = points[idx].position; //
        //PhotonNetwork.Instantiate("Tank", pos, Quaternion.identity, 0);
    }

    #endregion
    
    #region UI_CALLBACK
    public void OnLoginButtonClick()
    {
        //SetUserID();
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnMakeeRoomButtonClick()
    {
        //SetUserID();
      


        // �� �Ӽ��� ����
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        // �� ����
        PhotonNetwork.CreateRoom("room", ro);

    }
    #endregion

    #region USER_DIFINE_FUNC
    void SetUserID()
    {
        if (string.IsNullOrEmpty(usrID_IF.text))
        {
            userID = $"USER_{Random.Range(0, 1000):0000}";
            usrID_IF.text = userID;
        }
        userID = usrID_IF.text;
        // playerprefs ����ؼ� usderid ����
        PlayerPrefs.SetString("USER_ID", userID);
        PhotonNetwork.NickName = userID;
    }

    private void OnTriggerEnter(Collider cols)
    {
        print(1);
        if (cols.gameObject.CompareTag("Player"))
        {
            // StartCoroutine(Loading(cols));

            //SetUserID();
            PhotonNetwork.JoinRandomRoom();
        }
        //SceneManager.LoadScene("Map_01");
    }

    IEnumerator Loading(Collider cols)
    {
        Canvas loading = cols.GetComponentInChildren<Canvas>();
        
        loading.enabled = true;

        yield return null;
    }
    #endregion
}
