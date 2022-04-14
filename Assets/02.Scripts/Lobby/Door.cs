using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public GameObject leftDoor;
    public GameObject rightDoor;

    bool _isStartOpen;

    void Start()
    {
        
    }

    void Update()
    {
        if (_isStartOpen) 
        {
            if (leftDoor.transform.localPosition.x >= -5.81f)
                leftDoor.transform.localPosition += new Vector3(-0.1f, 0, 0);
            if (rightDoor.transform.localPosition.x <= 5.81f)
                rightDoor.transform.localPosition += new Vector3(0.1f, 0, 0);
        }
    }

    public void OpenDoor() 
    {
        _isStartOpen = true;
    }
}
