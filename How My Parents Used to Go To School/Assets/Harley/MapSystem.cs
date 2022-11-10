using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{ 
    public MapData mapData;
    public RoomBuilder roomBuilder;

    public List<RoomTran> genRooms = new List<RoomTran>();
    public List<RoomTran> unCrossRooms = new List<RoomTran>();

    public void Start()
    {
        // CrossBuilder = GetComponent<CrossBuilder>();
        roomBuilder = GetComponent<RoomBuilder>();
    }

    void SetSeed(bool bDebug)
    {
        int seed;
        if (bDebug)
        {
            seed = PlayerPrefs.GetInt("Seed");

        }
        else
        {
            seed = (int) System.DateTime.Now.Ticks;
            PlayerPrefs.SetInt("Seed", seed);
        }
        Random.InitState(seed);
    }

   public void RandRoomDatas()
    {
        if (roomBuilder == null || mapData == null)
        {
            return;
        }

        roomBuilder.StartCoroutine(roomBuilder.GenRooms(mapData.mapCenter, () =>
        {
            CreateRoomData();
            RandRoomCrosses();
        }));
    }

    void CreateRoomData()
    {
        for (int i = 1; i < genRooms.Count + 1; i++)
        {
            RoomData rd = new RoomData();
            rd.roomId = i;
            rd.roomTran = genRooms[i - 1];
            rd.roomType = RoomData.RoomType.QuizRoom;
            if (rd.roomId == 1)
                rd.roomType = RoomData.RoomType.FinalRoom;
            rd.crossRooms = new List<RoomData>();
            rd.monsters = new List<GameObject>();
            rd.isEndRoom = false;
            rd.isMainCrossRoom = false;

            mapData.roomDataDic.Add(rd.roomId, rd);
        }

    }

    void RandRoomCrosses()
    {

    }
}
