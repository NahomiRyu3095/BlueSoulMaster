using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;


    void OnTriggerEnter2D(Collider2D RES)
    {
        player.transform.position = respawnPoint.transform.position;  //Players postion is the same as respawn position
    }
}