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
    /**
     * If this is an end room
     */
    public bool isEndRoom;
    /**
     * If this is a room on main cross
     */
    public bool isMainCrossRoom;

    public enum RoomType
    {
        QuizRoom,
        AssignmentRoom,
        FinalRoom
    }
}
