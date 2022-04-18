using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign_Animations : MonoBehaviour
{
    float rotSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotSpeed * Time.deltaTime, 0));
    }
}
