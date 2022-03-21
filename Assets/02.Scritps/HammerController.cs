using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    public GameObject EffactParticle;
    public AudioClip hitAudio;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator HitTheMole(Vector3 target)
    {
        Instantiate(this.EffactParticle, transform.position, Quaternion.identity);

        this.audioSource.PlayOneShot(this.hitAudio);

        yield return new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Mole"))
        {
            GameObject mole = other.gameObject;

            bool isHit = mole.GetComponent<MoleController>().Hit();

            if(isHit)
            {
                StartCoroutine(HitTheMole(mole.transform.position));

                ScoreManager.score += 10;
            }


        }
    }




}
