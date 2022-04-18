using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Newtonsoft.Json;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;
    public TextMeshPro walletInfo;
    public GameObject activeNFT;

    GameObject _wallet = null;
    GameObject _ticket = null;
    GameObject[] _NFT;

    public GameObject Wallet {
        get { return _wallet; }
        set { this._wallet = value; }
    }

    public GameObject Ticket {
        get { return _ticket; }
        set { this._ticket = value; }
    }

    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    void Update()
    {
       
    }

    public void SetWalletInfo() 
    {
        if (gameObject.activeInHierarchy) 
        {
            if (WalletCache.address != null) {
                walletInfo.text = "-지갑 주소-\n" + WalletCache.address + "\n" + "-코인 종류-\n" + WalletCache.secretType + "\n" + "-기타 정보-\n" + WalletCache.description;
            }

            GameObject[] nfts = Resources.LoadAll<GameObject>("NFT");
            if (nfts[0].name.Contains("Ticket")) {
                nfts[0].GetComponent<Animation>().enabled = false;
                nfts[0].GetComponent<Animator>().enabled = false;
                Instantiate(nfts[0], activeNFT.transform);
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)) 
            {
                for (int i = 0; i < nfts.Length; i++) {
                    if (nfts[i].name.Contains("Ticket")) {
                        nfts[i].GetComponent<Animation>().enabled = false;
                        nfts[i].GetComponent<Animator>().enabled = false;
                        Instantiate(nfts[i], activeNFT.transform);
                    }
                }
            }
        }
    }
}
