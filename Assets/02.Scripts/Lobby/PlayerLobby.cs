using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobby : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(ActiveInventory());
    }

    void Update()
    {
    }
    
    IEnumerator ActiveInventory() {
        yield return new WaitUntil(() => OVRInput.Get(OVRInput.Button.One));
        if (gameObject.GetComponent<CharacterController>().enabled
            && Inventory.instance != null
            && Inventory.instance.Wallet != null) {
            if (OVRInput.GetDown(OVRInput.Button.One)) 
            {
                Inventory.instance.gameObject.SetActive(!Inventory.instance.gameObject.activeInHierarchy);
            }
        }
    }
}
