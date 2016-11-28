using UnityEngine;
using System.Collections;

// Use for connections between rooms
public class Door : MonoBehaviour {

    // The room this door appears within
    public Room myRoom;

    // The door this door leads to
    public Door destinationDoor;

	// Direction this door is going (Left or right value)
	// Left = -1.1, Right = 1.1
	public float doorDir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Get the door this door leads to
    public Door GetDestinationDoor()
    {
        return destinationDoor;
    }

    // Get the room this door is in
    public Room GetMyRoom()
    {
        return myRoom;
    }






	// TEST ROOM SWAPPING TO SEE IF IT WORKS
	public void OnTriggerEnter2D(Collider2D other) {
		// Was the collision made by the player
		if (other.gameObject.CompareTag(Tags.PLAYER)) {
			// Get the player's collider so we can offset its new position by the width
			BoxCollider2D playerCollider = other.gameObject.GetComponent<BoxCollider2D>();
			// Position of the destination door
			Vector3 newDest = destinationDoor.transform.position;
			// Add the player's collider width as offset so we aren't in the door
			newDest = new Vector3(newDest.x + playerCollider.bounds.size.x * doorDir, newDest.y, newDest.z);


			RoomManager rm = FindObjectOfType<RoomManager> ();
			rm.SetCurrentRoom (destinationDoor.GetMyRoom ());
			rm.CleanUpRooms ();

			other.gameObject.transform.position = newDest;
		}
	}
			
}
