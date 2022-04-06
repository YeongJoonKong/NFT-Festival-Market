using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour, SubjectLobby
{
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) {
            gameObject.SetActive(false);
            Inventory.instance.Wallet = this.gameObject;
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
