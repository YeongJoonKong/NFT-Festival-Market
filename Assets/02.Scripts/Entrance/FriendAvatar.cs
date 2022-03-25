using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendAvatar : MonoBehaviour
{
    public AudioClip[] audioClips;
    public Transform targetDestination;
    public GameObject player;
    Animator anim;

    int[] firstScript = { 0, 1, 2, 3, 4, 5 };
    int[] secondScript = { 6, 7, 8 };
    int[] thirdScript = { 16, 17, 18, 19, 20, 21 };

    NavMeshAgent navMeshAgent;

    void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public IEnumerator PlayFirstScript(int index, Action callback)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < firstScript.Length)
        {
            StartCoroutine(PlayFirstScript(index, callback));
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            navMeshAgent.SetDestination(targetDestination.position);
            anim.SetTrigger("Walk");

            if (callback != null) callback();
            Tutorial.instance.TurnOnInputTutorialRoad();
        }
    }

    public IEnumerator PlaySecondScript(int index)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < firstScript.Length + secondScript.Length)
        {
            StartCoroutine("PlaySecondScript", index);
        }
    }

    public IEnumerator PlayThirdScript(int index)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < firstScript.Length + secondScript.Length + thirdScript.Length)
        {
            StartCoroutine("PlayThirdScript", index);
        }
    }
}
