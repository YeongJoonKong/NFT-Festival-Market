using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdGameBomb : MonoBehaviour
{
    public float radius = 5f;

    float bombCurrentTime;
    float bombWaitingTime = 3;

    public GameObject explosionFactory;

    public int BombCounting;

    public void takeExplode()
    {
        BombCounting++;

        if(BirdHandController.Instance.isThrowingCheck == true)
        {
            bombCurrentTime += Time.deltaTime;
            if (bombCurrentTime >= bombWaitingTime)
            {
                GameObject birdExplosion = Instantiate(explosionFactory);
                birdExplosion.transform.position = transform.position;

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
        else
        {
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        takeExplode();
    }
}
