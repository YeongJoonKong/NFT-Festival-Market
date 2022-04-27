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

public class BirdGameManager : MonoBehaviourPunCallbacks
{
    enum State
    {
        START,
        Tutorial,
        PLAY,
        GAMEOVER,
    }
    State state;
bool checkphoton;
    public static float time;
    float timer;
    float timer2;
    public float timeLimit = 60;
    const float waitTime = 5;

    AudioSource audioSource;

    public birdGameBomb bombControll;
    public GameObject Hand;

    float SignCurrentTime;
    float SignWaitTime = 6;
    public GameObject Sign;

    public TextMeshProUGUI RemainingTimeText;

    public GameObject BirdGamePlayingGroup;
    public GameObject BirdGameOverGroup;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 51;

        this.state = State.START;
        this.timer = 0;
        this.audioSource = GetComponent<AudioSource>();
        this.bombControll = GetComponent<birdGameBomb>();
        RemainingTimeText.text = "Time : 0";
    }

    // Update is called once per frame
    void Update()
    {
        

        if (this.state == State.START)
        {
            this.state = State.PLAY;

            this.audioSource.Play();
        }
        else if (this.state == State.PLAY)
        {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            SignCurrentTime += Time.deltaTime;

            if(SignCurrentTime > SignWaitTime)
            {
                Sign.SetActive(false);
            }

            if (birdScoreManager.instance.BdCurrentScore >= 180)
            {
                this.state = State.GAMEOVER;
            }

            if (this.timer > timeLimit)
            {
                this.state = State.GAMEOVER;

                this.timer = 0;

                this.audioSource.loop = false;
            }

            RemainingTimeText.text = "Time : " + ((int)(timeLimit - timer));

        }
        else if (this.state == State.GAMEOVER)
        {
            BirdGamePlayingGroup.SetActive(false);
            BirdGameOverGroup.SetActive(true);
            this.timer2 += Time.deltaTime;

            if (this.timer2 > waitTime&&!checkphoton)
            {
                checkphoton=true;
                double totalScore = birdScoreManager.instance.BdCurrentScore;

                double coin = totalScore / 1000000;
                StartCoroutine("Request", coin);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
