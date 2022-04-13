using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public const float Top = 4.0f;
    public const float Bottom = -2.2f;

    public float tmpTime;
    public float WaitingTime = 1.0f;

    public GameObject WaterEffect01;
    public GameObject WaterEffect02;

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

        this.gameObject.transform.position = new Vector3(transform.position.x, Bottom -0.5f, transform.position.z);
        return true;
    }

    public void Up()
    {
        if(this.state == State.UNDER_GOUND)
        {
            this.state = State.UP;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.state = State.UNDER_GOUND;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.state == State.UP)
        {
            isWaterFloorChecking = true;
            //transform.Translate(0, Time.deltaTime * MovingSpeed, 0);
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * Random.Range(3, 11), Time.deltaTime); ;
            
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

            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down * Random.Range(3, 16), Time.deltaTime * 0.6f);

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
