using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunHand : MonoBehaviour
{

    [Header("OCULUS")]
    public OVRInput.Controller controller;

    public WaterGun waterGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonDown();
    }

    void ButtonDown()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            print(03);
            waterGun.waterShooting();
        }
    }
}
