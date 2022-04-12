using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonIKTransformView : MonoBehaviour,IPunObservable
{
    private Transform tr;
    private Vector3 currPos;

    private Quaternion currRot;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        currPos = tr.position;
        currRot = tr.rotation;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else    // 원격 플레이어의 위치 정보 송신
        {
            currPos = (Vector3)stream.ReceiveNext();
            print(currPos);
            currRot = (Quaternion)stream.ReceiveNext();

        }

    }
    private void Update()
    {
        print(currPos);

        if (tr.root.GetComponent<PhotonView>().IsMine == false)
        {
            tr.position = (Vector3)currPos;
            tr.rotation = (Quaternion)currRot;
        }
    }



}
