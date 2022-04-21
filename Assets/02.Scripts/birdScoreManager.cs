using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class birdScoreManager : MonoBehaviour
{
    public int BdCurrentScore;
    public int BdHighScore;

    [Header("UI Fields")]
    public TextMeshProUGUI BdhighScoreText;
    public TextMeshProUGUI BdcurrentScoreText;
    //public TextMeshProUGUI finalScoreText;

    public static birdScoreManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        BdHighScore = PlayerPrefs.GetInt("BirdGameHighScore", 0);
        BdhighScoreText.text = "High Score : " + BdHighScore.ToString();

        BdcurrentScoreText.text = "Score : 0";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int ScorePoint)
    {
        BdCurrentScore = BdCurrentScore + ScorePoint;
        PlayerPrefs.SetInt("BirdGameCurrentScore", BdCurrentScore);

        BdcurrentScoreText.text = "Score : " + BdCurrentScore.ToString();

        //finalScoreText.text = "Final Score : " + BdCurrentScore.ToString();

        if (BdCurrentScore > PlayerPrefs.GetInt("BirdGameHighScore", 0))
        {
            PlayerPrefs.SetInt("BirdGameHighScore", BdCurrentScore);
            BdhighScoreText.text = "High Score : " + BdCurrentScore.ToString();
        }
    }










}
