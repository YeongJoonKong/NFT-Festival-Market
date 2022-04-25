using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ParaglidingPlayer : MonoBehaviour
{
    public float speed = 5.0f;
    public float fallingForce = 0.4f;
    public float restartTimer = 3.0f;

    public TextMeshProUGUI infoText;

    private bool isGameOver;
    private bool won;

    //public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "마을 북쪽에 있는 꽃에 무사히 착지를 하세요!";
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == false)
        {
            if (transform.position.y >= 224f)
            {
                transform.position = new Vector3(transform.position.x, 224f, transform.position.z);
            }
            Vector3 direction = new Vector3(Camera.main.transform.forward.x, -fallingForce, Camera.main.transform.forward.z);
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
                    infoText.text += "\n마을로 다시 돌아갑니다." + Mathf.Ceil(restartTimer);
                }
                else
                {
                    infoText.text = "아쉽네요 다음에 다시한번 해보세요!";
                    infoText.text += "\n마을로 다시 돌아갑니다." + Mathf.Ceil(restartTimer);
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
