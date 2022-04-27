using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NFTObject : MonoBehaviour
{
    public bool isGrabbed;
    public GameObject originalParent;
    public GameObject leftHandParent;
    public GameObject rightHandParent;
    
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    public GameObject CounterSpawnPoint;
    public GameObject Case;
    public TextMeshProUGUI CaseText;

    Vector3 beforePosition;

    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
        Case.SetActive(false);
    }

    void Update()
    {
        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && !OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
            ChangeOriginalParent();
        }
        
        if (!isGrabbed) {
            if (beforePosition == transform.position) {
                if (gameObject.GetComponent<Rigidbody>() != null) {
                    Destroy(gameObject.GetComponent<Rigidbody>());
                }

                if (Vector3.Distance(CounterSpawnPoint.transform.position, transform.position) <= 1f) {
                    transform.position = CounterSpawnPoint.transform.position;
                    transform.rotation = CounterSpawnPoint.transform.rotation;
                } else {
                    transform.position = originalPosition;
                    transform.rotation = originalRotation;
                }
            }

            if (originalPosition == transform.position) {
                Case.SetActive(false);
            }
        }
        beforePosition = gameObject.transform.position;
    }
    
    void OnTriggerEnter(Collider otherCollider) 
    {

    }
    
    void OnTriggerStay(Collider otherCollider)
    {
       if(otherCollider.gameObject.name.Contains("GrabVolumeBig")) 
       {
           isGrabbed = true;
       }
    }

    void OnTriggerExit(Collider otherCollider)
    {
        isGrabbed = false;
    }

    public void ChangeLeftHandParent() 
    {
        if (this.transform.parent != leftHandParent.transform) {
            this.transform.parent = leftHandParent.transform;
        }
    }

    public void ChangeRightHandParent() 
    {
        if (this.transform.parent != rightHandParent.transform) {
            this.transform.parent = rightHandParent.transform;
        }
    }

    public void ChangeOriginalParent() 
    {
        try {
            if (this.transform.parent != originalParent.transform) {
                this.transform.parent = originalParent.transform;
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.useGravity = true;
                isGrabbed = false;
            }
        } catch (UnassignedReferenceException e) {

        }
    }
    
    public int GetObjectPrice()
    {
        TextMeshPro[] infos = gameObject.GetComponentsInChildren<TextMeshPro>();
        foreach (TextMeshPro info in infos) {
            if (info.text.Contains("Coin")) {
                int price = Int32.Parse(info.text.Replace(" Coin", ""));
                return price;
            }
        }
        return 0;
    }

}
