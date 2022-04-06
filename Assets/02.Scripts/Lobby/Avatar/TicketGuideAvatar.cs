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
    public GameObject iPad;
    public GameObject textBubble;
    
    Animator anim;
    AudioSource audioSource;
    bool _isMakeNFTTicketEnd;

    NFTManager _nftManager = new NFTManager();
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        iPad.SetActive(false);
    }


    void Update()
    {
        if (audioSource.isPlaying)
        {
            Vector3 tr = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(tr);
        }
    }


    public IEnumerator Guide(int index)
    {
        if (index == 0)
        {
            NotifyObserver("WAIT_PLAYER_TRY_CONVERSATION");
            yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Two));
            NotifyObserver("PLAYER_TRIED_CONVERSATION");
        }

        if (index == 2) 
        {
            NotifyObserver("WAIT_PLAYER_PURCHASE_TICKET_ANSWER");
            yield return new WaitUntil(() => !textBubble.activeInHierarchy);
        }

        NotifyObserver("PLAYER_ANSWER_PURCHASE_TICKET");
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < audioClips.Length)
        {
            StartCoroutine(Guide(index));
            
        } else
        {
            index = 0;
            StartCoroutine(makeWalletAndNFT());
        }
    }


    public IEnumerator makeWalletAndNFT()
    {
        anim.SetTrigger("makeNFT");
        iPad.SetActive(true);
        FileInfo fi = new FileInfo("Assets/07.Json/TicketInfo.json");
        if (fi.Exists)
        {
            var ticketInfoJson = LoadJsonFile<PurchaseTicketModel>("Assets/07.Json", "TicketInfo");
            // anim.SetTrigger("Idle2");
            print(ticketInfoJson);
            iPad.SetActive(false);
            anim.ResetTrigger("makeNFT");
            anim.SetTrigger("Idle2");

            NotifyObserver("MAKE_NFT_END");
        } 
        else
        {
            using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + "/users/purchase/ticket", ""))
            {
                yield return request.SendWebRequest();

                // TODO: 실패 처리 
                if (request.result != UnityWebRequest.Result.Success)
                {
                    anim.SetTrigger("Idle2");
                    iPad.SetActive(false);
                    print(request.result);
                }
                else
                {
                    CreateJsonFile("Assets/07.Json", "TicketInfo", request.downloadHandler.text);
                    anim.SetTrigger("Idle2");
                    iPad.SetActive(false);
                    NotifyObserver("MAKE_NFT_END");
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

    public void NotifyObserver(string _event) {
        foreach(var subscriber in _subscribers)
        {
            subscriber.DetectEvent(_event);
        }
    }
}
