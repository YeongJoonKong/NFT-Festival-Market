using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobby : MonoBehaviour
{

    void Start()
    {
        
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
        }
    }
    
}
