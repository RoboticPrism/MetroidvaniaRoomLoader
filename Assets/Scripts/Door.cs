using UnityEngine;
using System.Collections;

// Use for connections between rooms
public class Door : MonoBehaviour {

    // The room this door appears within
    public Room myRoom;

    // The door this door leads to
    public Door destinationDoor;

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
}
