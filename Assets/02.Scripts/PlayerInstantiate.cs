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
    public float CamrigHigh;
    public Transform rightHandAnchor;

    int count;
    //public GameObject myavatar;


    // CarmeraRig Pivot Transform
    private Transform headTr, leftHandTr, rightHandTr;
    // FalseCamera Rig Transform
    public Transform FalseHeadRig, FalseLeftRig, FalseRightRig, FalseRightHandAnchor;
    public float FalseRigSmooteness = 10f;

    public VRIK vrik;
    public VRIK[] Avrik;
    public CharacterController controller;
    public OVRPlayerController ovrcontroller;

    GameObject[] displayItem;
    public LineRenderer lr;


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
            CameaRig.transform.position = transform.position + new Vector3(0, CamrigHigh, 0);
            rightHandAnchor = GameObject.Find("RightHandAnchor").transform;
            AudioListener.volume = 1;

            avatar = PlayerPrefs.GetString("Avatar");


            //print(avatar);
            //myavatar = GameObject.FindGameObjectWithTag(avatar);

            //print(myavatar);
            //myavatar.transform.parent = transform;

            pv.RPC("AvatarChange", RpcTarget.AllBufferedViaServer, avatar);

            headTr = GameObject.FindGameObjectsWithTag(avatar)[0].transform;
            leftHandTr = GameObject.FindGameObjectsWithTag(avatar)[1].transform;
            rightHandTr = GameObject.FindGameObjectsWithTag(avatar)[2].transform;



        }
        if (!pv.IsMine)
        {

            //Avrik = transform.GetComponentsInChildren<VRIK>();
            //vrik=Avrik[count].GetComponent<VRIK>();

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
                count = i;
                this.avatars[i].SetActive(true);
            }

        }
    }
    void Start()
    {

        if (pv.IsMine)
        {
            vrik = transform.Find(avatar).GetComponent<VRIK>();
            print(GameObject.FindGameObjectsWithTag(avatar)[0].gameObject.transform);
            vrik.solver.spine.headTarget = headTr;
            vrik.solver.leftArm.target = leftHandTr;
            vrik.solver.rightArm.target = rightHandTr;
        }
        else
        {
            vrik = transform.GetComponentInChildren<VRIK>();
            vrik.solver.spine.headTarget = FalseHeadRig;
            vrik.solver.leftArm.target = FalseLeftRig;
            vrik.solver.rightArm.target = FalseRightRig;
        }

        displayItem = GameObject.FindGameObjectsWithTag("DISPLAY_ITEM");


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

    private Quaternion curRotRightHandAnchor;
    private Vector3 curPosRightHandAnchor;
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

            stream.SendNext(rightHandAnchor.rotation);
            stream.SendNext(rightHandAnchor.position);




        }
        else
        {

            currRotHead = (Quaternion)stream.ReceiveNext();
            curPosHead = (Vector3)stream.ReceiveNext();
            curRotLeftHand = (Quaternion)stream.ReceiveNext();
            curPosLeftHand = (Vector3)stream.ReceiveNext();
            curRotRightHand = (Quaternion)stream.ReceiveNext();
            curPosRightHand = (Vector3)stream.ReceiveNext();

            curRotRightHandAnchor = (Quaternion)stream.ReceiveNext();
            curPosRightHandAnchor = (Vector3)stream.ReceiveNext();
        }
    }
    private void Update()
    {
        if (!pv.IsMine)
        {
            //FalseHeadRig.rotation = currRotHead;
            //FalseHeadRig.position = curPosHead;
            FalseHeadRig.rotation = Quaternion.Slerp(FalseHeadRig.rotation, currRotHead, Time.deltaTime * FalseRigSmooteness);
            FalseHeadRig.position = Vector3.Lerp(FalseHeadRig.position, curPosHead, Time.deltaTime * FalseRigSmooteness);
            FalseLeftRig.rotation = Quaternion.Slerp(FalseLeftRig.rotation, curRotLeftHand, Time.deltaTime * FalseRigSmooteness);
            FalseLeftRig.position = Vector3.Lerp(FalseLeftRig.position, curPosLeftHand, Time.deltaTime * FalseRigSmooteness);
            FalseRightRig.rotation = Quaternion.Slerp(FalseRightRig.rotation, curRotRightHand, Time.deltaTime * FalseRigSmooteness);
            FalseRightRig.position = Vector3.Lerp(FalseRightRig.position, curPosRightHand, Time.deltaTime * FalseRigSmooteness);

            FalseRightHandAnchor.rotation = Quaternion.Slerp(FalseRightHandAnchor.rotation, curRotRightHandAnchor, Time.deltaTime * 10f);
            FalseRightHandAnchor.position = Vector3.Lerp(FalseRightHandAnchor.position, curPosRightHandAnchor, Time.deltaTime * 10f);
        }
        else
        {
            Ray();
            pv.RPC("Test", RpcTarget.All);
        }
    }

    void Ray()
    {
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        //if (ControllerManager.instance.GetOculus(ControllerManager.VRKey.Teleport, controller))
        //if (Input.GetKey(KeyCode.T))
        {

            lr.SetPosition(0, rightHandAnchor.position);
            Ray ray = new Ray(rightHandAnchor.position, rightHandAnchor.forward);
            int layer = 1 << LayerMask.NameToLayer("Hand");
            pv.RPC("RPCRay", RpcTarget.Others, 0, FalseRightHandAnchor.position, true);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo, float.MaxValue, ~layer))
            {



                lr.enabled = true;



                bool isHit = Physics.Raycast(ray, out hitinfo, float.MaxValue, ~layer);
                if (isHit)
                {
                    lr.SetPosition(1, hitinfo.point);
                    pv.RPC("RPCRay", RpcTarget.Others, 0, hitinfo.point, true);
                }

                //if (hitinfo.transform.tag == "TUTORIAL_ITEM")
                //{
                //    hitinfo.transform.gameObject.SetActive(false);
                //}
            }
            else // ���
            {
                lr.enabled = true;
                lr.SetPosition(1, ray.origin + ray.direction * 50);
                pv.RPC("RPCRay", RpcTarget.Others, 0, hitinfo.point, true);

            }
        }
        //else if (ControllerManager.instance.GetOculusUp(ControllerManager.VRKey.Teleport, controller))
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            lr.enabled = false;
            pv.RPC("RPCRay", RpcTarget.Others, 0, transform.position, false);

        }
    }

    [PunRPC]
    void RPCRay(int k, Vector3 pos, bool state)
    {
        if (lr == null)
        {
            lr = GetComponent<LineRenderer>();
        }
        lr.enabled = state;
        if (state)
        {
            lr.SetPosition(k, pos);
        }
    }


        [PunRPC]
    public void Test()
    {
        if (pv.IsMine)
        {
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                if (gameObject.GetComponent<CharacterController>().enabled
                    && Inventory.instance != null)
                {
                    if (Inventory.instance.gameObject.activeInHierarchy)
                    {
                        Inventory.instance.gameObject.SetActive(false);
                    }
                    else
                    {
                        Inventory.instance.gameObject.SetActive(true);
                        Inventory.instance.SetWalletInfo();
                    }
                }
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
            {
                foreach (GameObject dItem in displayItem)
                {
                    NFTObject obj = dItem.GetComponent<NFTObject>();
                    if (obj.isGrabbed)
                    {
                        obj.ChangeLeftHandParent();
                        break;
                    }
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                foreach (GameObject dItem in displayItem)
                {
                    NFTObject obj = dItem.GetComponent<NFTObject>();
                    if (obj.isGrabbed)
                    {
                        obj.ChangeRightHandParent();
                        break;
                    }
                }
            }

        }
    }
}


