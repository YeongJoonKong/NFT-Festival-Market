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
    public GameObject nftManager;
    public GameObject door;
    
    Animator anim;
    AudioSource audioSource;

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

        if (index == 1) 
        {
            NotifyObserver("WAIT_PLAYER_PURCHASE_TICKET_ANSWER");
            yield return new WaitUntil(() => !textBubble.activeInHierarchy);
        }

        if (index == 2)
        {
            StartCoroutine(makeWalletAndNFT());
        }

        NotifyObserver("PLAYER_ANSWER_PURCHASE_TICKET");
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < audioClips.Length)
        {
            StartCoroutine(Guide(index));
            
        } 
        else
        {
            index = 0;
            door.GetComponent<Door>().OpenBackDoor();
        }
    }


    public IEnumerator makeWalletAndNFT()
    {
        anim.SetTrigger("makeNFT");
        iPad.SetActive(true);

        System.Random random = new System.Random();
        int randomIndex = random.Next(1, 5);

        Debug.Log(randomIndex);
        TicketCache.randomIndex = randomIndex;
        string path = string.Format("Assets/07.Json/TicketInfo{0}.json", randomIndex);
        Debug.Log(path);
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
        {
            void SetAnimation() {
                anim.SetTrigger("Idle2");
                iPad.SetActive(false);
                NotifyObserver("MAKE_NFT_END");
            }

            nftManager.GetComponent<NFTManager>().RequestMakeWalletAndNFTTicket(SetAnimation);

            string filename = string.Format("TicketInfo{0}", randomIndex);
            var ticketInfoJson = LoadJsonFile<PurchaseTicketModel>("Assets/07.Json", filename);
            print(ticketInfoJson);

            PurchaseTicketModel walletAndTicketInfo = ticketInfoJson;
            WalletCache.id = walletAndTicketInfo.walletInfo.id;
            WalletCache.address = walletAndTicketInfo.walletInfo.address;
            WalletCache.walletType = walletAndTicketInfo.walletInfo.walletType;
            WalletCache.secretType = walletAndTicketInfo.walletInfo.secretType;
            WalletCache.createdAt = walletAndTicketInfo.walletInfo.createdAt;
            WalletCache.archived = walletAndTicketInfo.walletInfo.archived;
            WalletCache.description = walletAndTicketInfo.walletInfo.description;
            WalletCache.primary = walletAndTicketInfo.walletInfo.primary;
            WalletCache.hasCustomPin = walletAndTicketInfo.walletInfo.hasCustomPin;
            WalletCache.identifier = walletAndTicketInfo.walletInfo.identifier;
            WalletCache.balance = walletAndTicketInfo.walletInfo.balance;

            TicketCache.transactionHash = walletAndTicketInfo.ticketInfo.transactionHash;
            TicketCache.metadata = walletAndTicketInfo.ticketInfo.metadata;
            TicketCache.destinations = walletAndTicketInfo.ticketInfo.destinations;
            TicketCache.tokenIds = walletAndTicketInfo.ticketInfo.tokenIds;
            
            iPad.SetActive(false);
            anim.ResetTrigger("makeNFT");
            anim.SetTrigger("Idle2");

            NotifyObserver("MAKE_NFT_END");
        } 
        else
        {
            void SetAnimation() {
                anim.SetTrigger("Idle2");
                iPad.SetActive(false);
                NotifyObserver("MAKE_NFT_END");
            }

            nftManager.GetComponent<NFTManager>().RequestMakeWalletAndNFTTicket(SetAnimation);
            yield return "";
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
