using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorTrigger : MonoBehaviour
{
    public MapSystem mapSystem;
    public RoomData room;

    void Update()
    {
        if (room.monsters.Count == 0)
        {
            mapSystem.roomBuilder.DestroyAllDoors(room);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            mapSystem.roomBuilder.LockRoom(room);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            Destroy(this);
        }
    }
}
