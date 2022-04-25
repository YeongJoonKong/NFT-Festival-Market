using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Userhigh : MonoBehaviour
{

    public float userhigh;
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0, (userhigh - 154f)/100, 0); ;
    }

    // Update is called once per frame
    
}
