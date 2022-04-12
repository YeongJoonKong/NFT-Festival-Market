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
    //AudioSource audios;
    TextMeshProUGUI textUi;
    AudioSource audios;
    public int textNum;
    public int sectextnum;
    private int npcNum;

    bool minigameplayed;
    // Start is called before the first frame update
void Start()
    {
        audios = GetComponent<AudioSource>();
        npcDialogue = GetComponentInChildren<Dialogue>().npctext;
        npcClearDialogue = GetComponentInChildren<Dialogue>().cleartext;
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


    public void Conversation()
    {
        audios.Play();
        Playdata.instance.TextData(npcNum, textNum, minigameplayed);
        if (minigameplayed)
        {
            if (sectextnum >= npcClearDialogue.Length - 1)
            {
                sectextnum = npcClearDialogue.Length - 1;
                textUi.text = npcClearDialogue[sectextnum];
            }
            else
            {
                textUi.text = npcClearDialogue[sectextnum];
                StartCoroutine(WaitTime(minigameplayed));
            }
        }
        else if(textNum >= npcDialogue.Length-1)
        {
            textNum = npcDialogue.Length - 1;
            textUi.text = npcDialogue[textNum];
        }
        else
        {
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
