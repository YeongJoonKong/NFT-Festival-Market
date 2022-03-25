using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PurchaseTicketModel
{
    public string transactionHash;
    public Metadata metadata;
    public string[] destinations;
    public string[] tokenIds;
    public string walletAddress;
    
    public class Metadata
    {
        public string name;
        public string description;
        public string image;
        public string imagePreview;
        public string imageThumbnail;
        public string backgroundColor;
        public string background_color;
        public string externalUrl;
        public string external_url;
        public string[] animationUrls;
        public List<Attributes> attributes;
        public List<Properties> properties;
        public Contract contract;
        public AssetContract asset_contract;
        public bool fungible;
    }

    public class Attributes
    {
        public string type;
        public string name;
        public string value;
        public string traitType;
        public string trait_type;
    }

    public class Properties
    {
        public string type;
        public string name;
        public string value;
        public string traitType;
        public string trait_type;
    }

    public class Contract
    {
        public string address;
        public string name;
        public string symbol;
        public string image;
        public string imageUrl;
        public string image_Url;
        public string[] media;
        public string type;
    }

    public class AssetContract
    {
        public string address;
        public string name;
        public string symbol;
        public string image;
        public string imageUrl;
        public string image_url;
        public string description;
        public string externalLink;
        public string external_link;
        public string externalUrl;
        public string external_url;
        public string[] media;
        public string type;
    }
}
