using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject WaterShootingEffect;
    public GameObject playerHand;

    public void waterShooting()
    {
        print(04);
        GameObject waterShootingEffect = Instantiate(WaterShootingEffect);
        waterShootingEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f);
        waterShootingEffect.transform.rotation = playerHand.transform.rotation;
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
