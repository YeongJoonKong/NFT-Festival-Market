using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunHand : MonoBehaviour
{

    [Header("OCULUS")]
    public OVRInput.Controller controller;

    LineRenderer lineRenderer;

    public WaterGun waterGun;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonDown();
        LineRendering();
    }

    void ButtonDown()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            waterGun.waterShooting();
        }
    }

    void LineRendering()
    {
        Ray ray = new Ray(transform.position + new Vector3(0,  - 0.5f, 0), transform.forward);
        RaycastHit raycastHit;
        lineRenderer.SetPosition(0, transform.position);

        bool isHit = Physics.Raycast(ray, out raycastHit);

        if(isHit)
        {
            lineRenderer.SetPosition(1, raycastHit.point);
            print(raycastHit.transform.gameObject);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * 50);
        }
    }

}
