using UnityEngine;
using System.Collections;

// Use for objects in rooms that should not respawn, such as bosses and pick ups
public class OnetimeRoomObject : MonoBehaviour {

	// Has this one time object already been picked up
	public bool isAvailable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Reset this object to it's original state
	public void Reset() {
		//TODO: put this object back where it belongs
	}
}
