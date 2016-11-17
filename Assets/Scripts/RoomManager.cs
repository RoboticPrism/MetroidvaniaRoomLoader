using UnityEngine;
using System.Collections;

// Manages room loading/cleanup
public class RoomManager : MonoBehaviour {

    // The player instance
    public GameObject player;

    // All the room prefabs go here
    public Room[] allRooms;

    // The room instance the player is currently in
    private Room currentRoom;

    // The rooms adjacent to the current room that should be loaded
    private Room[] adjacentRooms;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
