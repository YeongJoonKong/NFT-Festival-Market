using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
public class Environment_AnimControll : MonoBehaviour
{

    [Header("TalkDistance")]
    public float talkdistance = 2f;
    public float nearDistance;

    private float dist;
    private bool onStart;

    private Animator anim;
    private GameObject player;
    private NavMeshAgent agent;
    private Canvas canvas;

    private GameObject checkPoint;
    private Transform[] checkPoints;

    void Start()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");

        canvas = GetComponentInChildren<Canvas>();

        if(transform.name == "Customer")
        {
            anim.SetTrigger("Customer");
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();
            checkPoint = transform.parent.Find("NpcCheckPoint").gameObject;
            checkPoints = checkPoint.GetComponentsInChildren<Transform>();
            agent.destination = checkPoints[Random.Range(1, checkPoints.Length)].position;
            anim.SetBool("Walk", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas != null)
        {
            dist = Vector3.Distance(transform.position, player.transform.position);
            if(dist < talkdistance)
            {
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }

        }
        else
        {
            dist = Vector3.Distance(agent.destination, transform.position);
            if(dist < nearDistance && !onStart)
            {
                onStart = true;
                anim.SetBool("Walk", false);
                StartCoroutine(OnIdle());
            }
            
        }
        
    }
    IEnumerator OnIdle()
    {
        yield return new WaitForSeconds(Random.Range(3, 7));
        anim.SetBool("Walk", true);
        onStart = false;
        agent.destination = checkPoints[Random.Range(1, checkPoints.Length)].position;

    }
}
