using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using RootMotion.FinalIK;

public class PlayerInstantiate : MonoBehaviour
{


    private PhotonView pv;
    //public GameObject cam;
    public GameObject[] avatars;
    string avatar;
    public GameObject CameaRig;
    public VRIK vrik;
    private OVRPlayerController opc;
    public GameObject headPivot;
    public GameObject leftHandPivot;
    public GameObject rightHandPivot;

    public Vector3 headoffset;
    public Vector3 leftHandoffset;
    public Vector3 rightHandoff;

    


    // Start is called before the first frame update
    private void Awake()
    {
        opc=GetComponent<OVRPlayerController>();
        avatar = PlayerPrefs.GetString("Avatar");
        pv = GetComponent<PhotonView>();

        

        if (pv.IsMine)
        {
            CameaRig = GameObject.Find("OVRCameraRig1");
            CameaRig.transform.parent = transform;
            headPivot.transform.parent = GameObject.Find("CenterEyeAnchor").transform;
            leftHandPivot.transform.parent = GameObject.Find("LeftHandAnchor").transform;
            rightHandPivot.transform.parent = GameObject.Find("RightHandAnchor").transform;

        }
        if (!pv.IsMine)
        {
            opc.enabled = false;
        }

    }
    void Start()
    {


        if (pv.IsMine)
        {
            pv.RPC("AvatarChange", RpcTarget.AllBufferedViaServer, avatar);
            
             vrik =GetComponentInChildren<VRIK>();
            print(GameObject.FindWithTag(avatar));
            
            vrik.solver.spine.headTarget = GameObject.Find("HeadPivot").gameObject.transform;
            vrik.solver.leftArm.target = GameObject.Find("LeftHandPivot").gameObject.transform;
            vrik.solver.rightArm.target = GameObject.Find("RightHandPivot").gameObject.transform;
        }
        if (!pv.IsMine)
        {
            vrik = GetComponentInChildren<VRIK>();
            vrik.enabled = false;

        }




        //if (pv.IsMine)
        //{
        //    //��ī�޶��ѿ�
        //    cam.SetActive(true);
        //}
        //if (!pv.IsMine)
        //{
        //    //��ī�޶����
        //    cam.SetActive(false);
        //}

    }
    [PunRPC]
    private void AvatarChange(string avatar)
    {


        for (int i = 0; i < avatars.Length; i++)
        {
            if (avatars[i])
            {
                this.avatars[i].SetActive(false);
            }
        }
        for (int i = 0; i < avatars.Length; i++)
        {
            if (avatars[i].tag == avatar)
            {
                this.avatars[i].SetActive(true);
            }

        }





    }

    //float rx, ry;
    //float rotSpeed = 200;
    // Update is called once per frame
    //void Update()
    //{
    //    // ���÷��̾
    //    if (pv.IsMine)
    //    {
    //        //�������

    //        // ���콺 �Է°����� ī�޶� ���� ȸ���ϰ�ʹ�.
    //        float mx = Input.GetAxis("Mouse X");
    //        float my = Input.GetAxis("Mouse Y");
    //        rx += my * rotSpeed * Time.deltaTime;
    //        ry += mx * rotSpeed * Time.deltaTime;

    //        rx = Mathf.Clamp(rx, -70, 45);

    //        cameraAxis.transform.eulerAngles = new Vector3(-rx, ry, 0);

    //        // Ű������ �̵����� �Է¹ް�ʹ�.
    //        float h = Input.GetAxisRaw("Horizontal");
    //        float v = Input.GetAxisRaw("Vertical");
    //        // Ű���� �Է��� �ִٸ�
    //        if (h != 0 || v != 0)
    //        {
    //            // ���� ī�޶� �ٶ󺸴� �������� ȸ���ϰ�ʹ�.
    //            Vector3 bodyDir = cameraAxis.transform.forward;
    //            bodyDir.y = 0;
    //            bodyDir.Normalize();
    //            body.forward = bodyDir;
    //        }

    //        // ���� �չ����� �������� �̵��ϰ�ʹ�.
    //        Vector3 dir = body.transform.right * h + body.transform.forward * v;
    //        dir.Normalize();


    //        transform.position += dir * speed * Time.deltaTime;

    //        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, cameraAxis.eulerAngles.y, 0));


    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);




    //    }

    //}
}
