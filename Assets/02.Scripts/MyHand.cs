using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHand : MonoBehaviour
{
    //public OVRInput.Controller controller;
    public LineRenderer lr;
    public Transform hand;
    string avatarName;
    public GameObject[] avatars;

    // Start is called before the first frame update
    void Start()
    {
        
        //lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One,OVRInput.Controller.RTouch))
        //if (ControllerManager.instance.GetOculus(ControllerManager.VRKey.Teleport, controller))
        //if (Input.GetKey(KeyCode.T))
        {

            lr.SetPosition(0, hand.position);
            Ray ray = new Ray(hand.position, hand.forward);
            int layer = 1 << LayerMask.NameToLayer("Hand");
            RaycastHit hitinfo;
            //floor = null;
            if (Physics.Raycast(ray, out hitinfo, float.MaxValue,~layer))
            {
                // 부딪힌 오브젝트 태그 string으로 담음
                avatarName = hitinfo.transform.tag;

                lr.enabled = true;
                if (hitinfo.transform.tag=="AvatarDoor")
                {
                    GameObject.Find("AvatarDoor").GetComponent<Door>().OpenDoor();
                }

                bool isHit = Physics.Raycast(ray, out hitinfo,float.MaxValue, ~layer);
                if (isHit)
                {
                    lr.SetPosition(1, hitinfo.point);
                }
            }
            else // 허공
            {
                lr.enabled = true;
                lr.SetPosition(1, ray.origin + ray.direction * 50);
            }
        }
        //else if (ControllerManager.instance.GetOculusUp(ControllerManager.VRKey.Teleport, controller))
        else if (OVRInput.GetUp(OVRInput.Button.One,OVRInput.Controller.RTouch))
        {
            lr.enabled = false;


            //플레이어 하위에 avatar 배열 넣음
            for (int i = 0; i < avatars.Length; i++)
            {
                //부딪힌애가 아바타가 아니거나 플레이어라면 리턴
                if (!(avatarName == "Cube" || avatarName == "Cylinder" || avatarName == "Capsule")) // 또는 플레이어라면
                {
                    return;
                }
                // 아바타라면 내아바타 배열에서 이름같은놈 켜
                else if (avatars[i].tag == avatarName)
                {
                   
                    avatars[i].SetActive(true);
                    PlayerPrefs.SetString("Avatar", avatars[i].tag);
                }
                // 그리고 나머지 다 꺼
                else
                {
                    avatars[i].SetActive(false);

                }
            }
        }
    }
}
