using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public const float Top = 4.0f;
    public const float Bottom = -2.2f;

    public float MovingSpeed = 5.0f;

    public float tmpTime;
    public float WaitingTime = 1.0f;

    enum State
    {
        UNDER_GOUND,
        ON_GROUND,
        UP,
        DOWN,
        HIT,
    }
    State state;

    public void Hit()
    {
        if(this.state == State.UNDER_GOUND)
        {
            return;
        }

        this.gameObject.transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);
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
            transform.Translate(0, Time.deltaTime * MovingSpeed, 0);
            
            if(transform.position.y > Top)
            {
                this.transform.position = new Vector3(transform.position.x, Top, transform.position.z);
                this.state = State.ON_GROUND;
            }
        }
        else if(this.state == State.ON_GROUND)
        {
            tmpTime += Time.deltaTime;
            if(tmpTime > WaitingTime)
            {
                this.state = State.DOWN;
            }
        }
        else if(this.state == State.DOWN)
        {
            transform.Translate(0, Time.deltaTime * MovingSpeed, 0);

            if(transform.position.y > Bottom)
            {
                this.transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);
                state = State.UNDER_GOUND;
            }
        }
    }
}
