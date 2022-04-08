using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEntrance : MonoBehaviour
{
    private OVRManager controller;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && transform.parent.name == ("MiniGame_Mole"))
        {
            Playdata.instance.spawnPointData = 1;
            SceneManager.LoadScene("Player_Game_KYJ_001_Wack A Mole");
        }
    }
}
