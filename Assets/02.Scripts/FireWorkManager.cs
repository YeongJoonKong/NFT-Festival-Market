using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorkManager : MonoBehaviour
{
    public GameObject FireWork01;
    public GameObject FireWork02;
    public GameObject FireWork03;
    public GameObject FireWork04;
    public GameObject FireWork05;
    public GameObject FireWork06;

    public AudioClip FireWorkSound01;
    public AudioClip FireWorkSound02;
    public AudioClip FireWorkSound03;

    AudioSource audioSource;

    float FireworkCurrentTime;
    public float FireworkCreatTime = 8;    
    
    float FireworkCurrentTime2;
    public float FireworkCreatTime2 = 15;

    float FireworkCurrentTime3;
    public float FireworkCreatTime3 = 15;

    float soundCurrentTime;
    float soundWaitTime = 2;

    bool isOnceCheck;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Invoke("FireWorkSound", 15f);
    }

    // Update is called once per frame
    void Update()
    {
        FireworkCurrentTime += Time.deltaTime;
        if(FireworkCurrentTime > FireworkCreatTime)
        {
            if(isOnceCheck == false)
            {
                FireWorkSound();
            }

            FireworkGenerator();

            FireworkCurrentTime2 += Time.deltaTime;
            if(FireworkCurrentTime2 > FireworkCreatTime2)
            {
                FireworkGenerator2();

                FireworkCurrentTime3 += Time.deltaTime;
                if(FireworkCurrentTime3 > FireworkCreatTime3)
                {
                    FireWork05.SetActive(true);
                    FireWork06.SetActive(true);
                }
            }
        }
    }

    void FireWorkSound()
    {
        audioSource.PlayOneShot(FireWorkSound01, 1);
        audioSource.PlayOneShot(FireWorkSound02, 2);
        audioSource.PlayOneShot(FireWorkSound03, 3);
        isOnceCheck = true;
        Invoke("SoundDelay",14.5f);
    }

    void SoundDelay()
    {
        isOnceCheck = false;
    }

    void FireworkGenerator()
    {
        FireWork01.SetActive(true);
        FireWork02.SetActive(true);
        Invoke("FireWorkDisactive", 7);
    }

    void FireWorkDisactive()
    {
        FireWork01.SetActive(false);
        FireWork02.SetActive(false);
    }

    void FireworkGenerator2()
    {
        FireWork03.SetActive(true);
        FireWork04.SetActive(true);
        Invoke("FireWorkDisactive2", 7);
    }

    void FireWorkDisactive2()
    {
        FireWork03.SetActive(false);
        FireWork04.SetActive(false);
    }
}
