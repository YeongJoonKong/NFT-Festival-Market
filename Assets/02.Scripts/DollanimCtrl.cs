using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollanimCtrl : MonoBehaviour
{
    [Range (0,1)]
    public float dollstartoffset;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();


        anim.Play("Idle", 0, dollstartoffset);
    }

    // Update is called once per frame
   
}
