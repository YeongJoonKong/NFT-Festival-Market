using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial: MonoBehaviour, ObserverLobby
{
    public GameObject friendAvatar;
    public GameObject ticketGuideAvatar;
    public GameObject player;
    public GameObject locomotionController;
    public GameObject inputTutorialRoad;
    public GameObject leftJoyStickTutorial;
    public GameObject rightJoyStickTutorial;
    public GameObject indexTriggerTutorial;
    public GameObject shakeHandTutorial;
    public GameObject purchaseTicketAnswer;
    public GameObject spotInFrontOfTicketGuideAvatarNPC;

    private void Start()
    {
        DeactiveKeyTutorial();
        friendAvatar.GetComponent<FriendAvatar>().AddObserver(this);
        inputTutorialRoad.GetComponentInChildren<DestinationZone>().AddObserver(this);
        ticketGuideAvatar.GetComponent<TicketGuideAvatar>().AddObserver(this);
        PlayFirstTutorial();
    }


    private void Update()
    {

    }

    void PlayFirstTutorial()
    {
        BanOVRInput();
        StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayFirstScript(0, AllowOVRInput));
    }


    void PlaySecondTutorial()
    {
        BanOVRInput();
        DeactiveKeyTutorial();
        player.transform.LookAt(new Vector3(friendAvatar.transform.position.x, player.transform.position.y, friendAvatar.transform.position.z));
        StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlaySecondScript(6, AllowOVRInput));
    }


    void PlayThirdTutorial()
    {
        BanOVRInput();
        StartCoroutine(ticketGuideAvatar.GetComponent<TicketGuideAvatar>().Guide(0, AllowOVRInput));
    }


    void PlayFourthTutorial()
    {
        BanOVRInput();
        StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayFourthScript(9, AllowOVRInput));
    }


    void ActiveKeyTutorial()
    {
        TurnOnInputTutorialRoad();
        TurnOnKeyTutorial();
    }


    void DeactiveKeyTutorial()
    {
        TurnOffInputTutorialRoad();
        TurnOffKeyTutorial();
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
        leftJoyStickTutorial.SetActive(true);
        rightJoyStickTutorial.SetActive(true);
        indexTriggerTutorial.SetActive(true);
    }


    void TurnOffKeyTutorial()
    {
        leftJoyStickTutorial.SetActive(false);
        rightJoyStickTutorial.SetActive(false);
        indexTriggerTutorial.SetActive(false);
        shakeHandTutorial.SetActive(false);
        purchaseTicketAnswer.SetActive(false);
    }


    bool isAllowOVRInput()
    {
        return player.GetComponent<CharacterController>().enabled;
    }

    public void DetectEvent(UnityEngine.Object obj)
    {
        if (obj is FriendAvatar)
        {
            var friendAvatar = obj as FriendAvatar;
            int endScript = friendAvatar.GetCurrentScriptEnd();
            bool isPlayerHandShake = friendAvatar.GetIsPlayerHandShake();

            if (!isPlayerHandShake)
            {
                shakeHandTutorial.SetActive(true);
            } 
            else if (isPlayerHandShake) 
            {
                shakeHandTutorial.SetActive(false);
            }

            if (endScript == 1)
            {
                ActiveKeyTutorial();
            }
            else if (endScript == 2)
            {
                player.transform.position = spotInFrontOfTicketGuideAvatarNPC.transform.position;
                player.transform.rotation = spotInFrontOfTicketGuideAvatarNPC.transform.rotation;
                PlayThirdTutorial();
            } 
        }
        else if (obj is TicketGuideAvatar) 
        {
            var ticketGuideAvatar = obj as TicketGuideAvatar;
            bool isScriptEnd = ticketGuideAvatar.GetIsScriptEnd();
            bool waitAnswer = ticketGuideAvatar.GetWaitAnswer();
            if (waitAnswer) {
                purchaseTicketAnswer.SetActive(true);
            }
            else if (!waitAnswer) {
                purchaseTicketAnswer.SetActive(false);
            }

            if (isScriptEnd)
            {
                player.transform.LookAt(new Vector3(friendAvatar.transform.position.x, player.transform.position.y, friendAvatar.transform.position.z));
                PlayFourthTutorial();
            }

        }
        else if (obj is DestinationZone) 
        {
            var destinationZone = obj as DestinationZone;
            bool isArrive = destinationZone.GetIsArrive();
            if (isArrive) {
                PlaySecondTutorial();
            }
        }
    }
}
