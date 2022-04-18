using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        canvas = transform.Find("Canvas").gameObject;
    }
}
