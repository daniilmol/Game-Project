using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{ 
    private MapData mapData;
    private RoomBuilder roomBuilder;

   public void RandRoomDatas()
    {
        if (roomBuilder == null || mapData == null)
        {
            return;
        }

        RoomBuilder.StartCoroutine(RoomBuilder.GenRooms(mapData.mapCenter, () =>
        {
            CreateRoomData();
            RandRoomCrosses();
        }));
    }

    void CreateRoomData()
    {
        RoomData rd = new RoomData();
        rd.roomId = 1;

    }

    void RandRoomCrosses()
    {

    }
}
