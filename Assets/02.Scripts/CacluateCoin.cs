using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CacluateCoin : MonoBehaviour
{
    Transform[] allChildren;
    // Start is called before the first frame update
    void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();
        foreach(Transform child in allChildren) 
        {
            if (child.gameObject.tag == "DISPLAY_ITEM")
            {
                TextMeshPro[] txt = child.gameObject.GetComponentsInChildren<TextMeshPro>();
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i].text.Contains("Coin"))
                    {
                        double price = Double.Parse(txt[i].text.Replace(" Coin", ""));
                        price *= 0.0001;
                        txt[i].text = price.ToString() + " MATIC";
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
