using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmove : MonoBehaviour
{
    public float speed = 5.0f;
    Transform tr;
    private float h => Input.GetAxis("Horizontal");
    private float v => Input.GetAxis("Vertical");
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector3.forward * Time.deltaTime * speed * v);
        tr.Rotate(Vector3.up * Time.deltaTime * 100.0f * h);
    }
}
