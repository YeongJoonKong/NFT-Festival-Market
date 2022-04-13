using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using RootMotion.FinalIK;

public class PlayerInstantiate : MonoBehaviour, IPunObservable
{


    private PhotonView pv;
    //public GameObject cam;
    public GameObject[] avatars;
    string avatar;
    public GameObject CameaRig;
    //public GameObject myavatar;


    // CarmeraRig Pivot Transform
    private Transform headTr, leftHandTr, rightHandTr;
    // FalseCamera Rig Transform
    public Transform FalseHeadRig, FalseLeftRig, FalseRightRig;

    public VRIK vrik;
    public CharacterController controller;
    public OVRPlayerController ovrcontroller;



    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        pv = GetComponent<PhotonView>();
        vrik = GetComponentInChildren<VRIK>();
        controller = GetComponent<CharacterController>();
        ovrcontroller = GetComponent<OVRPlayerController>();
        if (pv.IsMine)
        {
            CameaRig = GameObject.Find("OVRCameraRig1");

            CameaRig.transform.parent = transform;

            avatar = PlayerPrefs.GetString("Avatar");

            headTr = GameObject.FindGameObjectsWithTag(avatar)[0].transform;
            leftHandTr = GameObject.FindGameObjectsWithTag(avatar)[1].transform;
            rightHandTr = GameObject.FindGameObjectsWithTag(avatar)[2].transform;
       

            //print(avatar);
            //myavatar = GameObject.FindGameObjectWithTag(avatar);

            //print(myavatar);
            //myavatar.transform.parent = transform;

            pv.RPC("AvatarChange", RpcTarget.AllBufferedViaServer, avatar);


        }
        if (!pv.IsMine)
        {

            controller.enabled = false;
            ovrcontroller.enabled = false;
        }
        PhotonNetwork.IsMessageQueueRunning = true;
    }
    void Start()
    {

        if (pv.IsMine)
        {
            print(GameObject.FindGameObjectsWithTag(avatar)[0].gameObject.transform);
            vrik.solver.spine.headTarget = headTr;
            vrik.solver.leftArm.target = leftHandTr;
            vrik.solver.rightArm.target = rightHandTr;
        }
        else
        {
            vrik.solver.spine.headTarget = FalseHeadRig;
            vrik.solver.leftArm.target = FalseLeftRig;
            vrik.solver.rightArm.target = FalseRightRig;
        }

        //if (pv.IsMine)
        //{

        //pv.RPC("ConnectingVRIK", RpcTarget.AllBufferedViaServer, avatar);
        //}

        //PlayerPrefs.DeleteAll();



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
            if (avatars[i].tag == avatar)
            {
                this.avatars[i].SetActive(true);
            }

        }
    }
    //�Ӹ�����ȭ����
    private Quaternion currRotHead;
    private Vector3 curPosHead;
    //�޼յ���ȭ����
    private Quaternion curRotLeftHand;
    private Vector3 curPosLeftHand;

    //�����յ���ȭ����
    private Quaternion curRotRightHand;
    private Vector3 curPosRightHand;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(headTr.rotation);
            stream.SendNext(headTr.position);
            stream.SendNext(leftHandTr.rotation);
            stream.SendNext(leftHandTr.position);
            stream.SendNext(rightHandTr.rotation);
            stream.SendNext(rightHandTr.position);




        }
        else
        {
            
            currRotHead = (Quaternion)stream.ReceiveNext();
            curPosHead = (Vector3)stream.ReceiveNext();
            curRotLeftHand = (Quaternion)stream.ReceiveNext();
            curPosLeftHand = (Vector3)stream.ReceiveNext();
            curRotRightHand = (Quaternion)stream.ReceiveNext();
            curPosRightHand = (Vector3)stream.ReceiveNext();
        }
    }
    private void Update()
    {
        if (pv.IsMine)
        {

        }
        else
        {
            FalseHeadRig.rotation = currRotHead;
            FalseHeadRig.position = curPosHead;
            FalseLeftRig.rotation = curRotLeftHand;
            FalseLeftRig.position = curPosLeftHand;
            FalseRightRig.rotation = curRotRightHand;
            FalseRightRig.position = curPosRightHand;

        }
    }
}
//[PunRPC]
//private void ConnectingVRIK(string avatar)
//{

//    //print(GameObject.FindGameObjectsWithTag(avatar)[0].gameObject.transform);
//    vrik.solver.spine.headTarget = GameObject.FindGameObjectsWithTag(avatar)[0].gameObject.transform;
//    vrik.solver.leftArm.target = GameObject.FindGameObjectsWithTag(avatar)[1].gameObject.transform;
//    vrik.solver.rightArm.target = GameObject.FindGameObjectsWithTag(avatar)[2].gameObject.transform;
//}


//print(avatar);
//myavatar = GameObject.FindGameObjectWithTag(avatar);

//print(myavatar);
//myavatar.transform.parent = transform;




//}

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

