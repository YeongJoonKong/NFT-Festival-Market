using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyHand : MonoBehaviour
{
    //public OVRInput.Controller controller;
    public LineRenderer lr;
    public Transform hand;
    string avatarName;
    public GameObject[] avatars;
    public TextMeshPro raytext;
    public GameObject rayUI;
    public GameObject rotationUI;


    // Start is called before the first frame update
    void Start()
    {
        print(PlayerPrefs.HasKey("Avatar"));
        print(PlayerPrefs.GetString("Avatar"));
        //lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) 
        {
            GameObject.Find("AvatarDoor").GetComponent<Door>().OpenDoor();
        }

        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        //if (ControllerManager.instance.GetOculus(ControllerManager.VRKey.Teleport, controller))
        //if (Input.GetKey(KeyCode.T))
        {

            lr.SetPosition(0, hand.position);
            Ray ray = new Ray(hand.position, hand.forward);
            int layer = 1 << LayerMask.NameToLayer("Hand");
            RaycastHit hitinfo;
            //floor = null;
            if (Physics.Raycast(ray, out hitinfo, float.MaxValue, ~layer))
            {
                // �ε��� ������Ʈ �±� string���� ����
                avatarName = hitinfo.transform.tag;

                lr.enabled = true;
                
                // if (hitinfo.transform.tag=="AvatarDoor")
                // {
                //     GameObject.Find("AvatarDoor").GetComponent<Door>().OpenDoor();
                // }

                bool isHit = Physics.Raycast(ray, out hitinfo, float.MaxValue, ~layer);
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
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            lr.enabled = false;

            if (!(avatarName == "Avatar1" || avatarName == "Avatar2" || avatarName == "Avatar3" || avatarName == "Avatar4" || avatarName == "Avatar5")) // �Ǵ� �÷��̾���
            {
                return;
            }

            // �׸��� ������ �� ��
            for (int i = 0; i < avatars.Length; i++)
            {
                {
                    avatars[i].SetActive(false);

                }

            }
            //�÷��̾� ������ avatar �迭 ����
            for (int i = 0; i < avatars.Length; i++)
            {
                //�ε����ְ� �ƹ�Ÿ�� �ƴϰų� �÷��̾��� ����
                // �ƹ�Ÿ��� ���ƹ�Ÿ �迭���� �̸������� ��
                if (avatars[i].tag == avatarName)
                {
                    rayUI.SetActive(false);
                    rotationUI.SetActive(true);
                    raytext.text = "오른손 회전 키를 사용하여 뒤를 돌아보세요";
                    avatars[i].SetActive(true);
                    var av = avatars[i].tag;
                    PlayerPrefs.SetString("Avatar", av);

                    print(PlayerPrefs.HasKey("Avatar"));
                    print(PlayerPrefs.GetString("Avatar"));

                    break;
                }
            }
        }
    }
}
