using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class NpcDialugeManager : MonoBehaviour
{
    string[] npcDialogue;
    string[ ]npcClearDialogue;
    string[ ]npcWorkShopDialogue;
    //AudioSource audios;
    TextMeshProUGUI textUi;
    AudioSource audios;
    Canvas talkBox;
    public GameObject workShopArea;


    private int npcNum;
    public int textNum;
    public int sectextnum;
    bool minigameplayed;

    // Start is called before the first frame update
void Start()
    {
        npcDialogue = GetComponentInChildren<Dialogue>().npctext;
        npcClearDialogue = GetComponentInChildren<Dialogue>().cleartext;
        npcWorkShopDialogue = GetComponentInChildren<Dialogue>().workShoptext;
        talkBox = GetComponentInChildren<Canvas>();

        audios = GetComponent<AudioSource>();
        textUi = GetComponentInChildren<TextMeshProUGUI>();
        if(gameObject.name == "Market_male_03")
        {
            npcNum = 0;
        }
        else if(gameObject.name == "Market_male_04")
        {
            npcNum = 1;
        }
        textNum = Playdata.instance.textnumData[npcNum];
        minigameplayed = Playdata.instance.minigameplayed[npcNum];
    }
    void Update()
    {
        if(workShopArea.transform.GetComponent<WorkShopArea>().startConversation)
        {
            workShopArea.transform.GetComponent<WorkShopArea>().startConversation = false;
            talkBox.enabled = true;
            Conversation();
        }

    }
public bool talk;
    public void Conversation()
    {
        if(talk == false)
        {
            return;
        }
        Playdata.instance.TextData(npcNum, textNum, minigameplayed);
        if(minigameplayed && workShopArea.transform.GetComponent<WorkShopArea>().inWorkshop)
        {
            talkBox.enabled = false;
        }
        else if(minigameplayed && !workShopArea.transform.GetComponent<WorkShopArea>().inWorkshop && workShopArea.transform.GetComponent<WorkShopArea>().leaveworkshop)
        {
            audios.Play();
            textUi.text = npcWorkShopDialogue[0];
            StartCoroutine(WaitTime(minigameplayed));
        }
        else if (minigameplayed && !workShopArea.transform.GetComponent<WorkShopArea>().inWorkshop)
        {
            if (sectextnum >= npcClearDialogue.Length - 1)
            {
                audios.Play();
                sectextnum = npcClearDialogue.Length - 1;
                textUi.text = npcClearDialogue[sectextnum];
            }
            else
            {
                audios.Play();
                textUi.text = npcClearDialogue[sectextnum];
                StartCoroutine(WaitTime(minigameplayed));
            }
        }
        else if(textNum >= npcDialogue.Length-1)
        {
            audios.Play();
            textNum = npcDialogue.Length - 1;
            textUi.text = npcDialogue[textNum];
        }
        else
        {
            audios.Play();
            textUi.text = npcDialogue[textNum];
            StartCoroutine(WaitTime(minigameplayed));
        }
    }

    IEnumerator WaitTime(bool played)
    {
        if(played)
        {
            sectextnum++;
        }
        else
        {
            textNum++;
        }
        yield return new WaitForSeconds(5f);
        Conversation();
    }
}
