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
    int index;

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
       SetWalletInfo();
    }

    public void SetWalletInfo() 
    {
        if (gameObject.activeInHierarchy) 
        {
            if (WalletCache.address != null) {
                walletInfo.text = "-지갑 주소-\n" + WalletCache.address + "\n" + "-코인 종류-\n" + WalletCache.secretType + "\n" + "-잔액-\n" + CoinCache.coin + WalletCache.secretType;
            }

            GameObject[] nfts = Resources.LoadAll<GameObject>("NFT");

            if (OVRInput.GetDown(OVRInput.Button.Four)) 
            {
                // Destroy(nfts[index]);
                if (index + 1 == nfts.Length)
                {
                    index = 0;
                }
                else
                {
                    index += 1;
                }
                int childrenLength = activeNFT.transform.childCount;
                Debug.Log(childrenLength);
                if (childrenLength > 0) 
                {
                    for (int i = 0; i < childrenLength; i++)
                    {
                        Destroy(activeNFT.transform.GetChild(i).gameObject);
                    }
                }
                GameObject myNFT = Instantiate(nfts[index], activeNFT.transform, false);
                myNFT.transform.position = activeNFT.transform.position;
                myNFT.transform.rotation = activeNFT.transform.rotation;
            }
        }
    }
}
