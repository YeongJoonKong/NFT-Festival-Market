using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public GameObject WaterShootingEffect;
    public GameObject playerHand;

    public void waterShooting()
    {
        GameObject waterShootingEffect = Instantiate(WaterShootingEffect);
        waterShootingEffect.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 0.2f);
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
