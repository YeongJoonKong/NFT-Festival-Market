using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    public GameObject[] Lights;
    public AudioClip[] sfx;
    public GameObject vfx;

    AudioSource _AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        print(PlayerPrefs.HasKey("Avatar"));
        print(PlayerPrefs.GetString("Avatar"));
        //lr = GetComponent<LineRenderer>();
        _AudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ChooseAvatar();
        OpenDoor();
    }

    void OpenDoor() 
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) || OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) 
        {
            GameObject.Find("AvatarDoor").GetComponent<Door>().OpenDoor();
        }
    }

    void ManageLight(RaycastHit hitInfo)
    {
        for (int i = 1; i <= Lights.Length; i++) 
        {
            if (hitInfo.transform.tag.EndsWith(Convert.ToString(i)))
            {
                if (!_AudioSource.isPlaying && !Lights[i-1].activeInHierarchy) 
                {
                    _AudioSource.volume = 1;
                    _AudioSource.clip = sfx[0];
                    _AudioSource.Play();
                }
                Lights[i - 1].GetComponentInChildren<MeshRenderer>().material.EnableKeyword("_EMISSION");
                Lights[i - 1].transform.Find("Spotlight").gameObject.SetActive(true);
            }
            else
            {
                Lights[i - 1].GetComponentInChildren<MeshRenderer>().material.DisableKeyword("_EMISSION");
                Lights[i - 1].transform.Find("Spotlight").gameObject.SetActive(false);

            }
        }
    }

    void ChooseAvatar()
    {
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
                
                ManageLight(hitinfo);

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
                    _AudioSource.volume = 1;
                    _AudioSource.clip = sfx[1];
                    _AudioSource.Play();
                    StartCoroutine(PlayVFX());
                    var av = avatars[i].tag;
                    PlayerPrefs.SetString("Avatar", av);

                    print(PlayerPrefs.HasKey("Avatar"));
                    print(PlayerPrefs.GetString("Avatar"));

                    break;
                }
            }
        }
    }

    IEnumerator PlayVFX() 
    {
        vfx.SetActive(true);
        yield return new WaitForSeconds(1f);
        vfx.SetActive(false);
    }
}
