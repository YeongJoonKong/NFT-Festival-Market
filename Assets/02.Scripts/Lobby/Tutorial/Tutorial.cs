using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial: MonoBehaviour, ObserverLobby
{
    #region public variables
    
    public GameObject friendAvatar;
    public GameObject ticketGuideAvatar;
    public GameObject player;
    public GameObject locomotionController;
    public GameObject inputTutorialRoad;
    public GameObject leftJoyStickTutorial;
    public GameObject rightJoyStickTutorial;
    public GameObject indexTriggerTutorial;
    public GameObject purchaseTicketAnswer;
    public GameObject getNFTItemTutorial;
    public GameObject spotInFrontOfTicketGuideAvatarNPC;
    public GameObject nftTicket;
    public GameObject nftWallet;
    public GameObject npcConversationTutorial;
    public GameObject nftManager;
    public GameObject RayJoystickTutorial;
    
    #endregion

    #region private variables

    // NFTManager _nftManager = new NFTManager();
    int _getNFTItemCount;

    #endregion
    
    #region Life Cycle Method

    private void Awake() 
    {
        SubscribeGameObjectStatus();
    }

    private void Start()
    {
        StartPlayTutorial();
    }


    private void Update()
    {

    }

    #endregion

    #region Observing different gameObjects

    void SubscribeGameObjectStatus() 
    {
        friendAvatar.GetComponent<FriendAvatar>().AddObserver(this);
        inputTutorialRoad.GetComponentInChildren<DestinationZone>().AddObserver(this);
        ticketGuideAvatar.GetComponent<TicketGuideAvatar>().AddObserver(this);
        nftTicket.GetComponent<Ticket>().AddObserver(this);
        nftWallet.GetComponent<Wallet>().AddObserver(this);
    }


    public void DetectEvent(UnityEngine.Object obj)
    {
        if (obj is Ticket || obj is Wallet) 
        {
            _getNFTItemCount++;
            if (_getNFTItemCount == 2) 
            {
                TurnOffNFTWalletAndTicketTutorial();
            }
        }
    }

    public void DetectEvent(string _event) 
    {    
        if (_event.Equals("ACTIVE_KEY_TUTORIAL")) 
        {
            ActiveKeyTutorial();
        }
        else if (_event.Equals("PLAYER_ARRIVE_DESTINATION")) 
        {
            player.transform.LookAt(new Vector3(ticketGuideAvatar.transform.position.x, player.transform.position.y, ticketGuideAvatar.transform.position.z));
            // PlaySecondTutorial();
            PlayTempTutorial();
        }
        else if (_event.Equals("ACTIVE_PURCHASE_TICKET_TUTORIAL"))
        {
            player.transform.position = spotInFrontOfTicketGuideAvatarNPC.transform.position;
            player.transform.rotation = spotInFrontOfTicketGuideAvatarNPC.transform.rotation;
            PlayThirdTutorial();
        }
        else if (_event.Equals("WAIT_PLAYER_PURCHASE_TICKET_ANSWER"))
        {
            purchaseTicketAnswer.SetActive(true);
        }
        else if (_event.Equals("PLAYER_ANSWER_PURCHASE_TICKET"))
        {
            purchaseTicketAnswer.SetActive(false);
        }
        else if (_event.Equals("MAKE_NFT_END")) 
        {
            SpawnNFTWalletAndTicket();
            TurnOnNFTWalletAndTicketTutorial();
            StartCoroutine(PlayFinalTutorial());
        } 
        else if (_event.Equals("WAIT_PLAYER_TRY_CONVERSATION"))
        {
            TurnOnNPCConversationTutorial();
        }
        else if (_event.Equals("PLAYER_TRIED_CONVERSATION"))
        {
            TurnOffNPCConversationTutorial();
        }
    }

    #endregion

    #region Playing Tutorial Method
    void StartPlayTutorial() 
    {
        DeactiveKeyTutorial();
        PlayFirstTutorial();
    }

    void PlayFirstTutorial()
    {
        BanOVRInput();
        StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayScript(14, AllowOVRInput));
        ActiveKeyTutorial();
    }

    void PlayTempTutorial()
    {
        StartCoroutine(ticketGuideAvatar.GetComponent<TicketGuideAvatar>().Guide(0));
    }


    void PlaySecondTutorial()
    {
        BanOVRInput();
        DeactiveKeyTutorial();
        // StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlaySecondScript(6, AllowOVRInput));
    }


    void PlayThirdTutorial()
    {
        StartCoroutine(ticketGuideAvatar.GetComponent<TicketGuideAvatar>().Guide(0));
    }


    void PlayFourthTutorial()
    {
        Vector3 tr = new Vector3(friendAvatar.transform.position.x, player.transform.position.y, friendAvatar.transform.position.z);
        player.transform.LookAt(tr);
        // StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayFourthScript(9, AllowOVRInput));
    }

    IEnumerator PlayFinalTutorial() {
        yield return new WaitUntil(() => _getNFTItemCount == 2);
        PlayFourthTutorial();
    }

    #endregion

    #region Set active or deactive tutorial Objects

    void ActiveKeyTutorial()
    {
        TurnOnInputTutorialRoad();
        // TurnOnKeyTutorial();
    }


    void DeactiveKeyTutorial()
    {
        TurnOffInputTutorialRoad();
        // TurnOffKeyTutorial();
        TurnOffInteractionTutorial();
        TurnOffNFTWalletAndTicketTutorial();
        HideNFTWalletAndTicket();
        TurnOffNPCConversationTutorial();
    }


    void BanOVRInput()
    {
        player.GetComponent<CharacterController>().enabled = false;
        locomotionController.SetActive(false);
    }


    void AllowOVRInput()
    {
        player.GetComponent<CharacterController>().enabled = true;
        locomotionController.SetActive(true);
    }


    void TurnOnInputTutorialRoad()
    {
        inputTutorialRoad.SetActive(true);
    }


    void TurnOffInputTutorialRoad()
    {
        inputTutorialRoad.SetActive(false);
    }


    void TurnOnKeyTutorial()
    {
        IEnumerator SetActiveSlow() {
            GameObject[] tutorials = { leftJoyStickTutorial, rightJoyStickTutorial, indexTriggerTutorial };
            foreach (GameObject t in tutorials) 
            {
                t.SetActive(true);
                AudioSource audioSource = t.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);
                yield return new WaitForSeconds(0.7f);
            }
            AllowOVRInput();
        }

        StartCoroutine(SetActiveSlow());
    }


    void TurnOffKeyTutorial()
    {
        leftJoyStickTutorial.SetActive(false);
        rightJoyStickTutorial.SetActive(false);
        indexTriggerTutorial.SetActive(false);
    }


    void TurnOffInteractionTutorial() 
    {
        purchaseTicketAnswer.SetActive(false);
    }


    void TurnOffNFTWalletAndTicketTutorial() 
    {
        getNFTItemTutorial.SetActive(false);

    }

    void TurnOnNFTWalletAndTicketTutorial()
    {
        getNFTItemTutorial.SetActive(true);
    }


    void HideNFTWalletAndTicket() 
    {
        nftTicket.SetActive(false);
        nftWallet.SetActive(false);
    }


    void SpawnNFTWalletAndTicket() 
    {
        nftTicket.SetActive(true);
        nftWallet.SetActive(true);
    }

    void TurnOffNPCConversationTutorial()
    {
        npcConversationTutorial.SetActive(false);
    }

    void TurnOnNPCConversationTutorial()
    {
        npcConversationTutorial.SetActive(true);
    }

    #endregion

}
