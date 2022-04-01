using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Text;
using System;
using System.Collections.Generic;

public class TicketGuideAvatar : MonoBehaviour, SubjectLobby
{
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    public AudioClip[] audioClips;
    public GameObject player;
    
    Animator anim;
    AudioSource audioSource;
    bool _isScriptEnd;
    bool _isMakeNFTTicketEnd;
    bool _waitAnswer;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (audioSource.isPlaying)
        {
            Vector3 tr = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(tr);
        }
    }


    public IEnumerator Guide(int index, Action callback)
    {
        if (index == 0 || index == 2) {
            if (index == 2) {
                SetWaitAnswer(true);
            }
            NotifyObserver();
            yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Two));
        }

        SetWaitAnswer(false);
        NotifyObserver();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < audioClips.Length)
        {
            StartCoroutine(Guide(index, callback));
            
        } else
        {
            index = 0;
            StartCoroutine(makeWalletAndNFT());
            _isScriptEnd = true;
            NotifyObserver();
            if (callback != null) callback();
        }
    }


    // TODO : 입장권 만드는 애니메이션 넣기 
    public IEnumerator makeWalletAndNFT()
    {
        anim.SetTrigger("makeNFT");
        FileInfo fi = new FileInfo("Assets/07.Json/TicketInfo.json");
        if (fi.Exists)
        {
            var ticketInfoJson = LoadJsonFile<PurchaseTicketModel>("Assets/07.Json", "TicketInfo");
            // anim.SetTrigger("Idle2");
            print(ticketInfoJson);
            anim.ResetTrigger("makeNFT");
            anim.SetTrigger("Idle2");
        } 
        else
        {
            using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + "/users/purchase/ticket", ""))
            {
                yield return request.SendWebRequest();

                // TODO: 실패 처리 
                if (request.result != UnityWebRequest.Result.Success)
                {
                    print(request.result);
                }
                else
                {
                    CreateJsonFile("Assets/07.Json", "TicketInfo", request.downloadHandler.text);
                    anim.SetTrigger("Idle2");
                }
            }
        }

    }


    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }


    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public bool GetIsScriptEnd() {
        return _isScriptEnd;
    }

    public bool GetWaitAnswer() {
        return _waitAnswer;
    }

    public void SetWaitAnswer(bool value) {
        _waitAnswer = value;
    }

    public void AddObserver(ObserverLobby subscriber) {
        this._subscribers.Add(subscriber);
    }

    public void RemoveObserver(ObserverLobby subscriber) {
        if (_subscribers.Contains(subscriber))
        {
            this._subscribers.Remove(subscriber);
        }
    }
    
    public void NotifyObserver() {
        foreach(var subscriber in _subscribers)
        {
            subscriber.DetectEvent(this);
        }
    }
}
