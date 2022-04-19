using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarNPC : MonoBehaviour, SubjectLobby
{
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    public GameObject leftCurtain;
    public GameObject rightCurtain;
    public GameObject[] audioClips;

    void Start()
    {

    }

    void Update()
    {
        // PullCurtains();        
    }

    public void PlayScript() 
    {
        
    }

    public void PullCurtains() {
        if (leftCurtain.transform.position.x >= -34.7f) {
            leftCurtain.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (rightCurtain.transform.position.x <= -11.71f) {
            rightCurtain.transform.position += new Vector3(0.1f, 0, 0);
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
