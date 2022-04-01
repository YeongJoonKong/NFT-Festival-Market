using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationZone : MonoBehaviour, SubjectLobby
{
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    public bool _isArrive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _isArrive = true;
        NotifyObserver();
    }

    public bool GetIsArrive() {
        return _isArrive;
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
}
