using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Manages room loading/cleanup
public class RoomManager : MonoBehaviour {

    // The player instance
    public GameObject player;

    // All the room prefabs go here
    public List<Room> allRooms;

    // The room instance the player is currently in
    private Room currentRoom;

    // The rooms adjacent to the current room that should be loaded
    private List<Room> adjacentRooms;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Sets a new current room, prepares adjacent room
    public void SetCurrentRoom(Room room)
    {
        // Check if this room is not adjacent (prepared), prepare it
        if (!adjacentRooms.Contains(room))
        {
            room.PrepareRoom();
        }
        currentRoom = room;
        room.ActivateRoom();
        // Get new adjacent rooms and prepare them
        adjacentRooms = room.GetAdjacentRooms();
        foreach (Room r in adjacentRooms)
        {
            if (r.GetState() != Room.RoomState.PREPARED)
            {
                r.PrepareRoom();
                r.SetState(Room.RoomState.PREPARED);
            }
        }
    }

    // Use after setting a new current room to clean up old rooms
    public void CleanUpRooms()
    {
        foreach (Room r in allRooms)
        {
            // Don't do anything for the current room
            if (r == currentRoom)
            {

            }
            // Ensure all adjacent rooms are deactivated
            else if (adjacentRooms.Contains(r))
            {
                r.DeactivateRoom();
            }
            // Clear all rooms that aren't current or adjacent
            else
            {
                // if this room isn't deactive, it should be, clear and deactivate it
                if (r.GetState() != Room.RoomState.DEACTIVE)
                {
                    r.ClearRoom();
                    r.DeactivateRoom();
                    r.SetState(Room.RoomState.DEACTIVE);
                }
            }
        }
    } 
}
