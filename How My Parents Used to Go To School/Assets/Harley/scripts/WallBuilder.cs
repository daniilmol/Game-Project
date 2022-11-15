using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    public GameObject wallUnit;
    public float wallHeight;

    public int cellScale;

    private MapSystem mapSystem;

    private GameObject container;

    private string cellTag;

    Vector3 Dx = new Vector3(1, 0, 0);
    Vector3 Dy = new Vector3(0, 1, 0);
    Vector3 Dz = new Vector3(0, 0, 1);

    private void Start()
    {
        mapSystem = GetComponent<MapSystem>();
        // cellTag = MapSystem.cellTag;
        container = new GameObject("Walls");
        container.transform.position = mapSystem.mapData.mapCenter;
    }

    public IEnumerator GenWalls()
    {
        GameObject[] floors = GameObject.FindGameObjectsWithTag(cellTag);
        foreach (GameObject floor in floors)
        {
            Vector3 pos = floor.transform.position;
            if (!PointCheck(pos + Dx  * cellScale, cellScale / 4))
            {
                InsSetPos(pos + Dx * cellScale * 0.5f, true, container.transform);
            }
            if (!PointCheck(pos - Dx  * cellScale, cellScale / 4))
            {
                InsSetPos(pos - Dx * cellScale * 0.5f, true, container.transform);
            }
            if (!PointCheck(pos + Dz  * cellScale, cellScale / 4))
            {
                InsSetPos(pos + Dz * cellScale * 0.5f, false, container.transform);
            }
            if (!PointCheck(pos - Dz  * cellScale, cellScale / 4))
            {
                InsSetPos(pos - Dz * cellScale * 0.5f, false, container.transform);
            }
            yield return null;
        }
        // var rooms = mapSystem.genRooms;
        // foreach (RoomTran rt in rooms)
        // {

        // }
        // var crossUnitPos = mapSystem.mapData.crossUnitPos;
        // foreach (Vector3 pos in crossUnitPos)
        // {

        // }
    }

    bool PointCheck(Vector3 center, int radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length > 0)
        {
            return true;
        }
        return false;
    }

    bool RayCast(Vector3 ori, Vector3 dir, float mD)
    {
        return mapSystem.RayCast(ori, dir, mD);
    }

    void InsSetPos(Vector3 pos, bool rotate, Transform parent = null)
    {
        Vector3 rotation = rotate ? new Vector3(0, 90, 0) : Vector3.zero;
        var ins = Instantiate(wallUnit);
        ins.transform.position = pos + new Vector3(0, wallHeight * 0.5f, 0);
        ins.transform.eulerAngles = rotation;
        ins.transform.parent = parent;
    }
}
