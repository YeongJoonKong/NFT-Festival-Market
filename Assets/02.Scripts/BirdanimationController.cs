using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdanimationController : MonoBehaviour
{
    [Range(0,1)]
    public float animoffset;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("IdleA", 0, animoffset);
    }

    // Update is called once per frame
  
}
