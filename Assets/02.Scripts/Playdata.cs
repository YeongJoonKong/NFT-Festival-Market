using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Playdata : MonoBehaviour
{
    public static Playdata instance;

    // Start is called before the first frame update
    [Header("Npc First text Number")]
    public int[] textnumData;
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

    public void TextData(int npcnum, int textnum)
    {
        if(npcnum == 0)
        {
            textnumData[0] = textnum;
        }
        else if(npcnum == 1)
        {
            textnumData[1] = textnum;
        }
    }
}
