using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaglidingCloud : MonoBehaviour
{
    public float kAdujst = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 direction = new Vector3(transform.forward.x, -fallingForce, transform.forward.z);
        transform.Rotate(new Vector3(0, 0, kAdujst * Time.deltaTime));
    }
}
