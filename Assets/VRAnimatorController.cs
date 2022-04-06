using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatorController : MonoBehaviour
{
   private Animator animator;
   private Vector3 previousPos;
    Vector2 primaryAxis;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Mathf.Clamp(primaryAxis.x, -1, 1);
        Mathf.Clamp(primaryAxis.y, -1, 1);

    }

    // Update is called once per frame
    void Update()
    {
         primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (primaryAxis.x > 0.1f || primaryAxis.y>0.1f|| primaryAxis.x < -0.1f || primaryAxis.y < -0.1f)
        {
        animator.SetBool("isMoving", true);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        animator.SetFloat("DirectionX",primaryAxis.x);
        animator.SetFloat("DirectionY", primaryAxis.y);
       
    }
}
