using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Networking;
using System.Text;
using System.IO;
using Newtonsoft.Json;

public class SharkGamaManager : MonoBehaviourPunCallbacks
{
    enum State
    {
        START,
        PLAY,
        GAME_OVER,
    }
bool checkphoton;
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
            if(timer > WatingTime&&!checkphoton)
            {
                checkphoton=true;

                double totalScore = SharkScoreManager.Instance.CurrentScore;

                double coin = totalScore / 1000000;
                StartCoroutine("Request", coin);
               RoomOptions ro = new RoomOptions();
            ro.IsOpen = true;
            ro.IsVisible = true;
            ro.MaxPlayers = 20;
                 PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: ro);
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

     IEnumerator Request(double coin)
    {
        int randomIndex = TicketCache.randomIndex;
        string jsonPath = string.Format("Assets/07.Json/TicketInfo{0}.json", randomIndex);
        PurchaseTicketModel readJson = LoadJsonFile<PurchaseTicketModel>(jsonPath);
        string walletAddress = readJson.walletInfo.address;

        string json = "{\"walletAddress\":\""+walletAddress+"\", \"value\": "+coin+"}";
        var request = new UnityWebRequest(Constant.BASE_URL + Constant.EXECUTE_TRANSFER_COIN_TO_PLAYER, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        CoinCache.coin += coin;
        yield return request.Send();
        Debug.Log(CoinCache.coin);
        Debug.Log(request.responseCode);
    }

    T LoadJsonFile<T>(string loadPath)
    {
        FileStream fileStream = new FileStream(string.Format("{0}", loadPath), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
