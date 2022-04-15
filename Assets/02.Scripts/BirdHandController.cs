using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHandController : MonoBehaviour
{
    public GameObject bombFactory;
    GameObject bomb;
    public Transform hand;
    public float kAdjustForce = 3;

    public OVRInput.Controller controller;

    void UpdateThrowBomb()
    {
        // ���� ��ü�� ��Ҵٸ�
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            if (bomb == null)
            {
                // �����տ� ��ź���忡�� ��ź�� ���� �ڽ����� ���̰�ʹ�.
                bomb = Instantiate(bombFactory);
                bomb.transform.position = hand.position;
                bomb.transform.parent = hand;
                // Rigidbody�� isKinemetic�� �Ѱ�ʹ�.
                bomb.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            if (bomb != null)
            {
                // ��ź�� Rigidbody�� isKinemetic�� ����ʹ�.
                Rigidbody rb = bomb.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                // ������ ��Ʈ�ѷ��� ���� ��ź���� �����ϰ�ʹ�.
                rb.velocity = OVRInput.GetLocalControllerVelocity(controller);
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);
                // �θ��ڽİ��踦 ����
                bomb.transform.parent = null;
                bomb = null;
            }
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
