using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    float timer2;
    public float timeLimit = 60;
    const float waitTime = 5;

    AudioSource audioSource;

    public birdGameBomb bombControll;
    public GameObject Hand;

    float SignCurrentTime;
    float SignWaitTime = 6;
    public GameObject Sign;

    public TextMeshProUGUI RemainingTimeText;

    public GameObject BirdGamePlayingGroup;
    public GameObject BirdGameOverGroup;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 51;

        this.state = State.START;
        this.timer = 0;
        this.audioSource = GetComponent<AudioSource>();
        this.bombControll = GetComponent<birdGameBomb>();
        RemainingTimeText.text = "Time : 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (this.state == State.START)
        {
            this.state = State.PLAY;

            this.audioSource.Play();
        }
        else if (this.state == State.PLAY)
        {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            SignCurrentTime += Time.deltaTime;

            if(SignCurrentTime > SignWaitTime)
            {
                Sign.SetActive(false);
            }

            if (this.timer > timeLimit)
            {
                this.state = State.GAMEOVER;

                this.timer = 0;

                this.audioSource.loop = false;
            }

            RemainingTimeText.text = "Time : " + ((int)(timeLimit - timer));

        }
        else if (this.state == State.GAMEOVER)
        {
            BirdGamePlayingGroup.SetActive(false);
            BirdGameOverGroup.SetActive(true);
            this.timer2 += Time.deltaTime;

            if (this.timer2 > waitTime)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Map_01");
            }
        }
    }










}
