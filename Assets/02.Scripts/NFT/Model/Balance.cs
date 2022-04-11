using System;

[Serializable]
public class Balance {
    public bool available;
    public string secretType;
    public int balance;
    public int gasBalance;
    public string symbol;
    public string gasSymbol;
    public string rawBalance;
    public string rawGasBalance;
    public int decimals;
}