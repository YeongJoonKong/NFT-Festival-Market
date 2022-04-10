using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    // public static string BASE_URL = "https://vr-flea-market.herokuapp.com";
    public static string BASE_URL = "http://localhost:3000";
    public static string STORAGE_URL = "https://vr-flea-market.s3.ap-northeast-2.amazonaws.com";
    public static string TICKET_INFO_PATH = "Assets/07.Json/TicketInfo.json";
    public static string TICKET_NFT_NAME = "TicketInfo";
    public static string SAVE_JSON_PATH = "Assets/07.Json";
    public static string PURCHASE_NFT_TICKET_API_URL = "/users/purchase/ticket";
    public static string CREATE_WALLET_AND_CONTRACT = "/users/create/wallet";
    public static string CREATE_NFT_TICKET = "/users/create/ticket";
}
