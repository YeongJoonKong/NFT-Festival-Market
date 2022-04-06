using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialugeManager : MonoBehaviour
{
    string[] npcDialogue;
    TextMeshProUGUI textUi;
    int textNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        npcDialogue = GetComponentInChildren<Dialogue>().npctext;
        textUi = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnConversation()
    {   
        if(textNum > npcDialogue.Length)
        {
            textNum = 0;
        }
        else
        {
            textUi.text = npcDialogue[textNum];
            textNum++;

        }
    }
}
