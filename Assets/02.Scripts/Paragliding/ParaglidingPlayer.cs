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
        infoText.text = "���� ���ʿ� �ִ� �ɿ� ������ ������ �ϼ���!";
       
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
                    infoText.text = "���߾��! �����߾��!";
                    infoText.text += "\n������ �ٽ� ���ư��ϴ�." + Mathf.Ceil(restartTimer);
                }
                else
                {
                    infoText.text = "�ƽ��׿� ������ �ٽ��ѹ� �غ�����!";
                    infoText.text += "\n������ �ٽ� ���ư��ϴ�." + Mathf.Ceil(restartTimer);
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
