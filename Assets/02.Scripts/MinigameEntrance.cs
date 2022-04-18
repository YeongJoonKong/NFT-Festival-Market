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
            Playdata.instance.minigameplayed[0] = true;
            SceneManager.LoadScene("Play_Game01KYJ_Wack_A_Mole");
        }
        else if (other.CompareTag("Player") && transform.parent.name == ("MiniGame_Shark"))
        {
            Playdata.instance.spawnPointData = 2;
            Playdata.instance.minigameplayed[1] = true;
            SceneManager.LoadScene("Play_Game02_KYJ_Wack_A_Shark");
        }
    }
}
