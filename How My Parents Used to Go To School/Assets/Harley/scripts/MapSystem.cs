using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{ 
    public MapData mapData;
    public RoomBuilder roomBuilder;
    public CrossBuilder crossBuilder;

    public GameObject playerPref;

    public List<RoomTran> genRooms = new List<RoomTran>();
    public List<RoomTran> unCrossRooms = new List<RoomTran>();

    private RoomTran firstRoom;
    private RoomTran lastRoom;

    public float anotherCrossProbability = .2f;
    public float deadCrossProbability = .5f;
    public float deadAwayProbability = .6f;

    List<RoomTran> deadRooms = new List<RoomTran>();
    Dictionary<RoomTran, List<RoomTran>> roomCrossRooms = new Dictionary<RoomTran, List<RoomTran>>();

    // The main cross of map
    public LinkedList<RoomTran> mainCross = new LinkedList<RoomTran>();

    List<RoomTran> endRooms = new List<RoomTran>();

    [HideInInspector]
    public List<GameObject> crossUnitInsts = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> roomUnitInsts = new List<GameObject>();

    const string roomTag = "Room";
    const string wallTag = "Wall";

    public void Start()
    {
        crossBuilder = GetComponent<CrossBuilder>();
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
                rd.roomType = RoomData.RoomType.StartRoom;
            rd.crossRooms = new List<RoomData>();
            rd.monsters = new List<GameObject>();
            rd.isEndRoom = false;
            rd.isMainCrossRoom = false;

            mapData.roomDataDic.Add(rd.roomId, rd);
        }

    }

    void RandRoomCrosses()
    {
        if (genRooms.Count <= 0) return;

        firstRoom = genRooms[0];

        CalNextCross(firstRoom);
    }

    void UpdateMapData()
    {
        foreach (var rd in mapData.roomDataDic)
        {
            RoomTran rt = rd.Value.roomTran;
            if (roomCrossRooms.ContainsKey(rt))
            {
                var temp = new List<int>();
                foreach (var crt in roomCrossRooms[rt])
                {
                    var id = GetRoomIdByRT(crt);
                    if (id > 0)
                    {
                        temp.Add(id);

                        rd.Value.crossRooms.Add(mapData.roomDataDic[id]);

                        var cd = new CrossData();
                        cd.id1 = rd.Key;
                        cd.id2 = id;
                        if (!CrossDataContains(cd))
                            mapData.crossDataList.Add(cd);
                    }
                }
                mapData.roomCrossRoomsDic.Add(rd.Key, temp);
            }

            if (mainCross.Contains(rt))
            {
                rd.Value.isMainCrossRoom = true;
                if (endRooms.Contains(rt))
                {
                    rd.Value.roomType = RoomData.RoomType.FinalRoom;
                }
            }

            if (endRooms.Contains(rt))
            {
                rd.Value.isEndRoom = true;
            }
        }
    }

    bool CrossDataContains(CrossData d)
    {
        foreach (var cd in mapData.crossDataList)
        {
            if ((cd.id1 == d.id1 && cd.id2 == d.id2) || (cd.id1 == d.id2 && cd.id2 == d.id1))
                return true;
        }
        return false;
    }

    public int GetRoomIdByRT(RoomTran rt)
    {
        foreach (var rd in mapData.roomDataDic)
        {
            if (rd.Value.roomTran == rt)
                return rd.Key;
        }
        return -1;
    }

    void CalNextCross(RoomTran nr, RoomTran br = null)
    {
        mainCross.AddLast(nr);
        LRemove(unCrossRooms, nr);

        var fcl = FindLatelyRooms(nr);

        if (fcl.firstLately != null)
        {
            //CrossGenCtrl.CrossTwoRoom(nr, fcl.FirstLately);
            //UnCrossRooms.Remove(fcl.FirstLately);

            if (fcl.secondLately != null)
            {
                if (Random.value < anotherCrossProbability)
                {
                    //CrossGenCtrl.CrossTwoRoom(nr, fcl.SecondLately);
                    //UnCrossRooms.Remove(fcl.SecondLately);

                    if (Random.value < deadCrossProbability)
                    {
                        var dr = Random.value < .5f ? fcl.firstLately : fcl.secondLately;
                        var lr = dr == fcl.firstLately ? fcl.secondLately : fcl.firstLately;

                        //DeadRooms.Add(dr);
                        LAdd(deadRooms, dr);

                        //RoomCrossRooms.Add(nr, new List<RoomTran>() { lr });
                        AddItemToRoomDic(nr, lr, dr, br);
                        //MainCross.AddLast(lr);
                        LRemove(unCrossRooms, dr);
                        LRemove(unCrossRooms, lr);

                        CalDeadCross(dr, nr);
                        CalNextCross(lr, nr);
                    }
                    else
                    {
                        var mr = Random.value < .5f ? fcl.firstLately : fcl.secondLately;
                        var or = mr == fcl.firstLately ? fcl.secondLately : fcl.firstLately;
                        AddItemToRoomDic(or, nr);
                        AddItemToRoomDic(nr, mr, or, br);
                        CalNextCross(mr, nr);
                    }
                }
                else
                {
                    AddItemToRoomDic(nr, br, fcl.firstLately);
                    CalNextCross(fcl.firstLately, nr);
                }
            }
            else
            {
                AddItemToRoomDic(nr, br, fcl.firstLately);
                CalNextCross(fcl.firstLately, nr);
            }
        }
        else
        {
            lastRoom = nr;
            AddItemToRoomDic(nr, br);
            LAdd(endRooms, nr);

            Debug.Log("Generate Room Number: " + genRooms.Count);
            Debug.Log("Main Cross Number: " + mainCross.Count);
            Debug.Log("Dead Rooms Number: " + deadRooms.Count);
            Debug.Log("End Rooms Number: " + endRooms.Count);

            UpdateMapData();
            crossBuilder.StartCoroutine(crossBuilder.GenCrosses(() =>
            {
                ClearCrossPath();
                BuildRoom();
                SetPlayer();
            }));
        }
    }

    CrossLately FindLatelyRooms(RoomTran tar)
    {
        var cl = new CrossLately();
        float firstSqrdis = Mathf.Infinity;
        float secondSqrdis = Mathf.Infinity;

        foreach (var room in unCrossRooms)
        {
            var rc = new Vector3(room.centerPos.x, 0, room.centerPos.y);
            var tc = new Vector3(tar.centerPos.x, 0, tar.centerPos.y);
            float sqrdis = (rc - tc).sqrMagnitude;

            if (sqrdis < firstSqrdis)
            {
                firstSqrdis = sqrdis;
                cl.firstLately = room;
            }
            else if (sqrdis < secondSqrdis)
            {
                secondSqrdis = sqrdis;
                cl.secondLately = room;
            }
        }
        return cl;
    }

    void CalDeadCross(RoomTran fdr, RoomTran bdr)
    {
        var temp = FindLatelyRooms(fdr);
        if (temp.firstLately != null && Random.value < deadAwayProbability)
        {
            //CrossGenCtrl.CrossTwoRoom(fdr, temp.FirstLately);
            LRemove(unCrossRooms, temp.firstLately);
            LAdd(deadRooms, temp.firstLately);
            AddItemToRoomDic(fdr, temp.firstLately, bdr);
            CalDeadCross(temp.firstLately, fdr);
        }
        else
        {
            // Dead the cross
            LRemove(unCrossRooms, fdr);
            LAdd(deadRooms, fdr);
            AddItemToRoomDic(fdr, bdr);
            LAdd(endRooms, fdr);
        }
    }

    void LRemove<T>(List<T> list, T item)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
        }
    }

    void LAdd<T>(List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
        }
    }

    void AddItemToRoomDic(RoomTran key, RoomTran item1, RoomTran item2 = null, RoomTran item3 = null)
    {
        if (roomCrossRooms.ContainsKey(key))
        {
            if (item1 != null)
                if (!roomCrossRooms[key].Contains(item1))
                    roomCrossRooms[key].Add(item1);
            if (item2 != null)
                if (!roomCrossRooms[key].Contains(item2))
                    roomCrossRooms[key].Add(item2);
            if (item3 != null)
                if (!roomCrossRooms[key].Contains(item3))
                    roomCrossRooms[key].Add(item3);
        }
        else
        {
            roomCrossRooms.Add(key, new List<RoomTran>());
            AddItemToRoomDic(key, item1, item2, item3);
        }
    }

    public bool RayCast(Vector3 ori, Vector3 dir, float mD)
    {
        Ray ray = new Ray(ori, dir);
        RaycastHit[] hits = Physics.RaycastAll(ray, mD);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.tag == roomTag)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject InsSetPos(GameObject prefab, Vector3 pos, Transform parent = null)
    {
        var ins = Instantiate(prefab);
        ins.transform.position = pos;
        ins.transform.parent = parent;
        return ins;
    }

    public RoomTran GetRoomByPos(Vector3 pos)
    {
        foreach (var room in genRooms)
        {
            var to = new Vector3(room.length, 0, room.width) * .5f;

            var centerPos = new Vector3(room.centerPos.x, 0, room.centerPos.y);
            var ned = centerPos - to;
            var fod = centerPos + to;

            if (pos.x >= ned.x && pos.x <= fod.x && pos.y >= ned.y && pos.y <= fod.y && pos.z >= ned.z && pos.z <= fod.z)
            {
                return room;
            }
        }
        return null;
    }

    public ExCross GetExCross(Vector3 strPos, Vector3 tarPos)
    {
        var scale = crossBuilder.cellScale;
        var ec = new ExCross();
        var rt = GetRoomByPos(strPos);
        if (rt != null)
        {
            var to = new Vector3(rt.length + scale, 0, rt.width + scale) * .5f;

            var centerPos = new Vector3(rt.centerPos.x, 0, rt.centerPos.y);
            var ned = centerPos - to;
            var fod = centerPos + to;
            if (strPos.x == tarPos.x)
            {
                if (Random.value < .5f)
                {
                    ec.pos1 = strPos.z < tarPos.z ? new Vector3(ned.x - scale, strPos.y, strPos.z - scale  * 2) : new Vector3(ned.x - scale, strPos.y, strPos.z + scale * 2);
                    ec.pos2 = strPos.z < tarPos.z ? ec.pos1 + new Vector3(0, 0, rt.width + scale * 3) : ec.pos1 - new Vector3(0, 0, rt.width + scale * 3);
                    ec.pos3 = new Vector3(strPos.x, strPos.y, ec.pos2.z);
                }
                else
                {
                    ec.pos1 = strPos.z < tarPos.z ? new Vector3(fod.x + scale, strPos.y, strPos.z - scale * 2) : new Vector3(fod.x + scale, strPos.y, strPos.z + scale * 2);
                    ec.pos2 = strPos.z < tarPos.z ? ec.pos1 + new Vector3(0, 0, rt.width + scale * 3) : ec.pos1 - new Vector3(0, 0, rt.width + scale * 3);
                    ec.pos3 = new Vector3(strPos.x, strPos.y, ec.pos2.z);
                }

            }
            else if (strPos.z == tarPos.z)
            {
                if (Random.value < .5f)
                {
                    ec.pos1 = strPos.x < tarPos.x ? new Vector3(strPos.x - scale * 2, strPos.y, ned.z - scale) : new Vector3(strPos.x + scale * 2, strPos.y, ned.z - scale);
                    ec.pos2 = strPos.x < tarPos.x ? ec.pos1 + new Vector3(rt.length + scale * 3, 0, 0) : ec.pos1 - new Vector3(rt.length + scale * 3, 0, 0);
                    ec.pos3 = new Vector3(ec.pos2.x, strPos.y, strPos.z);
                }
                else
                {
                    ec.pos1 = strPos.x < tarPos.x ? new Vector3(strPos.x - scale * 2, strPos.y, fod.z + scale) : new Vector3(strPos.x + scale * 2, strPos.y, fod.z + scale);
                    ec.pos2 = strPos.x < tarPos.x ? ec.pos1 + new Vector3(rt.length + scale * 3, 0, 0) : ec.pos1 - new Vector3(rt.length + scale * 3, 0, 0);
                    ec.pos3 = new Vector3(ec.pos2.x, strPos.y, strPos.z);
                }
            }
        }
        return ec;
    }

    void ClearCrossPath()
    {
        foreach (var cp in mapData.crossPosList)
        {
            float dist = Vector3.Distance(cp.start, cp.end);
            RaycastHit[] hits = Physics.RaycastAll(cp.start, cp.end - cp.start, dist);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.tag == wallTag)
                {
                    Destroy(hits[i].collider.gameObject);
                }
            }
        }

        foreach (var rd in mapData.roomDataDic)
        {
            roomBuilder.DestroyAllDoors(rd.Value);
        }
    }

    void BuildRoom()
    {
        foreach (var rd in mapData.roomDataDic)
        {
            roomBuilder.BuildEnvironment(rd.Value);
        }
    }

    void SetPlayer()
    {
        var player = Instantiate(playerPref);
        Vector2 center = firstRoom.centerPos;
        player.transform.position = new Vector3(center.x, 0, center.y);
    }
}

public struct CrossLately
{
    public RoomTran firstLately;
    public RoomTran secondLately;
}

public struct ExCross
{
    public Vector3 pos1;
    public Vector3 pos2;
    public Vector3 pos3;
}
