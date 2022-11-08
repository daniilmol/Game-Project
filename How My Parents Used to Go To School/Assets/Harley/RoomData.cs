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
    [SerializeField] RoomTran roomTran;
    /**
     * The type of this room
     */
    [SerializeField] RoomType roomType;
    /**
     * The list of linked rooms
     */
    [SerializeField] List<RoomData> crossRooms;
    /**
     * The list of monsters inside
     */
    [SerializeField] List<GameObject> monsters;
    /**
     * If this is an end room
     */
    [SerializeField] bool isEndRoom;
    /**
     * If this is a room on main cross
     */
    [SerializeField] bool isMainCrossRoom;

    public enum RoomType
    {
        QuizRoom,
        AssignmentRoom,
        FinalRoom
    }
}
