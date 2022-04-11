using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobby : MonoBehaviour
{

    public GameObject leftHand;
    public GameObject rightHand;

    GameObject[] displayItem;

    void Start()
    {
        displayItem = GameObject.FindGameObjectsWithTag("DISPLAY_ITEM");
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) {
            if (gameObject.GetComponent<CharacterController>().enabled
                && Inventory.instance != null
                && Inventory.instance.Wallet != null) {
                    Inventory.instance.gameObject.SetActive(!Inventory.instance.gameObject.activeInHierarchy);
                    Inventory.instance.SetWalletInfo();
                }
        } else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) {
            foreach (GameObject dItem in displayItem) {
                NFTObject obj = dItem.GetComponent<NFTObject>();
                if (obj.isGrabbed) {
                    obj.ChangeLeftHandParent();
                }
            }
        } else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
            foreach (GameObject dItem in displayItem) {
                NFTObject obj = dItem.GetComponent<NFTObject>();
                if (obj.isGrabbed) {
                    obj.ChangeRightHandParent();
                }
            }
        }
    }
    
}
