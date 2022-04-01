using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    enum State
    {
        START,
        PLAY,
        GAMEOVER,
    }

    public static float time;
    public float timeLimit = 30;
    const float waitTime = 5;

    MoleManager moleManager;
    public Text StartText;
    public Text remainongTime;
    AudioSource audioSource;

    float currentTime;
    float creatTime=3;

    State state;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        this.state = State.START;
        this.timer = 0;
        this.moleManager = GameObject.Find("GameManager").GetComponent<MoleManager>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("state: " + state);

        if (this.state == State.START)
        {
            currentTime += Time.deltaTime;
            if(currentTime > creatTime)
            {
                StartText.text = "";

                this.state = State.PLAY;

                this.moleManager.StartGenerate();

                this.audioSource.Play();

                currentTime = 0;
            }


            
        }
        else if(this.state == State.PLAY)
        {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            if(this.timer > timeLimit)
            {
                StartText.text = "Game Over";

                this.remainongTime.text = "Time : 0";

                this.state = State.GAMEOVER;

                this.moleManager.StopGenerate();

                this.timer = 0;

                this.timeLimit = 0;

                this.audioSource.Stop();

                this.audioSource.loop = false;
            }
            this.remainongTime.text = "Time : " + ((int)(timeLimit - timer)).ToString("D2");
        }
        else if(this.state == State.GAMEOVER)
        {
            this.timer += Time.deltaTime;

            if(this.timer > waitTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            this.remainongTime.text = "";
        }
    }
}
