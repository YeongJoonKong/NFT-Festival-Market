using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    private Animator animator;
    public Vector3 footOffset;
    [Range(0, 1)]
    public float rightFootPoseWeight = 1;
    [Range(0, 1)]
    public float rightFootRotWeight = 1;
    [Range(0, 1)]
    public float leftFootPoseWeight = 1;
    [Range(0, 1)]
    public float leftFootRotWeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        int layer = 1 << LayerMask.NameToLayer("Hand");
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);
        RaycastHit hit;
          
        bool hashit = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hit,float.MaxValue,~layer);

        if (hashit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPoseWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hit.point+footOffset);

            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        
         hashit = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hit, float.MaxValue, ~layer);

        if (hashit)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPoseWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point+footOffset);

            Quaternion leftfootRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal), hit.normal);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftfootRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }
    }
}
