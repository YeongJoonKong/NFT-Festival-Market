using System;

[Serializable]
public class WalletInfo {
    public string id;
    public string address;
    public string walletType;
    public string secretType;
    public string createdAt;
    public bool archived;
    public string description;
    public bool primary;
    public bool hasCustomPin;
    public string identifier;
    public Balance balance;
    
}