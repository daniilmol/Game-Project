using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorTrigger : MonoBehaviour
{
    public MapSystem mapSystem;
    private RoomData room;
    // Start is called before the first frame update
    void Start()
    {
        mapSystem = GetComponent<MapSystem>();
        var rt = mapSystem.GetRoomByPos(this.transform.position);
        foreach (var rd in mapSystem.mapData.roomDataDic)
        {
            if (rd.Value.roomTran == rt)
            {
                room = rd.Value;
            }
        }
    }

    // Update is called once per frame
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
