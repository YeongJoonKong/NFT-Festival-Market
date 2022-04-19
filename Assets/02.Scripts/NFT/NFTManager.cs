using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class NFTManager : MonoBehaviour
{
    public GameObject nftTicket;
    
    void Start()
    {

    }

    
    void Update()
    {

    }

    public void RequestMakeWalletAndNFTTicket(Action callback) {
        StartCoroutine(MakeWalletAndNFTTicket(callback));
    }

    public object ReadTicketInfoJsonFile() {
        FileInfo fi = new FileInfo(Constant.TICKET_INFO_PATH);
        if (fi.Exists) {
            var ticketInfoJson = LoadJsonFile<PurchaseTicketModel>(Constant.TICKET_INFO_PATH);
            return ticketInfoJson;
        }
        return "";
    }

    public object ReadTicketInfoJsonFile(string key) {
        FileInfo fi = new FileInfo(Constant.TICKET_INFO_PATH);
        if (fi.Exists) {
            var ticketInfoJson = LoadJsonFile<PurchaseTicketModel>(Constant.TICKET_INFO_PATH);
        }
        return "";
    }

    IEnumerator MakeWalletAndNFTTicket(Action callback) {
        nftTicket.SetActive(true);
        GameObject[] ticketNFTObjects = GameObject.FindGameObjectsWithTag("TicketRandomNFT");

        int randomIndex = UnityEngine.Random.Range(0, ticketNFTObjects.Length);
        for (int i = 0; i < ticketNFTObjects.Length; i++)
        {
            if (i == randomIndex)
            {
                if (ticketNFTObjects[i].name.Contains("Apple"))
                {
                    nftTicket.GetComponentInChildren<TextMeshPro>().text = "NFT 입장권\n사과 VER.";
                }
                else if (ticketNFTObjects[i].name.Contains("Pumpkin"))
                {
                    nftTicket.GetComponentInChildren<TextMeshPro>().text = "NFT 입장권\n호박 VER.";
                }
                else if (ticketNFTObjects[i].name.Contains("Cheese"))
                {
                    nftTicket.GetComponentInChildren<TextMeshPro>().text = "NFT 입장권\n치즈케익 VER.";
                }
                else if (ticketNFTObjects[i].name.Contains("Carrot"))
                {
                    nftTicket.GetComponentInChildren<TextMeshPro>().text = "NFT 입장권\n당근 VER.";
                }
            }
            else
            {
                ticketNFTObjects[i].SetActive(false);
            }
        }

        GameObject particleSystem = nftTicket.transform.Find("Particle System").gameObject;
        particleSystem.SetActive(false);

        CreateNFTTicketPrefab(nftTicket);

        particleSystem.SetActive(true);

        nftTicket.SetActive(false);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", File.ReadAllBytes("Assets/Resources/NFT/Ticket.prefab"), "Ticket.prefab");

        using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + Constant.CREATE_WALLET_AND_CONTRACT, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success){
                MakeFailedResponse();
            } else {
                UnityWebRequest request1 = new UnityWebRequest(Constant.BASE_URL + Constant.CREATE_NFT_TICKET, "POST");
                byte[] encodedRequest = new System.Text.UTF8Encoding().GetBytes(request.downloadHandler.text);
                request1.uploadHandler = (UploadHandler)new UploadHandlerRaw(encodedRequest);
                request1.downloadHandler = new DownloadHandlerBuffer();
                request1.SetRequestHeader("Content-Type", "application/json");

                yield return request1.SendWebRequest();

                if (request1.result != UnityWebRequest.Result.Success) {

                } else {
                    CreateJsonFile(Constant.SAVE_JSON_PATH, "TicketInfo", request1.downloadHandler.text);
                    MakeSuccessResponse(request1.downloadHandler.text);
                    if (callback != null) callback();
                }

            }
        }
    }


    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }


    T LoadJsonFile<T>(string loadPath)
    {
        FileStream fileStream = new FileStream(string.Format("{0}", loadPath), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public void CreateNFTTicketPrefab(GameObject ticketPrefab)
    {
        ticketPrefab.transform.position = new Vector3(0, 0, 0);
        ticketPrefab.transform.rotation = Quaternion.Euler(0, 180, 0);
        ticketPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        string localPath = "Assets/Resources/NFT/Ticket.prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        bool prefabSuccess;
        PrefabUtility.SaveAsPrefabAsset(ticketPrefab, localPath, out prefabSuccess);
        if (prefabSuccess) {
            Debug.Log("Prefab was saved successfully");
        } else {
            Debug.Log("Prefab failed to save " + prefabSuccess);
        }
        ticketPrefab.transform.rotation = Quaternion.Euler(0, 90, 0);
        ticketPrefab.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void MakeFailedResponse()
    {

    }

    public void MakeSuccessResponse(string text)
    {
        PurchaseTicketModel walletAndTicketInfo = JsonConvert.DeserializeObject<PurchaseTicketModel>(text);
        WalletCache.id = walletAndTicketInfo.walletInfo.id;
        WalletCache.address = walletAndTicketInfo.walletInfo.address;
        WalletCache.walletType = walletAndTicketInfo.walletInfo.walletType;
        WalletCache.secretType = walletAndTicketInfo.walletInfo.secretType;
        WalletCache.createdAt = walletAndTicketInfo.walletInfo.createdAt;
        WalletCache.archived = walletAndTicketInfo.walletInfo.archived;
        WalletCache.description = walletAndTicketInfo.walletInfo.description;
        WalletCache.primary = walletAndTicketInfo.walletInfo.primary;
        WalletCache.hasCustomPin = walletAndTicketInfo.walletInfo.hasCustomPin;
        WalletCache.identifier = walletAndTicketInfo.walletInfo.identifier;
        WalletCache.balance = walletAndTicketInfo.walletInfo.balance;

        TicketCache.transactionHash = walletAndTicketInfo.ticketInfo.transactionHash;
        TicketCache.metadata = walletAndTicketInfo.ticketInfo.metadata;
        TicketCache.destinations = walletAndTicketInfo.ticketInfo.destinations;
        TicketCache.tokenIds = walletAndTicketInfo.ticketInfo.tokenIds;
    }
}
