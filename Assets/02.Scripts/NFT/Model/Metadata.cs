using System.Collections.Generic;
using System;

[Serializable]
public class Metadata {
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