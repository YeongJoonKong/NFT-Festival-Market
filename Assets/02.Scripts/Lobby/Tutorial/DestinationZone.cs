using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationZone : MonoBehaviour, SubjectLobby
{
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        NotifyObserver("PLAYER_ARRIVE_DESTINATION");
    }

    public void AddObserver(ObserverLobby subscriber) {
        this._subscribers.Add(subscriber); 
    }

    public void RemoveObserver(ObserverLobby subscriber)
    {
        if (_subscribers.Contains(subscriber))
        {
            this._subscribers.Remove(subscriber);
        }
    }

    public void NotifyObserver() {
        foreach(var subscriber in _subscribers)
        {
            subscriber.DetectEvent(this);
        }
    }

    public void NotifyObserver(string _event) {
        foreach(var subscriber in _subscribers)
        {
            subscriber.DetectEvent(_event);
        }
    }
}
