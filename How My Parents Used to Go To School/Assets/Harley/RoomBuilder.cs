using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomBuilder : MonoBehaviour
{
    /**
     * Floor cell object
     */
    [SerializeField] GameObject cell;
    /**
     * Fixed height for room
     */
    [SerializeField] int fixedRoomHeight;
    /**
     * The range of room transform
     */
    [SerializeField] Vector2Int roomTransRange;
    /**
     * Max area for a room
     */
    [SerializeField] int maxRoomArea;
    /**
     * Max count for generating rooms
     */
    [SerializeField] int maxRoomCount;
    /**
     * Min for width
     */
    [SerializeField] int minRoomEdge;
    /**
     * Max for length width scale (1-2 in default)
     */
    [SerializeField] int maxLengthWidthScale = 2;

    /**
     * Unit vectors
     */
    Vector3Int Dx = new Vector3Int(1, 0, 0);
    Vector3Int Dy = new Vector3Int(0, 1, 0);
    Vector3Int Dz = new Vector3Int(0, 0, 1);

    /**
     * Tag for floor cells
     */
    const string cellTag = "Floor";

    private MapSystem mapManager;

    private void Awake()
    {
        mapManager = GetComponent<MapSystem>();
    }

    /**
     * Generate rooms
     */
    public IEnumerator GenRooms(Vector3Int centerPos, UnityAction complete)
    {
        int temp = (int)Mathf.Sqrt(maxRoomArea * 1.0f / maxLengthWidthScale);
        minRoomEdge = temp > 3 ? temp : 3;
        for (int i = 0; i <= maxRoomCount; i++)
        {
            int r = Random.Range(1, 2);
            SetGenOneRoom(centerPos, r);
            yield return new WaitForSeconds(0.1f);
        }

        complete();
    }

    /**
     * Romdomly generate one room
     */
    void SetGenOneRoom(Vector3Int centerPos, int r)
    {
        RoomTran roomTran = RanRoomTran(centerPos);
        if (roomTran != null)
        {
            Vector3 roomCenter = new Vector3(roomTran.centerPos.x, 0, roomTran.centerPos.y);

            GameObject temp = new GameObject(r.ToString());
            temp.transform.position = roomCenter;
            temp.tag = cellTag;

            GenOneRoom(roomCenter, roomTran.length, roomTran.width, temp.transform);
            BoxCollider box = temp.AddComponent<BoxCollider>();
            box.size = new Vector3(roomTran.length, fixedRoomHeight, roomTran.width);

            mapManager.GenRooms.Add(roomTran);
            mapManager.UnCrossRooms.Add(roomTran);
        }
    }

    /**
     * Randomly generate a room transform information
     * Includes length, width, and center positon
     */
    RoomTran RanRoomTran(Vector3 centerPos)
    {
        RoomTran roomTran = new RoomTran();

        // Random set length and width within range
        int maxRoomEdge = maxRoomArea / minRoomEdge;
        roomTran.length = Random.Range(minRoomEdge + 1, maxRoomEdge + 1);
        int width = maxRoomArea / roomTran.length;
        roomTran.width = Random.Range(minRoomEdge, width + 1);

        // Random set center position
        roomTran.centerPos = new Vector2Int(Random.Range((int)(centerPos.x - roomTransRange.x * .5f), (int)(centerPos.x + roomTransRange.x * .5f)),
            Random.Range((int)(centerPos.z - roomTransRange.y * .5f), (int)(centerPos.z + roomTransRange.y * .5f)));

        Vector3 roomCenter = new Vector3(roomTran.centerPos.x, 0, roomTran.centerPos.y);

        // Do ray check to avoid overlapping
        if (RayRoomCheck(roomCenter, roomTran.length, roomTran.width))
        {
            return null;
        }
        return roomTran;
    }

    /**
     * Check if a new room is overlapping with existed rooms
     */
    bool RayRoomCheck(Vector3 centerPos, int length, int width)
    {
        bool result = false;

        Vector3 to = new Vector3(length + 1, 0, width + 1) * 0.5f;

        // Get 4 vertexes and 4 midpoints for each edge
        Vector3 v1 = centerPos - to;
        Vector3 v2 = v1 + new Vector3(0, 0, width + 1) * 0.5f;
        Vector3 v3 = v1 + new Vector3(0, 0, width + 1);
        Vector3 v4 = v1 + new Vector3(length * 0.5f + 0.5f, 0, width + 1);
        Vector3 v5 = v1 + to;
        Vector3 v6 = v1 + new Vector3(length + 1, 0, width * 0.5f + 0.5f);
        Vector3 v7 = v1 + new Vector3(length + 1, 0, 0);
        Vector3 v8 = v1 + new Vector3(length + 1, 0, 0) * 0.5f;

        // Check if ray cast hit any room based on each point
        result =
            RayCastCheckHitCell(v1, Dx, length + 1) ||
            RayCastCheckHitCell(v2, Dx, length + 1) ||
            RayCastCheckHitCell(v3, Dx, length + 1) ||

            RayCastCheckHitCell(v7, Dx * -1, length + 1) ||
            RayCastCheckHitCell(v6, Dx * -1, length + 1) ||
            RayCastCheckHitCell(v5, Dx * -1, length + 1) ||

            RayCastCheckHitCell(v1, Dz, width + 1) ||
            RayCastCheckHitCell(v8, Dz, width + 1) ||
            RayCastCheckHitCell(v7, Dz, width + 1) ||

            RayCastCheckHitCell(v3, Dz * -1, width + 1) ||
            RayCastCheckHitCell(v4, Dz * -1, width + 1) ||
            RayCastCheckHitCell(v5, Dz * -1, width + 1);

        return result;
    }

    bool RayCastCheckHitCell(Vector3 v, Vector3 dirction, int distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(v, dirction, out hit, distance))
        {
            if (hit.transform.tag == cellTag)
            {
                return true;
            }
        }
        return false;
    }

    /**
     * Generate one room in scene
     */
    void GenOneRoom(Vector3 centerPos, int length, int width, Transform parent = null)
    {
        Vector3 to = new Vector3(length - 1, 0, width - 1) * 0.5f;

        Vector3 ned = centerPos - to;
        Vector3 fod = centerPos + to;

        Vector3 v1 = new Vector3(ned.x, 0, ned.z);
        Vector3 v2 = new Vector3(ned.x, 0, fod.z);
        Vector3 v3 = new Vector3(fod.x, 0, ned.z);
        Vector3 v4 = new Vector3(fod.x, 0, fod.z);

        InsSetPos(v1, parent);
        InsSetPos(v2, parent);
        InsSetPos(v3, parent);
        InsSetPos(v4, parent);

        InsOneEdge(length, v1, Dx, parent);
        InsOneEdge(length, v2, Dx, parent);
        InsOneEdge(width, v1, Dz, parent);
        InsOneEdge(width, v3, Dz, parent);
    }

    void InsOneEdge(int edge, Vector3 v, Vector3 dir, Transform parent = null)
    {
        for (int i = 1; i < edge - 1; i++)
        {
            InsSetPos(v + i * dir, parent);
        }
    }

    void InsSetPos(Vector3 pos, Transform parent = null)
    {
        var ins = Instantiate(cell);
        ins.transform.position = pos;
        ins.transform.parent = parent;
    }
}
