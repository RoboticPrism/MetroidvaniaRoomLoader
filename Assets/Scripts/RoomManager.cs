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
    public Room currentRoom;

    // The rooms adjacent to the current room that should be loaded
    public List<Room> adjacentRooms;

	// Use this for initialization
	void Start ()
    {
		// Find all the rooms in the game
		GameObject[] roomsInScene = GameObject.FindGameObjectsWithTag(Tags.ROOM);
		foreach (GameObject room in roomsInScene) {
			// Get the Room component from the object
			Room roomComponent = room.GetComponent<Room>();

			// Add the room to the list of all rooms, and check if it's the spawn room
			// If spawn room, make it our initial current room
			allRooms.Add(roomComponent);
			if (roomComponent.spawnRoom) {
				SetCurrentRoom(roomComponent);
			}
		}

		// Needs at least one room in the game
		if (allRooms.Count == 0) {
			Debug.LogError("PLEASE PROVIDE AT LEAST ONE ROOM");
			Application.Quit();
		}
			
		// Initialize the player's starting room
		// !IMPORTANT!
		// Can't use SetCurrentRoom() here because we have no adjacent rooms yet
		if (currentRoom == null) {
			Debug.LogError("NO STARTING ROOM GIVEN");
			Application.Quit();
		}
			
		// Clean and deactivate all other rooms
		CleanUpRooms();
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
		room.SetState(Room.RoomState.ACTIVE);

		// Get new adjacent rooms and prepare them
		SetAdjacentRooms (room);
    }

	// Prepare all rooms adjacent to the given room, and set them to the PREPARED state
	public void SetAdjacentRooms(Room room)
	{
		// Get all rooms adjacent to the given room
		adjacentRooms = room.GetAdjacentRooms();

		// Prepare all adjacent Rooms r, to the given room
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
