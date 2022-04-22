using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public GameObject effect;
    public GameObject spawnPosition;
    public TextMeshPro infoText;
    public GameObject buyText;

    // Start is called before the first frame update
    void Start()
    {
        buyText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "DISPLAY_ITEM") 
        {
            // Instantiate(other.gameObject, other.gameObject.GetComponent<NFTObject>().originalPosition, other.gameObject.GetComponent<NFTObject>().originalRotation);
            other.gameObject.transform.position = spawnPosition.transform.position;
            effect.SetActive(false);
            infoText.enabled = false;
        }
    }
}
