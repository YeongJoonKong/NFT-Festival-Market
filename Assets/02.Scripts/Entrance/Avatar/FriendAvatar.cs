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

    bool isSetNavMeshAgentDestination;

    int index;

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
            isSetNavMeshAgentDestination = true;

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
            if (callback != null) callback();
        }
    }

    public IEnumerator PlayThirdScript(int index, Action callback)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[index];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 1);
        index += 1;
        if (index < firstScript.Length + secondScript.Length + thirdScript.Length)
        {
            StartCoroutine(PlayThirdScript(index, callback));
        }
        else
        {
            if (callback != null) callback();
        }
    }
}
