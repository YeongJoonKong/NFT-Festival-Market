using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ParaglidingPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    public float fallingForce = 0.25f;
    public float restartTimer = 3.0f;

    public TextMeshProUGUI infoText;

    private bool isGameOver;
    private bool won;

    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "땅에 무사히 착지를 하세요!";
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == false)
        {
            Vector3 direction = new Vector3(transform.forward.x, -fallingForce, transform.forward.z);
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            restartTimer -= Time.deltaTime;

            if(restartTimer < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                if(won == true)
                {
                    infoText.text = "잘했어요! 성공했어요!";
                    infoText.text += "\n게임이 곧 시작 됩니다." + Mathf.Ceil(restartTimer);
                }
                else
                {
                    infoText.text = "다시한번 해보세요!";
                    infoText.text += "\n게임이 곧 시작 됩니다." + Mathf.Ceil(restartTimer);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Target")
        {
            won = true;
        }
        else
        {
            won = false;
        }
        isGameOver = true;
    }
}
