using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTObject : MonoBehaviour
{
    public bool isGrabbed;
    public GameObject originalParent;
    public GameObject leftHandParent;
    public GameObject rightHandParent;
    
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    Rigidbody rb;

    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) && !OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
            ChangeOriginalParent();
        }
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
            rb.useGravity = false;
        }
    }

    public void ChangeRightHandParent() 
    {
        if (this.transform.parent != rightHandParent.transform) {
            this.transform.parent = rightHandParent.transform;
            rb.useGravity = false;
        }
    }

    public void ChangeOriginalParent() 
    {
        if (this.transform.parent != originalParent.transform) {
            this.transform.parent = originalParent.transform;
            // this.transform.position = originalPosition;
            // this.transform.rotation = originalRotation;
            isGrabbed = false;
            rb.useGravity = true;
        }
    }

}
