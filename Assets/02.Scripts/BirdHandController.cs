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
        // 만약 물체를 잡았다면
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            if (bomb == null)
            {
                isThrowingCheck = false;

                // 오른손에 폭탄공장에서 폭탄을 만들어서 자식으로 붙이고싶다.
                bomb = Instantiate(bombFactory);
                bomb.transform.position = hand.position;
                bomb.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                bomb.transform.parent = hand;
                // Rigidbody의 isKinemetic를 켜고싶다.
                bomb.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            if (bomb != null)
            {
                isThrowingCheck = true;

                // 폭탄의 Rigidbody의 isKinemetic를 끄고싶다.
                Rigidbody rb = bomb.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                // 오른쪽 컨트롤러의 힘을 폭탄에게 전달하고싶다.
                rb.velocity = OVRInput.GetLocalControllerVelocity(controller) * kAdjustForce;
                rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller) * kAdjustForce;
                // 부모자식관계를 끊고
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
