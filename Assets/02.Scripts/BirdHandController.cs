using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdHandController : MonoBehaviour
{
    public static BirdHandController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject bombFactory;
    GameObject bomb;
    public Transform hand;
    public float kAdjustForce = 4;

    public bool isThrowingCheck;

    public OVRInput.Controller controller;
    void UpdateThrowBomb()
    {
        // ���� ��ü�� ��Ҵٸ�
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            if (bomb == null)
            {
                isThrowingCheck = false;

                // �����տ� ��ź���忡�� ��ź�� ���� �ڽ����� ���̰�ʹ�.
                bomb = Instantiate(bombFactory);
                bomb.transform.position = hand.position;
                bomb.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                bomb.transform.parent = hand;
                // Rigidbody�� isKinemetic�� �Ѱ�ʹ�.
                bomb.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            if (bomb != null)
            {
                isThrowingCheck = true;

                // ��ź�� Rigidbody�� isKinemetic�� ����ʹ�.
                Rigidbody rb = bomb.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                // ������ ��Ʈ�ѷ��� ���� ��ź���� �����ϰ�ʹ�.
                rb.velocity = OVRInput.GetLocalControllerVelocity(controller) * kAdjustForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller) * kAdjustForce;
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
        UpdateThrowBomb();
    }
}
