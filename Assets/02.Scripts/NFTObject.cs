using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTObject : MonoBehaviour
{
    public bool isGrabbed;
    public GameObject originalParent;
    public GameObject leftHandParent;
    public GameObject rightHandParent;

    public Dictionary<string, string> info;
    
    Vector3 originalPosition;
    Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
    }

    // Update is called once per frame
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
       if(otherCollider.gameObject.name.Contains("GrabVolumeBig")) {
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
            this.transform.position = originalPosition;
            this.transform.rotation = originalRotation;
            isGrabbed = false;
        }
    }
}
