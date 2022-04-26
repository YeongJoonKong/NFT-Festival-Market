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
        if (_isStartOpen) 
        {
            OpenBackDoor();
            if (leftDoor.transform.localPosition.x <= -5.81f && rightDoor.transform.localPosition.x >= 5.81f) {
                _isStartOpen = false;
            }
        }
    }

    public void OpenDoor() 
    {
        // _isStartOpen = true;
        if (Vector3.Distance(player.transform.position, transform.position) <= 3f) {
            leftDoor.transform.localPosition += new Vector3(-0.1f, 0, 0);
            rightDoor.transform.localPosition += new Vector3(0.1f, 0, 0);
        }
    }

    public void OpenBackDoor()
    {
        _isStartOpen = true;
        leftDoor.transform.localPosition += new Vector3(-0.1f, 0, 0);
        rightDoor.transform.localPosition += new Vector3(0.1f, 0, 0);
    }
}
