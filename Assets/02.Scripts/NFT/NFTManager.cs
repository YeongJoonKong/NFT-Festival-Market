using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class NFTManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestMakeWalletAndNFTTicket() {
        StartCoroutine(MakeWalletAndNFTTicket());
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
            if (key.Equals("transactionHash")) {
                return ticketInfoJson.transactionHash;
            } else if (key.Equals("metadata")) {
                return ticketInfoJson.metadata;
            } else if (key.Equals("destinations") || key.Equals("walletAddress")) {
                return ticketInfoJson.destinations;
            } else if (key.Equals("tokenIds")) {
                return ticketInfoJson.tokenIds;
            } else {
                return "";
            }
        }
        return "";
    }

    IEnumerator MakeWalletAndNFTTicket() {
        using (UnityWebRequest request = UnityWebRequest.Post(Constant.BASE_URL + Constant.PURCHASE_NFT_TICKET_API_URL, ""))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success){
                    MakeFailedResponse();
                } else {
                    CreateJsonFile(Constant.SAVE_JSON_PATH, "TicketInfo", request.downloadHandler.text);
                    MakeSuccessResponse();
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

    public void MakeFailedResponse() {

    }

    public void MakeSuccessResponse() {
        
    }
}
