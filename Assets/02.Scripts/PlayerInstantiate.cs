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
    public float FalseRigSmooteness = 10f;

    public VRIK vrik;
    public VRIK[] Avrik;
    public CharacterController controller;
    public OVRPlayerController ovrcontroller;



    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        pv = GetComponent<PhotonView>();
        //vrik = GetComponentInChildren<VRIK>();
        controller = GetComponent<CharacterController>();
        ovrcontroller = GetComponent<OVRPlayerController>();
        if (pv.IsMine)
        {
            CameaRig = GameObject.Find("OVRCameraRigNetWork");

            CameaRig.transform.parent = transform;

            avatar = PlayerPrefs.GetString("Avatar");
       

            //print(avatar);
            //myavatar = GameObject.FindGameObjectWithTag(avatar);

            //print(myavatar);
            //myavatar.transform.parent = transform;

            pv.RPC("AvatarChange", RpcTarget.AllBufferedViaServer, avatar);

            headTr = GameObject.FindGameObjectsWithTag(avatar)[0].transform;
            leftHandTr = GameObject.FindGameObjectsWithTag(avatar)[1].transform;
            rightHandTr = GameObject.FindGameObjectsWithTag(avatar)[2].transform;
            vrik = transform.Find(avatar).GetComponent<VRIK>();
            

        }
        if (!pv.IsMine)
        {
            Avrik = transform.GetComponentsInChildren<VRIK>();
            for(int i=0; i<Avrik.Length; i++)
            {
                if(Avrik[i].gameObject.activeSelf)
                {
                    vrik = Avrik[i];
                }
            }

            //vrik = transform.GetComponentInChildren<VRIK>();
            controller.enabled = false;
            ovrcontroller.enabled = false;
        }
        PhotonNetwork.IsMessageQueueRunning = true;
    }
    [PunRPC]
    private void AvatarChange(string avatar)
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            {
                avatars[i].SetActive(false);

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


    }
    //머리동기화변수
    private Quaternion currRotHead;
    private Vector3 curPosHead;
    //왼손동기화변수
    private Quaternion curRotLeftHand;
    private Vector3 curPosLeftHand;

    //오른손동기화변수
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
            //FalseHeadRig.rotation = currRotHead;
            //FalseHeadRig.position = curPosHead;
            FalseHeadRig.rotation = Quaternion.Slerp(FalseHeadRig.rotation, currRotHead, Time.deltaTime * FalseRigSmooteness);
            FalseHeadRig.position = Vector3.Lerp(FalseHeadRig.position, curPosHead, Time.deltaTime * FalseRigSmooteness);
            FalseLeftRig.rotation = Quaternion.Slerp(FalseLeftRig.rotation, curRotLeftHand, Time.deltaTime * FalseRigSmooteness);
            FalseLeftRig.position = Vector3.Lerp(FalseLeftRig.position, curPosLeftHand, Time.deltaTime * FalseRigSmooteness);
            FalseRightRig.rotation = Quaternion.Slerp(FalseRightRig.rotation, curRotRightHand, Time.deltaTime * FalseRigSmooteness);
            FalseRightRig.position = Vector3.Lerp(FalseRightRig.position, curPosRightHand, Time.deltaTime * FalseRigSmooteness);

        }
    }
}


