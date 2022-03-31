using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationZone : MonoBehaviour
{
    public static DestinationZone instance = null;
    public bool isArrive;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isArrive = true;
    }
}
