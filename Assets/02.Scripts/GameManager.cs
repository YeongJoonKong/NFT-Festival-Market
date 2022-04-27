using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
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
                int totalScore = WackAMoleScoreManager.instance.CurrentScore;
                //290 ~390
                int coin = totalScore / 1000000;

                // WWWForm form = new WWWForm();
                
                // PurchaseTicketModel readJson = LoadJsonFile<PurchaseTicketModel>("Assets/07.json/TicketInfo1.json");

                // string walletInfo = JsonConvert.SerializeObject(readJson.walletInfo);
                // form.AddField("walletInfo", walletInfo);
                // form.AddField("filename", filename);
                
                PurchaseTicketModel readJson = LoadJsonFile<PurchaseTicketModel>("Assets/07.json/TicketInfo1.json");
                string walletAddress = JsonConvert.SerializeObject(readJson.walletInfo.address);
                
                

                using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + Constant.EXECUTE_TRANSFER_COIN_TO_PLAYER))
                {
                }
                // using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + Constant.CREATE_NFT_OBJECT_CONTRACT, form))
                // {
                //     yield return request.SendWebRequest();

                //     if (request.result != UnityWebRequest.Result.Success){
                //         MakeFailedResponse();
                //     } else {
                //         var result = new JObject();
                //         result.Merge(JObject.Parse(request.downloadHandler.text));
                //         result.Merge(JObject.Parse(walletInfo));

                //         string res = JsonConvert.SerializeObject(result);
                //         UnityWebRequest request1 = new UnityWebRequest(Constant.BASE_URL + Constant.CREATE_NFT_OBJECT, "POST");
                        
                //         byte[] encodedRequest = new System.Text.UTF8Encoding().GetBytes(res);
                //         request1.uploadHandler = (UploadHandler)new UploadHandlerRaw(encodedRequest);
                //         request1.downloadHandler = new DownloadHandlerBuffer();
                //         request1.SetRequestHeader("Content-Type", "application/json");

                //         yield return request1.SendWebRequest();

                //         if (request1.result != UnityWebRequest.Result.Success) {

                //         } else {
                //             CreateJsonFile(Constant.SAVE_JSON_PATH, "NFTInfo_"+filename, request1.downloadHandler.text);
                //         }
                //     }
                // }
                ///execute/transfer/coin
                SceneManager.LoadScene("Map_01");
            }
        }
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
