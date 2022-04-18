using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Playdata : MonoBehaviour
{
    public static Playdata instance;
    public bool[] minigameplayed;

    // Start is called before the first frame update
    [Header("Npc First text Number")]
    public int[] textnumData;

    [Header("Player SpawnPoints")]
    public int spawnPointData = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void TextData(int npcnum, int textnum, bool minigame)
    {
        if(npcnum == 0)
        {
            textnumData[0] = textnum;
            minigameplayed[0] = minigame;

        }
        else if(npcnum == 1)
        {
            textnumData[1] = textnum;
            minigameplayed[1] = minigame;

        }
    }

}
