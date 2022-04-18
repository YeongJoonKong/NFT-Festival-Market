using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkShopArea : MonoBehaviour
{
    public bool inWorkshop;
    public bool leaveworkshop;
    public bool startConversation;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inWorkshop = true;
        }
    }
    public void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            inWorkshop = false;
            leaveworkshop = true;

            startConversation = true;
            StartCoroutine(LeavingWorkShop());
        }
        
    }
    IEnumerator LeavingWorkShop()
    {
        yield return new WaitForSeconds(5f);
        leaveworkshop = false;
    }
}
