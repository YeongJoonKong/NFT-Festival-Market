using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcDialugeManager : MonoBehaviour
{
    string[] npcDialogue;
    //AudioSource audios;
    TextMeshProUGUI textUi;
    AudioSource audios;
    int textNum = 0;

    bool Played;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        npcDialogue = GetComponentInChildren<Dialogue>().npctext;
        textUi = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void Conversation()
    {
        audios.Play();
        if(textNum >= npcDialogue.Length-1)
        {
            textNum = npcDialogue.Length-1;
            textUi.text = npcDialogue[textNum];
        }
        else
        {
            textUi.text = npcDialogue[textNum];
            StartCoroutine("WaitTime");
        }
    }

    IEnumerator WaitTime()
    {
        textNum++;
        yield return new WaitForSeconds(5f);
        Conversation();
    }
}
