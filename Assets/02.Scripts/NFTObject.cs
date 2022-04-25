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

    public GameObject CounterSpawnPoint;

    Vector3 beforePosition;

    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
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
        if (this.transform.parent != originalParent.transform) {
            this.transform.parent = originalParent.transform;
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            // this.transform.position = originalPosition;
            // this.transform.rotation = originalRotation;
            isGrabbed = false;
        }
    }

}
