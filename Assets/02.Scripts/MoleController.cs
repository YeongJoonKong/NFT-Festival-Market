using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float waitTime = 1.0f;

    private const float Top = -2.35f;
    private const float Bottom = -3.45f;
    private float tmpTime = 0;


    enum State
    {
        UNDER_GROUND,
        ON_GROUND,
        UP,
        DOWN,
        HIT,
    }
    State state;


    public bool Hit()
    {
        if(this.state == State.UNDER_GROUND)
        {
            return false;
        }

        transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);

        this.state = State.UNDER_GROUND;

        return true;
    }

    public void Up()
    {
        if(this.state == State.UNDER_GROUND)
        {
            this.state = State. UP;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.state = State.UNDER_GROUND;
    }

    // Update is called once per frame
    void Update()
    {

        //print("state :" + state);

        if(this.state == State.UP)
        {
            transform.Translate(0, this.moveSpeed, 0);

            if(transform.position.y > Top)
            {
                transform.position = new Vector3(transform.position.x, Top, transform.position.z);

                this.state = State.ON_GROUND;

                this.tmpTime = 0;
            }
        }
        else if(this.state == State.ON_GROUND)
        {
            this.tmpTime += Time.deltaTime;

            if(this.tmpTime > this.waitTime)
            {
                this.state = State.DOWN;
            }
        }
        else if(this.state == State.DOWN)
        {
            transform.Translate(0, -this.moveSpeed, 0);

            if(transform.position.y < Bottom)
            {
                transform.position = new Vector3(transform.position.x, Bottom, transform.position.z);

                this.state = State.UNDER_GROUND;
            }
        }
    }
}
