using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdGameBomb : MonoBehaviour
{
    public float radius = 3;

    float bombCurrentTime;
    float bombWaitingTime = 3;

    //public GameObject explosionFactory;

    public int BombCounting;

    public BirdHandController birdHandController;

    public void takeExplode()
    {
        BombCounting++;

        //GameObject birdExplosion = Instantiate(explosionFactory);
        //birdExplosion.transform.position = transform.position;

        if(birdHandController.isThrowingCheck == true)
        {
            bombCurrentTime += Time.deltaTime;
            if (bombCurrentTime >= bombWaitingTime)
            {
                int layerMask = 1 << LayerMask.NameToLayer("Bird");
                Collider[] cols = Physics.OverlapSphere(transform.position, radius, layerMask);

                for (int i = 0; i < cols.Length; i++)
                {
                    Rigidbody rbBird = cols[i].GetComponent<Rigidbody>();
                    rbBird.isKinematic = false;

                    cols[i].GetComponent<birdBombBird>().ExplodeBomb();
                }
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        birdHandController = GetComponent<BirdHandController>();
    }

    // Update is called once per frame
    void Update()
    {
        takeExplode();
    }
}
