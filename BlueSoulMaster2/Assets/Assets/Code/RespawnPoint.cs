using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    //caida parte 1
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    //caida parte 2
    [SerializeField] private Transform respawnPoint02;

    void OnTriggerEnter2D(Collider2D RES)
    {

        if (RES.gameObject.CompareTag("Plataforma"))
        {
            player.transform.position = respawnPoint.transform.position;  //Players postion is the same as respawn position

        }

        if (RES.gameObject.CompareTag("Plataforma02"))
        {
            player.transform.position = respawnPoint02.transform.position;  //Players postion is the same as respawn position

        }
    }
}