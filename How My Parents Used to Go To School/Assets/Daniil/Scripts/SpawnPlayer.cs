using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject[] playerSpawns;
    private int numberOfRooms = 4;

    void Start()
    {
        numberOfRooms = playerSpawns.Length;
        CreateRandomRoom();
    }
    private void CreateRandomRoom()
    {
        playerSpawns[Random.Range(0, numberOfRooms)].SetActive(true);
    }
}
