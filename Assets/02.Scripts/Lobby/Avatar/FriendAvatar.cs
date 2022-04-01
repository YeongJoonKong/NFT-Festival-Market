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

    Animator anim;
    AudioSource audioSource;

    int[] firstScript = { 0, 1, 2, 3, 4, 5 };
    int[] secondScript = { 6, 7, 8 };
    int[] thirdScript = { 16, 17, 18, 19, 20, 21 };

    NavMeshAgent navMeshAgent;

    bool isSetNavMeshAgentDestination;
    bool _isPlayerHandShake;

    int _currentScriptEnd;

    void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {
        ArriveNavMeshTargetDestination();
    }

    void ArriveNavMeshTargetDestination() 
    {
        if (isSetNavMeshAgentDestination)
        {
            if (navMeshAgent.remainingDistance == 0)
            {
                anim.SetTrigger("Idle");
                Vector3 tr = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.LookAt(tr);
            }
        }
    }

    // TODO: 중복코드 제거
    public IEnumerator PlayFirstScript(int index, Action callback)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        if (index == 2)
        {
            anim.SetTrigger("Greeting");
            SetIsPlayerHandShake(false);
            NotifyObserver();
            yield return new WaitUntil(() => OVRInput.GetDown(OVRInput.Button.Two));
        } else {
            yield return new WaitForSeconds(audioSource.clip.length + 1);
        }
        
        SetIsPlayerHandShake(true);
        NotifyObserver();

        index += 1;
        if (index < firstScript.Length)
        {
            anim.SetTrigger("Idle");
            StartCoroutine(PlayFirstScript(index, callback));
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            navMeshAgent.SetDestination(targetDestination.position);
            anim.SetTrigger("Walk");
            isSetNavMeshAgentDestination = true;
            _currentScriptEnd = 1;
            NotifyObserver();
            if (callback != null) callback();
        }
    }

    public IEnumerator PlaySecondScript(int index, Action callback)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < firstScript.Length + secondScript.Length)
        {
            StartCoroutine(PlaySecondScript(index, callback));
        }
        else
        {
            _currentScriptEnd = 2;
            NotifyObserver();
            if (callback != null) callback();
        }
    }

    public IEnumerator PlayFourthScript(int index, Action callback)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < firstScript.Length + secondScript.Length + thirdScript.Length)
        {
            StartCoroutine(PlayFourthScript(index, callback));
        }
        else
        {
            _currentScriptEnd = 4;
            NotifyObserver();
            if (callback != null) callback();
        }
    }

    public int GetCurrentScriptEnd()
    {
        return this._currentScriptEnd;
    }

    public bool GetIsPlayerHandShake() {
        return this._isPlayerHandShake;
    }

    public void SetIsPlayerHandShake(bool value) {
        this._isPlayerHandShake = value;
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
}
