using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Photon.Pun;
using Photon.Realtime;


public class GameManager : MonoBehaviourPunCallbacks
{

    enum State
    {
        START,
        PLAY,
        GAMEOVER,
    }

    public static float time;
    public float timeLimit = 30;
    const float waitTime = 7;

    MoleManager moleManager;
    public TextMeshProUGUI RemainingTimeText;
    //Text remainongTime;
    AudioSource audioSource;

    State state;
    float timer;

    public GameObject GamePlayingUI;
    public GameObject GameOverUI;

    List<MoleController> moles2 = new List<MoleController>();


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        this.state = State.START;
        this.timer = 0;
        this.moleManager = GameObject.Find("GameManager").GetComponent<MoleManager>();
        RemainingTimeText.text = "Time : 0";
        this.audioSource = GetComponent<AudioSource>();

        GameOverUI.SetActive(false);

        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Mole");

        foreach(GameObject go2 in gos2)
        {
            moles2.Add(go2.GetComponent<MoleController>());
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (this.state == State.START)
        {
            this.state = State.PLAY;

            this.moleManager.StartGenerate();

            this.audioSource.Play();
        }
        else if(this.state == State.PLAY)
        {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            if(this.timer > timeLimit)
            {
                this.state = State.GAMEOVER;

                this.moleManager.StopGenerate();

                this.timer = 0;

                this.audioSource.loop = false;
            }

            RemainingTimeText.text = "Time : " + ((int)(timeLimit - timer));
        }
        else if(this.state == State.GAMEOVER)
        {
            GameOverUI.SetActive(true);
            GamePlayingUI.SetActive(false);

            for (int i = 0; i < moles2.Count; i++)
            {
                this.moles2[i].GetComponent<SphereCollider>().enabled = false;
            }

            this.timer += Time.deltaTime;

            if(this.timer > waitTime)
            {
                double totalScore = WackAMoleScoreManager.instance.CurrentScore;

                double coin = totalScore / 1000000;
                Debug.Log(coin);
                StartCoroutine("Request", coin);

                PhotonNetwork.JoinRandomRoom();
                //SceneManager.LoadScene("Map_01");
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
