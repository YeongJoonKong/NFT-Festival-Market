using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WackAMoleScoreManager : MonoBehaviour
{
    public int CurrentScore;
    public int HighScore;

    [Header("UI Fields")]
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI finalScoreText;

    public static WackAMoleScoreManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

    }



    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score : " + HighScore.ToString();

        currentScoreText.text = "Score : 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int ScorePoint)
    {
        CurrentScore = CurrentScore + ScorePoint;
        PlayerPrefs.SetInt("CurrentScore", CurrentScore);

        currentScoreText.text = "Score : " + CurrentScore.ToString();

        finalScoreText.text = "Final Score : " + CurrentScore.ToString();

        if (CurrentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
            highScoreText.text = "High Score : " + CurrentScore.ToString(); 
        }
    }
}
