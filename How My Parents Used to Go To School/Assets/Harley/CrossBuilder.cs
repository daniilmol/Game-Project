using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleCrossType
{
    Break,
    Detour
}

public class CrossBuilder : MonoBehaviour
{
    public GameObject crossUnit;
    public ObstacleCrossType ObstacleCrossType;
    public int cellScale;

    private MapSystem mapSystem;

    private GameObject container;

    Vector3Int Dx = new Vector3Int(1, 0, 0);
    Vector3Int Dy = new Vector3Int(0, 1, 0);
    Vector3Int Dz = new Vector3Int(0, 0, 1);

    private void Start()
    {
        mapSystem = GetComponent<MapSystem>();
        container = new GameObject("CrossPath");
        container.transform.position = mapSystem.mapData.mapCenter;
    }

    public IEnumerator GenCrosses()
    {
        var fr = mapSystem.mainCross.First.Value;
        var lr = mapSystem.mainCross.Last.Value;

        Debug.Log("First: Room" + mapSystem.GetRoomIdByRT(fr));
        Debug.Log("Last: Room" + mapSystem.GetRoomIdByRT(lr));

        foreach (var cd in mapSystem.mapData.crossDataList)
        {
            CrossTwoRoom(cd.id1, cd.id2);
            yield return null;
        }
    }

    void CrossTwoRoom(int id1, int id2)
    {
        var from = mapSystem.mapData.roomDataDic[id1].roomTran;
        var to = mapSystem.mapData.roomDataDic[id2].roomTran;

        SameYCross(from, to);
    }

    Vector3 SameYCross(RoomTran from, RoomTran to)
    {
        var frcp = new Vector3(from.centerPos.x, 0, from.centerPos.y);
        var trcp = new Vector3(to.centerPos.x, 0, to.centerPos.y);

        var sr = Random.value < .5f ? from : to;
        var or = sr == from ? to : from;

        var rangex = CheckCommonSide(frcp.x, from.length, trcp.x, to.length);
        var rangez = CheckCommonSide(frcp.z, from.width, trcp.z, to.width);

        Vector3 pos1;
        Vector3 pos2;

        // no overlap on x
        if (rangex.y > rangex.x)
        {
            // no overlap on y
            if (rangez.y > rangez.x)
            {
                var fxmax = frcp.x + (from.length - cellScale) * .5f - cellScale;
                var fzmax = frcp.z + (from.width - cellScale) * .5f - cellScale;

                // from at left, to at right
                if (fxmax == rangex.x)
                {
                    if (fzmax == rangez.x)
                    {
                        // to at from top right
                        var fe = Random.value < .5f ? fxmax : fzmax;
                        if (fe == fxmax)
                        {
                            pos1 = new Vector3(fe + cellScale * 2, 0, fzmax);
                            pos2 = new Vector3(rangex.y, 0, rangez.y - cellScale * 2);
                            if (pos1.x >= pos2.x)
                            {
                                pos2 = new Vector3(pos1.x + cellScale, 0, pos2.z);
                            }

                            if (pos1.z >= pos2.z)
                            {
                                pos1 = new Vector3(pos1.x, 0, pos2.z - cellScale);
                            }

                            var cp = new Vector3(pos2.x, 0, pos1.z);
                            LShapePos(pos1, pos2, cp);
                        }
                        else
                        {
                            pos1 = new Vector3(fxmax, 0, fe + cellScale * 2);
                            pos2 = new Vector3(rangex.y - cellScale * 2, 0, rangez.y);

                            if (pos1.x >= pos2.x)
                            {
                                pos1 = new Vector3(pos2.x - cellScale, 0, pos1.z);
                            }

                            if (pos1.z >= pos2.z)
                            {
                                pos2 = new Vector3(pos2.x, 0, pos1.z + cellScale);
                            }

                            var cp = new Vector3(pos1.x, 0, pos2.z);
                            LShapePos(pos1, pos2, cp);
                        }
                    }
                    else
                    {
                        // to at from bottom right
                        var fe = Random.value < .5f ? fxmax : rangez.y;
                        if (fe == fxmax)
                        {
                            pos1 = new Vector3(fe + cellScale * 2, 0, rangez.y);
                            pos2 = new Vector3(rangex.y, 0, rangez.x + cellScale * 2);

                            if (pos1.x >= pos2.x)
                            {
                                pos2 = new Vector3(pos1.x + cellScale, 0, pos2.z);
                            }

                            if (pos1.z <= pos2.z)
                            {
                                pos1 = new Vector3(pos1.x, 0, pos2.z + cellScale);
                            }

                            var cp = new Vector3(pos2.x, 0, pos1.z);
                            LShapePos(pos1, pos2, cp);
                        }
                        else
                        {
                            pos1 = new Vector3(rangex.x, 0, fe - cellScale * 2);
                            pos2 = new Vector3(rangex.y - cellScale * 2, 0, rangez.x);

                            if (pos1.x >= pos2.x)
                            {
                                pos1 = new Vector3(pos2.x - cellScale, 0, pos1.z);
                            }

                            if (pos1.z <= pos2.z)
                            {
                                pos2 = new Vector3(pos2.x, 0, pos1.z - cellScale);
                            }

                            var cp = new Vector3(pos1.x, 0, pos2.z);                      
                            LShapePos(pos1, pos2, cp);
                        }
                    }
                }
                else
                {
                    if (fzmax == rangez.x)
                    {
                        // to at from top left
                        var fe = Random.value < .5f ? rangex.y : fzmax;
                        if (fe == fzmax)
                        {
                            pos1 = new Vector3(rangex.y, 0, fe + cellScale * 2);
                            pos2 = new Vector3(rangex.x + cellScale * 2, 0, rangez.y);

                            if (pos1.x <= pos2.x)
                            {
                                pos1 = new Vector3(pos2.x + cellScale, 0, pos1.z);
                            }

                            if (pos1.z >= pos2.z)
                            {
                                pos2 = new Vector3(pos2.x, 0, pos1.z + cellScale);
                            }

                            var cp = new Vector3(pos1.x, 0, pos2.z);
                            LShapePos(pos1, pos2, cp);
                        }
                        else
                        {
                            pos1 = new Vector3(fe - cellScale * 2, 0, rangez.x);
                            pos2 = new Vector3(rangex.x, 0, rangez.y - cellScale * 2);

                            if (pos1.x <= pos2.x)
                            {
                                pos2 = new Vector3(pos1.x - cellScale, 0, pos2.z);
                            }

                            if (pos1.z >= pos2.z)
                            {
                                pos1 = new Vector3(pos1.x, 0, pos2.z - cellScale);
                            }

                            var cp = new Vector3(pos2.x, 0, pos1.z);
                            LShapePos(pos1, pos2, cp);
                        }
                    }
                    else
                    {
                        // to at from bottom left
                        var fe = Random.value < .5f ? rangex.y : rangez.y;
                        if (fe == rangex.y)
                        {
                            pos1 = new Vector3(fe - cellScale * 2, 0, rangez.y);
                            pos2 = new Vector3(rangex.x, 0, rangez.x + cellScale * 2);

                            if (pos1.x <= pos2.x)
                            {
                                pos2 = new Vector3(pos1.x - cellScale, 0, pos2.z);
                            }
                            if (pos1.z <= pos2.z)
                            {
                                pos1 = new Vector3(pos1.x, 0, pos2.z + cellScale);
                            }

                            var cp = new Vector3(pos2.x, 0, pos1.z);
                            LShapePos(pos1, pos2, cp);
                        }
                        else
                        {
                            pos1 = new Vector3(rangex.y, 0, fe - cellScale * 2);
                            pos2 = new Vector3(rangex.x + cellScale * 2, 0, rangez.x);

                            if (pos1.x <= pos2.x)
                            {
                                pos1 = new Vector3(pos2.x + cellScale, 0, pos1.z);
                            }
                            if (pos1.z <= pos2.z)
                            {
                                pos2 = new Vector3(pos2.x, 0, pos1.z - cellScale);
                            }

                            var cp = new Vector3(pos1.x, 0, pos2.z);
                            LShapePos(pos1, pos2, cp);
                        }
                    }
                }
            }
            else
            {
                var rz = EdgeRandom(rangez.y, rangez.x);
                var rx1 = rangex.x + cellScale * 2;
                var rx2 = rangex.y - cellScale * 2;

                pos1 = new Vector3(rx1, 0, rz);
                pos2 = new Vector3(rx2, 0, rz);

                LineTwoPos(pos1, pos2);
            }
        }
        else
        {
            var rx = EdgeRandom(rangex.y, rangex.x);
            var rz1 = rangez.x + cellScale * 2;
            var rz2 = rangez.y - cellScale * 2;

            pos1 = new Vector3(rx, 0, rz1);
            pos2 = new Vector3(rx, 0, rz2);

            LineTwoPos(pos1, pos2);
        }
        return PosOfRoom(pos1, pos2, or);
    }

    Vector3 PosOfRoom(Vector3 pos1, Vector3 pos2, RoomTran room)
    {
        var lmax = room.centerPos.x + (room.length - cellScale) * .5f;
        var lmin = room.centerPos.x - (room.length - cellScale) * .5f;
        var wmax = room.centerPos.y + (room.width - cellScale) * .5f;
        var wmin = room.centerPos.y - (room.width - cellScale) * .5f;

        if (pos1.x >= lmin && pos1.x <= lmax && pos1.z >= wmin && pos1.z <= wmax)
            return pos1;
        else
            return pos2;
    }

    Vector2 CheckCommonSide(float axis1, int edge1, float axis2, int edge2)
    {
        var max1 = axis1 + (edge1 - cellScale) * .5f - cellScale;
        var min1 = axis1 - (edge1 - cellScale) * .5f + cellScale;
        var max2 = axis2 + (edge2 - cellScale) * .5f - cellScale;
        var min2 = axis2 - (edge2 - cellScale) * .5f + cellScale;

        return new Vector2(max1 > max2 ? max2 : max1, min1 > min2 ? min1 : min2);
    }

    float EdgeRandom(float min, float max)
    {
        int cut = 1;
        var diff = max - min;
        if (diff % cellScale == 0)
            cut = cellScale;
        int c = (int)(diff / cut);

        return Random.Range(0, c + 1) * cut + min;
    }

    void LineTwoPos(Vector3 pos1, Vector3 pos2)
    {
        if (pos1.y == pos2.y)
        {
            if (pos1.x == pos2.x)
            {
                var min = pos1.z < pos2.z ? pos1 : pos2;
                var max = min.z == pos1.z ? pos2 : pos1;

                var diff = max.z - min.z;

                if (diff % cellScale == 0)
                {
                    for (float i = min.z; i <= max.z; i += cellScale)
                    {
                        var pos = new Vector3(min.x, min.y, i);

                        if (Mathf.Abs(pos.z - max.z) > 2 * cellScale)
                        {
                            if (RayCast(pos, Dz, cellScale))
                            {
                                var posex = new Vector3(pos.x, pos.y, pos.z + cellScale);
                                var cps = mapSystem.GetExCross(posex, max);
                                switch (ObstacleCrossType)
                                {
                                    case ObstacleCrossType.Break:
                                        CrossBreak(pos, max, cps);
                                        InsSetPos(posex);
                                        break;
                                    case ObstacleCrossType.Detour:
                                        CrossAround(pos, max, cps);
                                        break;
                                }
                                return;
                            }
                        }
                        InsSetPos(pos);
                    }
                }
                else
                {
                    for (float i = min.z; i < max.z; i += cellScale)
                    {
                        var pos = new Vector3(pos1.x, pos1.y, i);

                        if (Mathf.Abs(pos.z - max.z) > 2 * cellScale)
                        {
                            if (RayCast(pos, Dz, cellScale))
                            {
                                var posex = new Vector3(pos.x, pos.y, pos.z + cellScale);
                                var cps = mapSystem.GetExCross(posex, max);
                                switch (ObstacleCrossType)
                                {
                                    case ObstacleCrossType.Break:
                                        CrossBreak(pos, max, cps);
                                        InsSetPos(posex);
                                        break;
                                    case ObstacleCrossType.Detour:
                                        CrossAround(pos, max, cps);
                                        break;
                                }
                                return;
                            }
                        }
                        InsSetPos(pos);
                    }
                    InsSetPos(max);
                }
            }
            else if (pos1.z == pos2.z)
            {
                var min = pos1.x < pos2.x ? pos1 : pos2;
                var max = min.x == pos1.x ? pos2 : pos1;

                var diff = max.x - min.x;

                if (diff % cellScale == 0)
                {
                    for (float i = min.x; i <= max.x; i += cellScale)
                    {
                        var pos = new Vector3(i, min.y, min.z);

                        if (Mathf.Abs(pos.x - max.x) > 2 * cellScale)
                        {
                            if (RayCast(pos, Dx, cellScale))
                            {
                                var posex = new Vector3(pos.x + cellScale, pos.y, pos.z);
                                var cps = mapSystem.GetExCross(posex, max);
                                switch (ObstacleCrossType)
                                {
                                    case ObstacleCrossType.Break:
                                        CrossBreak(pos, max, cps);
                                        InsSetPos(posex);
                                        break;
                                    case ObstacleCrossType.Detour:
                                        CrossAround(pos, max, cps);
                                        break;
                                }
                                return;
                            }
                        }
                        InsSetPos(pos);
                    }
                }
                else
                {
                    for (float i = min.x; i < max.x; i += cellScale)
                    {
                        var pos = new Vector3(i, pos1.y, pos1.z);

                        if (Mathf.Abs(pos.x - max.x) > 2 * cellScale)
                        {
                            if (RayCast(pos, Dx, cellScale))
                            {
                                var posex = new Vector3(pos.x + cellScale, pos.y, pos.z);
                                var cps = mapSystem.GetExCross(posex, max);
                                switch (ObstacleCrossType)
                                {
                                    case ObstacleCrossType.Break:
                                        CrossBreak(pos, max, cps);
                                        InsSetPos(posex);
                                        break;
                                    case ObstacleCrossType.Detour:
                                        CrossAround(pos, max, cps);
                                        break;
                                }
                                return;
                            }
                        }
                        InsSetPos(pos);
                    }
                    InsSetPos(max);
                }
            }
        }
    }

    void LShapePos(Vector3 pos1, Vector3 pos2, Vector3 cp)
    {
        // Check if the middle point is at other rooms
        var rt = mapSystem.GetRoomByPos(cp);
        if (ObstacleCrossType == ObstacleCrossType.Detour && rt != null)
        {
            var to = new Vector2(rt.length + cellScale * 3, rt.width + cellScale * 3) * .5f;

            var ned = rt.centerPos - to;
            var fod = rt.centerPos + to;

            Vector3[] v4 = new Vector3[4];
            v4[0] = new Vector3(ned.x, cp.y, ned.y);
            v4[1] = new Vector3(ned.x, cp.y, fod.y);
            v4[2] = new Vector3(fod.x, cp.y, ned.y);
            v4[3] = new Vector3(fod.x, cp.y, fod.y);

            var minx = pos1.x < pos2.x ? pos1.x : pos2.x;
            var maxx = minx == pos1.x ? pos2.x : pos1.x;
            var minz = pos1.z < pos2.z ? pos1.z : pos2.z;
            var maxz = minz == pos1.z ? pos2.z : pos1.z;

            for (int i = 0; i < v4.Length; i++)
            {
                if (v4[i].x > minx && v4[i].x < maxx && v4[i].z > minz && v4[i].z < maxz)
                {
                    var ncp1 = new Vector3(cp.x, cp.y, v4[i].z);
                    var ncp2 = new Vector3(v4[i].x, cp.y, cp.z);

                    var pos1cp = ncp1.x == pos1.x || ncp1.z == pos1.z ? ncp1 : ncp2;
                    var pos2cp = pos1cp == ncp1 ? ncp2 : ncp1;

                    LShapePos(pos1, v4[i], pos1cp);
                    LShapePos(v4[i], pos2, pos2cp);
                    return;
                }
            }
        }

        LineTwoPos(pos1, cp);
        LineTwoPos(pos2, cp);
    }

    void CrossAround(Vector3 pos, Vector3 max, ExCross cps)
    {
        if (cps.pos1 != Vector3.zero && cps.pos2 != Vector3.zero && cps.pos3 != Vector3.zero)
        {
            LineTwoPos(pos, cps.pos1);
            LineTwoPos(cps.pos1, cps.pos2);
            LineTwoPos(cps.pos2, cps.pos3);
            LineTwoPos(cps.pos3, max);
        }
    }

    void CrossBreak(Vector3 pos, Vector3 max, ExCross cps)
    {
        if (cps.pos1 != Vector3.zero && cps.pos2 != Vector3.zero && cps.pos3 != Vector3.zero)
        {
            InsSetPos(pos);
            LineTwoPos(cps.pos3, max);
        }
    }

    bool RayCast(Vector3 ori, Vector3 dir, float mD)
    {
        return mapSystem.RayCast(ori, dir, mD);
    }

    void InsSetPos(Vector3 pos, Transform parent = null)
    {
        if (container != null)
        {
            parent = container.transform;
        }
        var temp = mapSystem.mapData.crossUnitPos;
        if (!temp.Contains(pos))
        {
            temp.Add(pos);
            mapSystem.crossUnitInsts.Add(mapSystem.InsSetPos(crossUnit, pos, parent));
            mapSystem.mapData.crossUnitPos = temp;
        }
    }
}
