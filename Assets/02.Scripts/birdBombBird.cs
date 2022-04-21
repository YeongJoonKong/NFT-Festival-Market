using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBombBird : MonoBehaviour
{
    //public GameObject birdExplosionFactory;

    public void ExplodeBomb()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        Vector3 dir = UnityEngine.Random.insideUnitSphere;
        dir.y = Mathf.Abs(dir.y) * 10;
        dir.Normalize();
        rb.AddForce(dir * 10, ForceMode.Impulse);
        rb.AddTorque(dir * 20, ForceMode.Impulse);

        Invoke("TakeShot", 1.2f);
    }

    public void TakeShot()
    {
        //GameObject explosion = Instantiate(birdExplosionFactory);
        //explosion.transform.position = transform.position;
        Destroy(gameObject);
        birdScoreManager.instance.AddScore(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
