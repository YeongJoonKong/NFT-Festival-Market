using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdGameManager : MonoBehaviour
{
    enum State
    {
        START,
        Tutorial,
        PLAY,
        GAMEOVER,
    }
    State state;

    public static float time;
    float timer;
    public float timeLimit = 60;
    const float waitTime = 5;

    AudioSource audioSource;

    public birdGameBomb bombControll;
    public GameObject Hand;

    float GmCurrentTime;
    float GmWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        this.state = State.START;
        this.timer = 0;
        this.audioSource = GetComponent<AudioSource>();
        this.bombControll = GetComponent<birdGameBomb>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == State.START)
        {
            this.state = State.Tutorial;
            //this.state = State.PLAY;

            this.audioSource.Play();
        }
        else if (this.state == State.Tutorial)
        {
            if(bombControll.BombCounting == 5)
            {
                GmCurrentTime += Time.deltaTime;
                if(GmCurrentTime > GmWaitTime)
                {
                    Hand.SetActive(false);
                    this.state = State.PLAY;
                }
            }
        }
        else if (this.state == State.PLAY)
        {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            if (this.timer > timeLimit)
            {
                this.state = State.GAMEOVER;

                this.timer = 0;

                this.audioSource.loop = false;
            }
        }
        else if (this.state == State.GAMEOVER)
        {
            this.timer += Time.deltaTime;

            if (this.timer > waitTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }










}
