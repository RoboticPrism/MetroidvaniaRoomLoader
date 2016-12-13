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
    public List<RoomObject> roomObjects;

	// Variables for camera locking
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;


	// Use this for initialization
	void Awake ()
    {
		// Search this room's children for it's doors and objects
		foreach (Transform child in GetComponentInChildren<Transform>())
        {
			if (child.GetComponent<Door>() != null)
            {
				Door currentDoor = child.GetComponent<Door>();
				// Set this door's parent room to this room, then add it to
				// this room's list of doors
				currentDoor.myRoom = this;
				doors.Add(currentDoor);
			}
            if (child.GetComponent<RoomObject>() != null)
            {
                roomObjects.Add(child.GetComponent<RoomObject>());
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
		foreach(RoomObject respawnObject in roomObjects) {
			respawnObject.Prepare();
		}
    }

    // Clears out objects within a room
    public void ClearRoom()
    {
        foreach (RoomObject respawnObject in roomObjects)
        {
            respawnObject.Clear();
        }
        // TODO: Destroy projectiles
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

	public void SetLimits() {
		CameraControl cc = Object.FindObjectOfType<CameraControl> ();

		cc.SetNewLimits (minX, maxX, minY, maxY);
	}

}
