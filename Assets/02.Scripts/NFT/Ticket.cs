using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class Ticket : MonoBehaviour, SubjectLobby
{
    public TextMesh ticketNumber;
    public TextMeshPro ticketName;

    bool _isDecideNFT;
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    void Start()
    {
        NFTManager nftManager = new NFTManager();
        ticketNumber.text = nftManager.ReadTicketInfoJsonFile("transactionHash").ToString();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
            gameObject.SetActive(false);
            Inventory.instance.Ticket = this.gameObject;
            NotifyObserver();
        }
    }

    public void AddObserver(ObserverLobby subscriber)
    {
        this._subscribers.Add(subscriber);
    }

    public void RemoveObserver(ObserverLobby subscriber)
    {
        if (_subscribers.Contains(subscriber))
        {
            this._subscribers.Remove(subscriber);
        }
    }

    public void NotifyObserver()
    {
        foreach(var subscriber in _subscribers)
        {
            subscriber.DetectEvent(this);
        }
    }

    public void NotifyObserver(string _event) 
    {
        foreach(var subscriber in _subscribers) 
        {
            subscriber.DetectEvent(_event);
        }
    }
}
