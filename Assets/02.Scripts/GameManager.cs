using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    const float waitTime = 7;

    MoleManager moleManager;
    public TextMeshProUGUI RemainingTimeText;
    //Text remainongTime;
    AudioSource audioSource;

    State state;
    float timer;

    public GameObject GamePlayingUI;
    public GameObject GameOverUI;

    List<MoleController> moles2 = new List<MoleController>();


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        this.state = State.START;
        this.timer = 0;
        this.moleManager = GameObject.Find("GameManager").GetComponent<MoleManager>();
        RemainingTimeText.text = "Time : 0";
        this.audioSource = GetComponent<AudioSource>();

        GameOverUI.SetActive(false);

        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Mole");

        foreach(GameObject go2 in gos2)
        {
            moles2.Add(go2.GetComponent<MoleController>());
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (this.state == State.START)
        {
            this.state = State.PLAY;

            this.moleManager.StartGenerate();

            this.audioSource.Play();
        }
        else if(this.state == State.PLAY)
        {
            this.timer += Time.deltaTime;
            time = this.timer / timeLimit;

            if(this.timer > timeLimit)
            {
                this.state = State.GAMEOVER;

                this.moleManager.StopGenerate();

                this.timer = 0;

                this.audioSource.loop = false;
            }

            RemainingTimeText.text = "Time : " + ((int)(timeLimit - timer));
        }
        else if(this.state == State.GAMEOVER)
        {
            GameOverUI.SetActive(true);
            GamePlayingUI.SetActive(false);

            for (int i = 0; i < moles2.Count; i++)
            {
                this.moles2[i].GetComponent<SphereCollider>().enabled = false;
            }

            this.timer += Time.deltaTime;

            if(this.timer > waitTime)
            {
                SceneManager.LoadScene("Map_01");
            }
        }
    }
}
