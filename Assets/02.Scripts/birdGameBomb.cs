using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdGameBomb : MonoBehaviour
{
    public float radius = 3;
    //public GameObject explosionFactory;

    float birdCurrentTime;
    float birdWaitingTime = 5;

    public void BirdBooming()
    {
        birdCurrentTime += Time.deltaTime;

        if(birdCurrentTime > birdWaitingTime)
        {
            //GameObject birdExplosion = Instantiate(explosionFactory);
            //birdExplosion.transform.position = transform.position;

            int layerMask = 1 << LayerMask.NameToLayer("Bird");
            Collider[] cols = Physics.OverlapSphere(transform.position, radius, layerMask);

            for(int i = 0; i < cols.Length; i++)
            {
                cols[i].GetComponent<birdBombBird>().ExplodeBomb();
            }
            Destroy(gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BirdBooming();
    }
}
