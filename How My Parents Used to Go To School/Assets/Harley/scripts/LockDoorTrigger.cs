using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorTrigger : MonoBehaviour
{
    public MapSystem mapSystem;
    public RoomData room;

    void Update()
    {
        if (room.monstersCount == 0)
        {
            mapSystem.roomBuilder.DestroyAllDoors(room);
            if (room.roomType == RoomData.RoomType.FinalRoom)
            {
                // maybe drop something?
                // leave stage
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            mapSystem.roomBuilder.LockRoom(room);
            if(room.roomType == RoomData.RoomType.FinalRoom){
                print("in boss room");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            Destroy(this);
        }
    }
}
