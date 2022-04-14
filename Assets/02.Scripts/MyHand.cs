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
                // �ε��� ������Ʈ �±� string���� ����
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
            else // ���
            {
                lr.enabled = true;
                lr.SetPosition(1, ray.origin + ray.direction * 50);
            }
        }
        //else if (ControllerManager.instance.GetOculusUp(ControllerManager.VRKey.Teleport, controller))
        else if (OVRInput.GetUp(OVRInput.Button.One,OVRInput.Controller.RTouch))
        {
            lr.enabled = false;


            //�÷��̾� ������ avatar �迭 ����
            for (int i = 0; i < avatars.Length; i++)
            {
                //�ε����ְ� �ƹ�Ÿ�� �ƴϰų� �÷��̾��� ����
                if (!(avatarName == "Cube" || avatarName == "Cylinder" || avatarName == "Capsule")) // �Ǵ� �÷��̾���
                {
                    return;
                }
                // �ƹ�Ÿ��� ���ƹ�Ÿ �迭���� �̸������� ��
                else if (avatars[i].tag == avatarName)
                {
                   
                    avatars[i].SetActive(true);
                    PlayerPrefs.SetString("Avatar", avatars[i].tag);
                }
                // �׸��� ������ �� ��
                else
                {
                    avatars[i].SetActive(false);

                }
            }
        }
    }
}
