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
     * The scale of cell object
     */
    [SerializeField] int cellScale;
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
     * The size of start room
     */
    [SerializeField] int startRoomSize;
    /**
     * Min for width
     */
    private int minRoomEdge;
    /**
     * Max for length width scale (1-2 in default)
     */
    [SerializeField] float maxLengthWidthScale = 1.5f;

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
        SetGenStartRoom(centerPos);
        yield return new WaitForSeconds(0.1f);
        int temp = (int)Mathf.Sqrt(maxRoomArea * 1.0f / maxLengthWidthScale);
        minRoomEdge = temp > 3 ? temp : 3;
        for (int i = 0; i <= maxRoomCount; i++)
        {
            SetGenOneRoom(centerPos);
            yield return new WaitForSeconds(0.1f);
        }

        complete();
    }

    /**
     * Generate start room
     */
    void SetGenStartRoom(Vector3Int centerPos)
    {
        RoomTran roomTran = new RoomTran();
        roomTran.length = startRoomSize * cellScale;
        roomTran.width = startRoomSize * cellScale;
        roomTran.centerPos = new Vector2Int(Random.Range((int)(centerPos.x - roomTransRange.x * .5f), (int)(centerPos.x + roomTransRange.x * .5f)) * cellScale,
            Random.Range((int)(centerPos.z - roomTransRange.y * .5f), (int)(centerPos.z + roomTransRange.y * .5f)) * cellScale);
        Vector3 roomCenter = new Vector3(roomTran.centerPos.x, 0, roomTran.centerPos.y);
        GameObject temp = new GameObject("Room1");
        temp.transform.position = roomCenter;
        temp.tag = cellTag;
        GenOneRoom(roomCenter, roomTran.length, roomTran.width, temp.transform);
        var box = temp.AddComponent<BoxCollider>();
        box.size = new Vector3(roomTran.length, 1, roomTran.width);
        box.isTrigger = true;
        mapManager.genRooms.Add(roomTran);
        mapManager.unCrossRooms.Add(roomTran);
    }

    /**
     * Romdomly generate one room
     */
    void SetGenOneRoom(Vector3Int centerPos)
    {
        RoomTran roomTran = RanRoomTran(centerPos);
        if (roomTran != null)
        {
            Vector3 roomCenter = new Vector3(roomTran.centerPos.x, 0, roomTran.centerPos.y);

            GameObject temp = new GameObject("Room" + (mapManager.genRooms.Count + 1).ToString());
            temp.transform.position = roomCenter;
            temp.tag = cellTag;

            GenOneRoom(roomCenter, roomTran.length, roomTran.width, temp.transform);
            var box = temp.AddComponent<BoxCollider>();
            box.size = new Vector3(roomTran.length, 1, roomTran.width);
            box.isTrigger = true;

            mapManager.genRooms.Add(roomTran);
            mapManager.unCrossRooms.Add(roomTran);
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
        int length = Random.Range(minRoomEdge + 1, maxRoomEdge + 1);
        int width = Random.Range(minRoomEdge, maxRoomArea / length + 1);
        roomTran.length = length * cellScale;
        roomTran.width = width * cellScale;

        // Random set center position
        roomTran.centerPos = new Vector2Int(Random.Range((int)(centerPos.x - roomTransRange.x * .5f), (int)(centerPos.x + roomTransRange.x * .5f)) * cellScale,
            Random.Range((int)(centerPos.z - roomTransRange.y * .5f), (int)(centerPos.z + roomTransRange.y * .5f)) * cellScale);
        
        if (length % 2 != 0)
        {
            roomTran.centerPos -= new Vector2Int(cellScale / 2, 0);
        }

        if (width % 2 != 0)
        {
            roomTran.centerPos -= new Vector2Int(0, cellScale / 2);
        }

        Vector3 roomCenter = new Vector3(roomTran.centerPos.x, 0, roomTran.centerPos.y);

        // Do ray check to avoid overlapping
        if (RoomOverlapCheck(roomCenter, roomTran.length, roomTran.width))
        {
            return null;
        }
        return roomTran;
    }

    /**
     * Check if a new room is overlapping with existed rooms
     */
    bool RoomOverlapCheck(Vector3 centerPos, int length, int width)
    {
        bool result = false;

        int lengthBorder = length + cellScale;
        int widthBorder = width + cellScale;

        Vector3 to = new Vector3(lengthBorder, 0, widthBorder) * 0.5f;

        // Get 4 vertexes and 4 midpoints for each edge
        Vector3 v1 = centerPos - to;
        Vector3 v2 = v1 + new Vector3(0, 0, widthBorder) * 0.5f;
        Vector3 v3 = v1 + new Vector3(0, 0, widthBorder);
        Vector3 v4 = v1 + new Vector3(lengthBorder * 0.5f, 0, widthBorder);
        Vector3 v5 = centerPos + to;
        Vector3 v6 = v1 + new Vector3(lengthBorder, 0, widthBorder * 0.5f);
        Vector3 v7 = v1 + new Vector3(lengthBorder, 0, 0);
        Vector3 v8 = v1 + new Vector3(lengthBorder, 0, 0) * 0.5f;

        // Check if hit any room based on each point
        result =
            PointOverlapCheck(centerPos, lengthBorder > widthBorder ? widthBorder : lengthBorder) ||
            PointOverlapCheck(v1, cellScale) ||
            PointOverlapCheck(v2, cellScale) ||
            PointOverlapCheck(v3, cellScale) ||
            PointOverlapCheck(v4, cellScale) ||
            PointOverlapCheck(v5, cellScale) ||
            PointOverlapCheck(v6, cellScale) ||
            PointOverlapCheck(v7, cellScale) ||
            PointOverlapCheck(v8, cellScale);

        return result;
    }

    bool PointOverlapCheck(Vector3 center, int radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length > 0)
        {
            return true;
        }
        return false;
    }

    /**
     * Generate one room in scene
     */
    void GenOneRoom(Vector3 centerPos, int length, int width, Transform parent = null)
    {
        Vector3 to = new Vector3(length - cellScale, 0, width - cellScale) * 0.5f;
        Vector3 ned = centerPos - to;

        for (int i = 0; i < length / cellScale; i++)
        {
            for (int j = 0; j < width / cellScale; j++)
            {
                InsSetPos(ned + i * cellScale * Dx + j * cellScale * Dz, parent);
            }
        }
    }

    void InsSetPos(Vector3 pos, Transform parent = null)
    {
        var ins = Instantiate(cell);
        ins.transform.position = pos;
        ins.transform.parent = parent;
    }
}
