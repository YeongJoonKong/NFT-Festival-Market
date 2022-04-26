using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SharkGamaManager : MonoBehaviour
{
    enum State
    {
        START,
        PLAY,
        GAME_OVER,
    }

    State state;

    float timer;
    public static float SharkTime;
    public float sharkTimeLimit = 40;
    public float WatingTime = 5.0f;

    SharkManager sharkManager;



    // Start is called before the first frame update
    void Start()
    {
        this.state = State.START;
        this.timer = 0;
        sharkManager = GameObject.Find("SharkGameManager").GetComponent<SharkManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if(state == State.START)
        {
            sharkManager.StartGenerate();
            this.state = State.PLAY;
        }
        else if(state == State.PLAY)
        {
            this.timer += Time.deltaTime;

            SharkTime = timer / sharkTimeLimit;
            
            if(timer >= sharkTimeLimit)
            {
                this.state = State.GAME_OVER;
                
                this.sharkManager.StopGenerate();
                
                this.timer = 0;
            }
        }
        else if(state == State.GAME_OVER)
        {
            timer += Time.deltaTime;
            if(timer > WatingTime)
            {
                SceneManager.LoadScene("Map_01");
            }
        }
    }
}
