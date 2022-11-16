using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    /**
     * The ID of the room
     */
    public int roomId;
    /**
     * Transform information of this room
     */
    public RoomTran roomTran;
    /**
     * The type of this room
     */
    public RoomType roomType;
    /**
     * The list of linked rooms
     */
    public List<RoomData> crossRooms;
    /**
     * The list of monsters inside
     */
    public List<GameObject> monsters;
    // just store the count
    public int monstersCount = 0;

    /**
     * If this is an end room
     */
    public bool isEndRoom;
    /**
     * If this is a room on main cross
     */
    public bool isMainCrossRoom;

    public List<RoomDoorData> doorList = new List<RoomDoorData>();

    public enum RoomType
    {
        StartRoom,
        QuizRoom,
        FinalRoom
    }
}

public struct RoomDoorData
{
    public Vector3 position;
    public bool rotate;
}
