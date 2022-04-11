using System;

[Serializable]
public class TicketInfo {
    public string transactionHash;
    public Metadata metadata;
    public string[] destinations;
    public string[] tokenIds;
}