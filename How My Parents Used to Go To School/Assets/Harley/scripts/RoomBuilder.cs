using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.AI;
using UnityEditor.AI;

public class RoomBuilder : MonoBehaviour
{
    /**
     * Floor cell object
     */
    [SerializeField] GameObject cell;
    /**
     * Wall cell object
     */
    [SerializeField] GameObject wall;

    [SerializeField] GameObject door;

    [SerializeField] GameObject[] items;

    [SerializeField] GameObject[] enemies;
    [SerializeField] int enemyCount;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject telepoter;

    /**
     * The scale of cell object
     */
    [SerializeField] int cellScale;
    /**
     * The height of wall object
     */
    [SerializeField] int wallHeight;
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
    [SerializeField] GameObject baker;



    /**
     * Unit vectors
     */
    Vector3Int Dx = new Vector3Int(1, 0, 0);
    Vector3Int Dy = new Vector3Int(0, 1, 0);
    Vector3Int Dz = new Vector3Int(0, 0, 1);

    /**
     * Tag for floor cells
     */
    const string roomTag = "Room";
    const string wallTag = "Wall";

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

    public NavMeshSurface getSurface(){
        return baker.GetComponent<NavMeshSurface>();

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
        temp.tag = roomTag;
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
            temp.tag = roomTag;

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
        Vector3 v1 = ned + new Vector3(0, wallHeight, -cellScale) * 0.5f;
        Vector3 v2 = v1 + new Vector3(0, 0, width);
        Vector3 v3 = ned + new Vector3(-cellScale, wallHeight, 0) * 0.5f;
        Vector3 v4 = v3 + new Vector3(length, 0, 0);

        for (int i = 0; i < length / cellScale; i++)
        {
            for (int j = 0; j < width / cellScale; j++)
            {
                InsSetPos(cell, ned + i * cellScale * Dx + j * cellScale * Dz, false, parent);
                cell.tag = "Floor";
            }
        }

        for (int i = 0; i < length / cellScale; i++)
        {
            InsSetPos(wall, v1 + i * cellScale * Dx, false, parent);
            InsSetPos(wall, v2 + i * cellScale * Dx, false, parent);
        }

        for (int i = 0; i < width / cellScale; i++)
        {
            InsSetPos(wall, v3 + i * cellScale * Dz, true, parent);
            InsSetPos(wall, v4 + i * cellScale * Dz, true, parent);
        }
    }

    public void BuildEnvironment(RoomData room)
    {
        var parent = GameObject.Find(roomTag + room.roomId).transform;
        if (room.roomType != RoomData.RoomType.StartRoom)
        {
            if (room.roomType == RoomData.RoomType.FinalRoom)
            {
                var cp = room.roomTran.centerPos;
                InsSetPos(telepoter, new Vector3(cp.x, 0, cp.y), false, parent);
            }
            foreach (GameObject item in items)
            {
                for (int i = 0; i < Random.Range(0, 3); i++)
                {
                    var pos = FindEmptyPlace(room.roomTran);
                    InsSetPos(item, pos, false, parent);
                }
            }
        }
    }

    Vector3 FindEmptyPlace(RoomTran rt)
    {
        var cp = rt.centerPos;
        while (true)
        {
            int x = Random.Range(-rt.length / 2 + cellScale, rt.length / 2 - cellScale);
            int y = Random.Range(-rt.width  / 2 + cellScale, rt.width / 2 - cellScale);
            Vector3 pos = new Vector3(cp.x + x, 0, cp.y + y);
            if (Physics.OverlapSphere(pos, cellScale, 1 << 11).Length == 0)
            {
                return pos;
            }
        }
    }

    public GameObject GetCell(){
        return cell;
    }

    public void SpawnEnemies(RoomData room)
    {
        var parent = GameObject.Find(roomTag + room.roomId).transform;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int healerNumber = enemyCount / 5;
        int swordsmanNumber = enemyCount / 2;
        int gunnerNumber = enemyCount / 3;
        int tripleGunnerNumber = enemyCount / 3;
        int tankNumber = enemyCount / 3;
        GameObject[] prefab = enemies;
        if (room.roomType == RoomData.RoomType.QuizRoom)
        {

            for (int i = 0; i < enemyCount; i++)
            {
                int enemyIndex = Random.Range(0, prefab.Length);
                var pos = FindEmptyPlace(room.roomTran);

                GameObject item = (GameObject)Instantiate(prefab[enemyIndex], pos, Quaternion.identity, parent);
                item.GetComponent<EnemyStatContainer>().SetRoomId(room.roomId);
                //InsSetPos(item, new Vector3(cp.x + x, 0, cp.y + y));
                if (item.tag == "Enemy") {
                    item.GetComponent<Enemy>().SetTarget(player, bulletPrefab);
                    item.GetComponent<EnemyStatContainer>().IncreaseStats(PlayerPrefs.GetFloat("Scale"));
                }
                if (item.TryGetComponent(out Swordsman s)) {
                    if(--swordsmanNumber <= 0){
                        print("Removing swordsman from suggestion");
                        prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                    }
                }if (item.TryGetComponent(out Healer h)) {
                    if(--healerNumber <= 0){
                        print("Removing healer from suggestion");
                        prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                    }
                }if (item.TryGetComponent(out Gunner g)) {
                    if(--gunnerNumber <= 0){
                        print("Removing gunner from suggestion");
                        prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                    }
                }if (item.TryGetComponent(out TripleGunner tg)) {
                    if(--tripleGunnerNumber <= 0){
                        print("Removing triple gunner from suggestion");
                        prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                    }
                }if (item.TryGetComponent(out Tank t)) {
                    if(--tankNumber <= 0){
                        print("Removing tank from suggestion");
                        prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                    }
                }

                room.monsters.Add(item);
                room.monstersCount++;
            }
        }
        if (room.roomType == RoomData.RoomType.FinalRoom)
        {
            // if (bosses.Length != 0)
            // {
            //     Vector3 pos = new Vector3(room.roomTran.centerPos.x, 0, room.roomTran.centerPos.y);
            //     GameObject boss = InsSetPos(bosses[Random.Range(0, bosses.Length)], pos, false, parent);
            //     boss.GetComponent<Enemy>().SetTarget(player, bulletPrefab);
            //     boss.GetComponent<EnemyStatContainer>().IncreaseStats(PlayerPrefs.GetFloat("Scale"));
            //     room.monsters.Add(boss);
            // }
        }
    }

    public void DestroyAllDoors(RoomData room)
    {
        foreach (var door in room.doorList)
        {
            if (door.rotate)
            {
                RaycastHit[] hits = Physics.RaycastAll(door.position - Dx, Dx, 2.0f);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.tag == wallTag)
                    {
                        Destroy(hits[i].collider.gameObject);
                    }
                }
            }
            else
            {
                RaycastHit[] hits = Physics.RaycastAll(door.position - Dz, Dz, 2.0f);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].transform.tag == wallTag)
                    {
                        Destroy(hits[i].collider.gameObject);
                    }
                }
            }
        }
    }

    public void LockRoom(RoomData room)
    {
        var parent = GameObject.Find(roomTag + room.roomId).transform;
        foreach (var dr in room.doorList)
        {
            InsSetPos(door, dr.position, dr.rotate, parent);
        }
    }

    GameObject InsSetPos(GameObject prefab, Vector3 pos, bool rotate = false, Transform parent = null)
    {
        var ins = Instantiate(prefab);
        ins.transform.position = pos;
        ins.transform.eulerAngles = rotate ? new Vector3(0, 90, 0) : Vector3.zero;
        ins.transform.parent = parent;
        return ins;
    }
}
