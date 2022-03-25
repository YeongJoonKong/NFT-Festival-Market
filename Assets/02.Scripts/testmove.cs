using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class testmove : MonoBehaviour
{
    public Transform cameraAxis;
    public Transform body;
    public float speed = 5;

    private PhotonView pv;
    public GameObject cam;
    public GameObject[] avatars;
    string avatar;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {


        pv = GetComponent<PhotonView>();


        avatar = PlayerPrefs.GetString("Avatar");

        if (pv.IsMine)
        {
            pv.RPC("AvatarChange", RpcTarget.AllBufferedViaServer, avatar);

        }
        PlayerPrefs.DeleteAll();



        if (pv.IsMine)
        {
            //내카메라만켜요
            cam.SetActive(true);
        }
        if (!pv.IsMine)
        {
            //남카메라오프
            cam.SetActive(false);
        }

    }
    [PunRPC]
    private void AvatarChange(string avatar)
    {

        
        for (int i = 0; i < avatars.Length; i++)
        {
            if (avatars[i].tag == avatar)
            {
                this.avatars[i].SetActive(true);
            }
           
        }

        


    }

    float rx, ry;
    float rotSpeed = 200;
    // Update is called once per frame
    void Update()
    {
        // 내플레이어만
        if (pv.IsMine)
        {
            //살았을때

            // 마우스 입력값으로 카메라 축을 회전하고싶다.
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");
            rx += my * rotSpeed * Time.deltaTime;
            ry += mx * rotSpeed * Time.deltaTime;

            rx = Mathf.Clamp(rx, -70, 45);

            cameraAxis.transform.eulerAngles = new Vector3(-rx, ry, 0);

            // 키보드의 이동축을 입력받고싶다.
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            // 키보드 입력이 있다면
            if (h != 0 || v != 0)
            {
                // 몸을 카메라가 바라보는 앞쪽으로 회전하고싶다.
                Vector3 bodyDir = cameraAxis.transform.forward;
                bodyDir.y = 0;
                bodyDir.Normalize();
                body.forward = bodyDir;
            }

            // 몸의 앞방향을 기준으로 이동하고싶다.
            Vector3 dir = body.transform.right * h + body.transform.forward * v;
            dir.Normalize();


            transform.position += dir * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, cameraAxis.eulerAngles.y, 0));


            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);




        }

    }
}
