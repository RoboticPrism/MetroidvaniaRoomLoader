using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Represents each room and the items within it
public class Room : MonoBehaviour {
    // States a room can have.  ACTIVE is the current room and is active.  PREPARED rooms are loaded but not active.  DEACTIVE rooms are unloaded and deactive
    public enum RoomState {ACTIVE, PREPARED, DEACTIVE};

    // This room's state
    public RoomState state;

	// Is this room the current spawn room
	public bool spawnRoom;
    
    // The doors in this room
    public List<Door> doors;

    // Respawning objects in this room
    public List<RespawningRoomObject> respawningObjects;

    // Onetime objects in this room
    public List<OnetimeRoomObject> onetimeObjects;

	// Use this for initialization
	void Awake ()
    {
		// Search this room's children for it's doors
		foreach (Transform child in this.transform) {
			if (child.CompareTag(Tags.DOOR)) {
				Door currentDoor = child.GetComponent<Door>();
				// Set this door's parent room to this room, then add it to
				// this room's list of doors
				currentDoor.myRoom = this;
				doors.Add(currentDoor);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Get room state
    public RoomState GetState()
    {
        return state;
    }

    // Set room state
    public void SetState(RoomState s)
    {
        state = s;
    }

    // Activates a room
    public void ActivateRoom()
    {
        this.gameObject.SetActive(true);
    }

    // Deactivates a room
    public void DeactivateRoom()
    {
        this.gameObject.SetActive(false);
    }

    // Prepares objects within a room
    public void PrepareRoom ()
    {
        //TODO: Load respawning room objects and one time objects

		foreach(RespawningRoomObject respawnObject in respawningObjects) {
			respawnObject.Reset();
		}

		foreach(OnetimeRoomObject oneTimeObject in onetimeObjects) {
			if(oneTimeObject.isAvailable) {
				oneTimeObject.Reset();
			}
		}
    }

    // Clears out objects within a room
    public void ClearRoom()
    {
        //TODO: Clear all non-permanent room objects
    }

    // Returns all rooms adjacent to this one
    public List<Room> GetAdjacentRooms()
    {
        List<Room> return_list = new List<Room>();
        foreach (Door d in doors)
        {
            return_list.Add(d.GetDestinationDoor().GetMyRoom());
        }
        return return_list;
    }

}
