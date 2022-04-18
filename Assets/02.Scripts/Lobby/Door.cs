using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject player;

    bool _isStartOpen;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OpenDoor() 
    {
        // _isStartOpen = true;
        if (Vector3.Distance(player.transform.position, transform.position) <= 1f) {
            leftDoor.transform.localPosition += new Vector3(-0.1f, 0, 0);
            rightDoor.transform.localPosition += new Vector3(0.1f, 0, 0);
        }
    }
}
