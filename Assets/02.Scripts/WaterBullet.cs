using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    public float moveSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        //Vector3.forward 는 월드좌표에서의 앞 방향
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
        //transform.forward 는 자기 자신을 기준으로 앞 방향
        
        Vector3 dir = transform.forward;

        transform.position += (dir * moveSpeed) * Time.deltaTime; 
    }
}
