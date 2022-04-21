using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendAvatar : MonoBehaviour, SubjectLobby
{
    List<ObserverLobby> _subscribers = new List<ObserverLobby>();

    public AudioClip[] audioClips;
    public Transform targetDestination;
    public GameObject player;
    public GameObject rayJoystickTutorial;
    public GameObject marker;

    Animator _anim;
    AudioSource _audioSource;

    int[] firstScript = { 0, 1, 2, 3, 4, 5 };
    int[] secondScript = { 6, 7, 8 };
    int[] thirdScript = { 16, 17, 18, 19, 20, 21 };
    int[] Script = {15, 16, 17, 18, 19, 20, 21};

    NavMeshAgent _navMeshAgent;

    bool _isStartWalk;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        rayJoystickTutorial.SetActive(false);
        marker.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
        CheckArriveNavMeshTargetDestination();
    }

    void CheckArriveNavMeshTargetDestination() 
    {
        if (_isStartWalk) 
        {
            if (_navMeshAgent.remainingDistance == 0)
            {
                _anim.SetTrigger("Idle");
                Vector3 tr = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(tr);
            }
        }
    }


    // public IEnumerator PlayFirstScript(int index)
    // {
    //     _audioSource.clip = audioClips[index];
    //     _audioSource.Play();
    //     if (index == 2)
    //     {
    //         _anim.SetTrigger("Greeting");
    //     }
    //     yield return new WaitForSeconds(_audioSource.clip.length + 1);

    //     index += 1;
    //     if (index < firstScript.Length)
    //     {
    //         if (index == 3) {
    //             _anim.SetTrigger("Idle");
    //         }
    //         StartCoroutine(PlayFirstScript(index));
    //     }
    //     else
    //     {
    //         _navMeshAgent.SetDestination(targetDestination.position);
    //         _isStartWalk = true;
    //         _anim.SetTrigger("Walk");
    //         NotifyObserver("ACTIVE_KEY_TUTORIAL");
    //     }
    // }

    // public IEnumerator PlaySecondScript(int index, Action callback)
    // {
    //     _audioSource.clip = audioClips[index];
    //     _audioSource.Play();
    //     yield return new WaitForSeconds(_audioSource.clip.length + 1);
    //     index += 1;
    //     if (index < firstScript.Length + secondScript.Length)
    //     {
    //         StartCoroutine(PlaySecondScript(index, callback));
    //     }
    //     else
    //     {
    //         NotifyObserver("ACTIVE_PURCHASE_TICKET_TUTORIAL");
    //         if (callback != null) callback();
    //     }
    // }

    // public IEnumerator PlayFourthScript(int index, Action callback)
    // {
    //     _audioSource.clip = audioClips[index];
    //     _audioSource.Play();
    //     if (index == 17) {
    //         yield return new WaitUntil(() => OVRInput.Get(OVRInput.Button.Two));
    //     }
    //     yield return new WaitForSeconds(_audioSource.clip.length + 1);
    //     index += 1;
    //     if (index < firstScript.Length + secondScript.Length + thirdScript.Length)
    //     {
    //         StartCoroutine(PlayFourthScript(index, callback));
    //     }
    //     else
    //     {
    //         door.GetComponent<Door>().OpenDoor();
    //         if (callback != null) callback();
    //         transform.LookAt(door.transform);
    //         _navMeshAgent.SetDestination(door.transform.position);
    //         _isStartWalk = true;
    //         _anim.SetTrigger("Walk");
    //     }
    // }

    public IEnumerator PlayScript(int index, Action callback)
    {
        _audioSource.clip = audioClips[index];
        _audioSource.Play();
        if (index == 18) 
        {
            rayJoystickTutorial.SetActive(true);
            _navMeshAgent.SetDestination(targetDestination.position);
            _isStartWalk = true;
            _anim.SetTrigger("Walk");
            yield return new WaitUntil(() => OVRInput.GetUp(OVRInput.Button.One));
        }
        yield return new WaitForSeconds(_audioSource.clip.length + 1);
        index += 1;

        if (index <= Script[Script.Length-1])
        {
            StartCoroutine(PlayScript(index, callback));
        }
        else
        {
            if (callback != null) callback();
            marker.SetActive(true);
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
