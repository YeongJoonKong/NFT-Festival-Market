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
    public TextMeshProUGUI SharkHighScoreText;
    public TextMeshProUGUI SharkCurrentScoreText;
    public TextMeshProUGUI SharkfinalScoreText;


    public int SharkCurrentScore;
    public int SharkHighScore;


    // Start is called before the first frame update
    void Start()
    {
        SharkHighScore = PlayerPrefs.GetInt("HighScore", 0);
        SharkHighScoreText.text = "High Score : " + SharkHighScore.ToString();

        SharkCurrentScoreText.text = "Score : 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int ScorePoint)
    {
        SharkCurrentScore = SharkCurrentScore + ScorePoint;
        PlayerPrefs.SetInt("CurrentScore", SharkCurrentScore);

        SharkCurrentScoreText.text = "Score : " + SharkCurrentScore.ToString();

        SharkfinalScoreText.text = "Final Score : " + SharkCurrentScore.ToString();

        if (SharkCurrentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", SharkCurrentScore);
            SharkHighScoreText.text = "High Score : " + SharkCurrentScore.ToString();
        }
    }
}
