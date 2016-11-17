using UnityEngine;
using System.Collections;

// Represents each room and the items within it
public class Room : MonoBehaviour {
    // The doors in this room
    public Door[] doors;

    // Respawning objects in this room
    public RespawningRoomObject[] respawningObjects;

    // Onetime objects in this room
    public OnetimeRoomObject[] onetimeObjects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
