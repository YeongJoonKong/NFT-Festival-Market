using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SharkScoreManager : MonoBehaviour
{
    public static SharkScoreManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    [Header("UI Field")]
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI CurrentScoreText;

    public int CurrentScore;
    public int HighScore;


    // Start is called before the first frame update
    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreText.text = "High Score : " + HighScore.ToString();

        CurrentScoreText.text = "Score : 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int ScorePoint)
    {
        CurrentScore = CurrentScore + ScorePoint;
        PlayerPrefs.SetInt("CurrentScore", CurrentScore);

        CurrentScoreText.text = "Score : " + CurrentScore.ToString();

        if (CurrentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
            HighScoreText.text = "High Score : " + CurrentScore.ToString();
        }
    }
}
