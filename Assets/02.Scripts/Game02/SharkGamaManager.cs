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
    public static float sharkTimeLimit = 50;
    public float WatingTime = 5.0f;

    SharkManager sharkManager;

    AudioSource audioSource;

    public TextMeshProUGUI timeText;

    public GameObject gameOverUI;
    public GameObject gamePlayingUI;
    public GameObject sharksGroup;

    // Start is called before the first frame update
    void Start()
    {
        this.state = State.START;
        this.timer = 0;
        sharkManager = GameObject.Find("SharkGameManager").GetComponent<SharkManager>();
        audioSource = GetComponent<AudioSource>();
        timeText.text = "Time : " + timeText;
        gameOverUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(state == State.START)
        {
            sharkManager.StartGenerate();
            this.state = State.PLAY;
            audioSource.Play();
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
                sharksGroup.SetActive(false);
                gamePlayingUI.SetActive(false);
            }

            timeText.text = "Time : " + ((int)(sharkTimeLimit - timer));
        }
        else if(state == State.GAME_OVER)
        {
            gameOverUI.SetActive(true);
            audioSource.loop = false;
            timer += Time.deltaTime;
            if(timer > WatingTime)
            {
                //SceneManager.LoadScene("Map_01");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
