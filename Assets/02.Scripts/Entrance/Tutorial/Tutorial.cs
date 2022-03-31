using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial: MonoBehaviour
{
    public GameObject friendAvatar;
    public GameObject ticketGuideAvatar;
    public GameObject player;
    public GameObject locomotionController;
    public GameObject inputTutorialRoad;
    public GameObject leftJoyStickTutorial;
    public GameObject rightJoyStickTutorial;
    public GameObject indexTriggerTutorial;

    bool isFirstTutorial;
    bool isSecondTutorial;
    bool isThirdTutorial;
    bool isFourthTutorial;

    private void Start()
    {
        DeactiveKeyTutorial();
    }


    private void Update()
    {
        PlayTutorial();
    }


    void PlayTutorial()
    {
        PlayFirstTutorial();
        ActiveKeyTutorial();

        PlaySecondTutorial();

        PlayThirdTutorial();

        PlayFourthTutorial();
    }


    void PlayFirstTutorial()
    {
        if (!isFirstTutorial)
        {
            isFirstTutorial = true;
            BanOVRInput();
            StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayFirstScript(0, AllowOVRInput));
        }
    }


    void PlaySecondTutorial()
    {
        if (isAllowOVRInput())
        {
            if (isFirstTutorial && !isSecondTutorial && DestinationZone.instance != null && DestinationZone.instance.isArrive)
            {
                isSecondTutorial = true;
                BanOVRInput();
                DeactiveKeyTutorial();
                StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlaySecondScript(6, AllowOVRInput));
            }
        }
    }


    void PlayThirdTutorial()
    {
        if (isAllowOVRInput() && isFirstTutorial && isSecondTutorial && !isThirdTutorial)
        {
            if (OVRInput.Get(OVRInput.Button.Two))
            {
                isThirdTutorial = true;
                BanOVRInput();
                StartCoroutine(ticketGuideAvatar.GetComponent<TicketGuideAvatar>().Guide(0, AllowOVRInput));
            }
        }
    }


    void PlayFourthTutorial()
    {
        if (isAllowOVRInput() && isFirstTutorial && isSecondTutorial && isThirdTutorial && !isFourthTutorial)
        {
            isFourthTutorial = true;
            BanOVRInput();
            StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayThirdScript(9, AllowOVRInput));
        }
    }


    void ActiveKeyTutorial()
    {
        if (isAllowOVRInput() && isFirstTutorial && !isSecondTutorial)
        {
            TurnOnInputTutorialRoad();
            TurnOnKeyTutorial();
        }
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
    }


    bool isAllowOVRInput()
    {
        return player.GetComponent<CharacterController>().enabled;
    }
}
