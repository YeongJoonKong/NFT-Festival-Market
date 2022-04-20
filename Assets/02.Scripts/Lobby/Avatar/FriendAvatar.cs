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
    public GameObject marker;
    public GameObject rayTextTutorial;
    public GameObject aButtonHighlight;
    public GameObject moveTutorial;

    Animator _anim;
    AudioSource _audioSource;

    int[] Script = {0, 1, 2, 3, 4, 5, 6};

    NavMeshAgent _navMeshAgent;

    bool _isStartWalk;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        marker.SetActive(false);
        aButtonHighlight.SetActive(false);
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


    public IEnumerator PlayScript(int index, Action callback)
    {
        if (index < 0) 
        {
            yield return new WaitForSeconds(1);
            index += 1;
            StartCoroutine(PlayScript(index, callback));
        }
        else
        {
            _audioSource.clip = audioClips[index];
            _audioSource.Play();
            if (index == 3) 
            {
                rayTextTutorial.SetActive(true);
                aButtonHighlight.SetActive(true);
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
                moveTutorial.SetActive(true);
            }
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
