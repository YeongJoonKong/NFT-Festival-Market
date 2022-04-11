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
    public int textNum;
    private int npcNum;
    // Start is called before the first frame update
void Start()
    {
        audios = GetComponent<AudioSource>();
        npcDialogue = GetComponentInChildren<Dialogue>().npctext;
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
    }


    public void Conversation()
    {
        audios.Play();
        Playdata.instance.TextData(npcNum, textNum);
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
