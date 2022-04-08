using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i <= SpawnPoints.Length; i++)
        {
            if(i == Playdata.instance.spawnPointData)
            {
                transform.position = SpawnPoints[i].transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
