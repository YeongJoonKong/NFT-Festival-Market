using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public GameObject effect;
    public GameObject spawnPosition;
    public TextMeshPro infoText;
    public TextMeshPro buyText;
    public GameObject yesBubble;
    public GameObject noBubble;

    GameObject[] displayItems;

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

    void CheckCoin() 
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
            buyText.text = "-";
            buyText.enabled = false;
            yesBubble.SetActive(false);
            noBubble.SetActive(false);
            infoText.enabled = true;
            effect.SetActive(true);
        }

    }
}
