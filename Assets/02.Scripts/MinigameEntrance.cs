using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MinigameEntrance : MonoBehaviour
{
    private OVRManager controller;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && transform.parent.name == ("MiniGame_Mole"))
        {
            Playdata.instance.spawnPointData = 1;
            Playdata.instance.minigameplayed[0] = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Play_Game01KYJ_Wack_A_Mole");

        }
        else if (other.CompareTag("Player") && transform.parent.name == ("MiniGame_Shark"))
        {
            Playdata.instance.spawnPointData = 2;
            Playdata.instance.minigameplayed[1] = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Play_Game02_KYJ_Wack_A_Shark");
        }
         else if (other.CompareTag("Player") && transform.parent.name == ("MiniGame_Bird"))
        {
            Playdata.instance.spawnPointData = 3;
            Playdata.instance.minigameplayed[2] = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Player_Game_KYJ_003_BirdBombBowling");
        }
         else if (other.CompareTag("Player") && transform.parent.name == ("MiniGame_Parachute"))
        {
            Playdata.instance.spawnPointData = 4;
            Playdata.instance.minigameplayed[3] = true;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Player_Game_KYJ_004_ParaglidingTown");
        }
    }
}
