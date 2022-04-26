using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameCounter : MonoBehaviour
{
    private float deltatime = 0f;

    

    [SerializeField]
    private Color color = Color.black;


    public TextMeshPro tmp;
    public TextMeshProUGUI tmp2;
    //public bool isshow;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        deltatime += (Time.unscaledDeltaTime - deltatime) * 0.1f;

        OnGUI();
    }

    public void OnGUI()
    {
        float ms = deltatime * 1000f;
        float fps = 1f / deltatime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        //tmp.text = text;
        tmp2.text = text;
    }
}
