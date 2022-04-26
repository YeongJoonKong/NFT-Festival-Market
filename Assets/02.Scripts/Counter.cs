using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Counter : MonoBehaviour
{
    public GameObject effect;
    public GameObject spawnPosition;
    public TextMeshPro infoText;
    public TextMeshPro buyText;
    public GameObject yesBubble;
    public GameObject noBubble;
    public GameObject nftManager;
    GameObject[] displayItems;
    public bool isMakingNFT;

    // Start is called before the first frame update
    void Start()
    {
        buyText.enabled = false;
        yesBubble.SetActive(false);
        noBubble.SetActive(false);
        displayItems = GameObject.FindGameObjectsWithTag("DISPLAY_ITEM");
    }

    // Update is called once per frame
    void Update()
    {
        CheckCoin();
    }

    private void OnCollisionEnter(Collision other) 
    {

    }

    void CheckCoin() 
    {
        if (!isMakingNFT) 
        {
            int count = 0;
            foreach (GameObject item in displayItems)
            {
                if (item.transform.position == spawnPosition.transform.position)
                {
                    count += 1;
                    // isSpawnItem = true;
                    int price = item.GetComponent<NFTObject>().GetObjectPrice();
                    infoText.enabled = false;
                    effect.SetActive(false);
                    CoinCache.coin = 100;
                    if (price > CoinCache.coin)
                    {
                        buyText.text = "코인이 부족합니다!\n미니게임으로 돈을 벌어보세요!";
                        buyText.enabled = true;
                    }
                    else
                    {
                        buyText.text = "구매가 가능합니다!\nNFT로 만들어볼까요?";
                        buyText.enabled = true;
                        yesBubble.SetActive(true);
                        noBubble.SetActive(true);
                    }
                    break;
                }

            }

            if (count == 0)
            {
                restoreCounterEffect();
            }
        }

    }

    public void makeNFT()
    {
        isMakingNFT = true;
        buyText.enabled = false;
        yesBubble.SetActive(false);
        noBubble.SetActive(false);
        infoText.enabled = false;
        effect.SetActive(false);

        foreach (GameObject item in displayItems)
        {
            if (item.transform.position == spawnPosition.transform.position)
            {
                if (item.GetComponent<NFTObject>().Case != null) {
                    string date = DateTime.Now.ToString("dd-MM-yyyy");
                    item.GetComponent<NFTObject>().CaseText.text = date;
                    item.GetComponent<NFTObject>().Case.SetActive(true);
                }
                nftManager.GetComponent<NFTManager>().CreateNFTObjectPrefab(item);
                buyText.text = "NFT가 제작되었습니다!";
                yesBubble.GetComponentInChildren<TextMeshPro>().text = "감사합니다!";
                buyText.enabled = true;
                yesBubble.SetActive(true);
                break;
            }

        }
    }

    public void restoreCounterEffect()
    {
        buyText.text = "-";
        buyText.enabled = false;
        yesBubble.SetActive(false);
        noBubble.SetActive(false);
        infoText.enabled = true;
        effect.SetActive(true);
    }

    public void restoreCounterEffect(GameObject nftObject)
    {
        buyText.text = "-";
        buyText.enabled = false;
        yesBubble.SetActive(false);
        noBubble.SetActive(false);
        infoText.enabled = true;
        effect.SetActive(true);
        nftObject.transform.position = nftObject.GetComponent<NFTObject>().originalPosition;
        nftObject.transform.rotation = nftObject.GetComponent<NFTObject>().originalRotation;
    }
}
