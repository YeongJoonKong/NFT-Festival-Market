using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial: MonoBehaviour
{
    public static Tutorial instance = null;
    public GameObject friendAvatar;
    public GameObject ticketGuideAvatar;
    public GameObject player;
    public GameObject locomotionController;
    public GameObject inputTutorialRoad;

    private void Start()
    {
        instance = this;
        TurnOffInputTutorialRoad();
        PlayInOrder();    
    }

    public void PlayInOrder()
    {
        FirstGuide();
    }

    void FirstGuide()
    {
        BanOVRInput();
        StartCoroutine(friendAvatar.GetComponent<FriendAvatar>().PlayFirstScript(0, AllowOVRInput));
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

    public void TurnOnInputTutorialRoad()
    {
        inputTutorialRoad.SetActive(true);
    }

    public void TurnOffInputTutorialRoad()
    {
        inputTutorialRoad.SetActive(false);
    }
}
