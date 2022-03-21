using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    Text ScoreText;

    private void Awake()
    {
        this.ScoreText = GetComponent<Text>();
        score = 0;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.ScoreText.text = "Score: " + score;
    }
}
