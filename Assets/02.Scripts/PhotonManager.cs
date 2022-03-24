using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    //게임버젼
    private readonly string gameVersion = "1.0";
    //유저명 
    public string userID = "MADhyunsoo";
    // UserID , Room Name InputField
    public TMP_InputField usrID_IF;
    public TMP_InputField roomName_IF;


    private void Awake()
    {
        //접속정보설정
        PhotonNetwork.GameVersion = gameVersion;
        //유저네임
        PhotonNetwork.NickName = userID;

        //방장이 씬을 로딩했을때 자동으로 씬이 호출되는 기능
        PhotonNetwork.AutomaticallySyncScene = false;

        //서버에접속이 안되어있다면
        if (!PhotonNetwork.IsConnected)
        {
            //포토서버접속
            PhotonNetwork.ConnectUsingSettings();

        }
    }
    private void Start()
    {
        // 유저명 로딩
        userID = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0, 1000):0000}");
        //usrID_IF.text = userID;
        //유저명 설정
        PhotonNetwork.NickName = userID;
    }
    #region PUN_CALLBACK
    //포톤 서버 (클라우드) 접속하면 호츨되는 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("서접완");
        // 로비접속
        PhotonNetwork.JoinLobby();
    }
    // lobby 에 접속했을 때 호불하는 콜백
    public override void OnJoinedLobby()
    {
        Debug.Log("로접완");
        //  PhotonNetwork.JoinRandomRoom();
    }
    //랜덤조인 실패했을때 호출되는 콜백 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"code={returnCode},message={message}");
        //룸속성정의
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        //룸생성 함수
        OnMakeeRoomButtonClick();
    }
    //룸생성후 호출 콜백함수
    public override void OnCreatedRoom()
    {
        Debug.Log("방생성");
    }
    //룸에 입장한후 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료");

        //배틀필드 씬으로 전환
        //if (PhotonNetwork.IsMasterClient == true)
        //{
            //  포톤에서 씬로드방법
            PhotonNetwork.LoadLevel("TestScene");
        //}



        // 게임매니저 스타트로 넘김

        //List<Transform> points = new List<Transform>();
        //GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(points);
        ////1번은 부모자체도 들어감 
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
      


        // 룸 속성을 정의
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        // 룸 생성
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
        // playerprefs 사용해서 usderid 저장
        PlayerPrefs.SetString("USER_ID", userID);
        PhotonNetwork.NickName = userID;
    }

    private void OnTriggerEnter(Collider cols)
    {
        print(1);
        if (cols.gameObject.CompareTag("Player"))
        {
            //SetUserID();
            PhotonNetwork.JoinRandomRoom();
        }
    }

    #endregion
}
