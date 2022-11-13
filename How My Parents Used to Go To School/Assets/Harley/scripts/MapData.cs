using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public Vector3Int mapCenter;

    public Dictionary<int, RoomData> roomDataDic = new Dictionary<int, RoomData>();

    public List<CrossData> crossDataList = new List<CrossData>();

    public Dictionary<int, List<int>> roomCrossRoomsDic = new Dictionary<int, List<int>>();

    public List<Vector3> crossUnitPos = new List<Vector3>();
}

public struct CrossData
{
    public int id1;
    public int id2;
}
