using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public const float Top = 4.0f;
    public const float Bottom = -2.2f;

    public float WaitingTime = 1.0f;
    float tmpTime;

    public GameObject WaterEffect01;
    public GameObject WaterEffect02;

    AudioSource audioSource;

    public AudioClip jumpingSound;
    public AudioClip bubblingSound;

    bool isWaterFloorChecking;
    enum State
    {
        UNDER_GOUND,
        ON_GROUND,
        UP,
        DOWN,
        HIT,
    }
    State state;

    public bool Hit()
    {
        if(this.state == State.UNDER_GOUND)
        {
            return false;
        }

        audioSource.PlayOneShot(bubblingSound);
        this.gameObject.transform.position = new Vector3(transform.position.x, Bottom -0.5f, transform.position.z);
        return true;
    }

    public void Up()
    {
        if(this.state == State.UNDER_GOUND)
        {
            audioSource.PlayOneShot(jumpingSound);
            this.state = State.UP;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = State.UNDER_GOUND;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.state == State.UP)
        {
            isWaterFloorChecking = true;
            //transform.Translate(0, Time.deltaTime * MovingSpeed, 0);
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * Random.Range(3, 16), Time.deltaTime);
            
            if(transform.position.y > Top)
            {
                this.transform.position = new Vector3(transform.position.x, Top, transform.position.z);
                this.state = State.ON_GROUND;
                isWaterFloorChecking = false;
            }
        }
        else if(this.state == State.ON_GROUND)
        {
            tmpTime += Time.deltaTime;
            if(tmpTime > WaitingTime)
            {
                this.state = State.DOWN;
                tmpTime = 0;
            }
        }
        else if(this.state == State.DOWN)
        {
            //transform.Translate(0, Time.deltaTime * -MovingSpeed, 0);

            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down * Random.Range(3, 20), Time.deltaTime);

            if(transform.position.y < Bottom)
            {
                this.transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);
                state = State.UNDER_GOUND;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isWaterFloorChecking == false)
        {
            if (other.CompareTag("WaterFloor"))
            {
                GameObject waterEffect02 = Instantiate(WaterEffect02);
                waterEffect02.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isWaterFloorChecking == true)
        {
            if (other.CompareTag("WaterFloor"))
            {
                GameObject waterEffect01 = Instantiate(WaterEffect01);
                waterEffect01.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
    }
}
