using UnityEngine;
using System.Collections;

// Use for objects in rooms that will be cleaned up when the room is not in use, such as enemies and items
public class RoomObject : MonoBehaviour {

    // The object this spawner with create
    public GameObject objectPrefab;

    // Pointer to the instantiated object
    GameObject instantiatedObject;

    // If set to false, this item won't respawn when the room is loaded again
    public bool respawnObject = true;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Spawns the object prefab as an instance
    public void Prepare()
    {
        if (respawnObject)
        {
            instantiatedObject = (GameObject)Instantiate(objectPrefab, this.transform.position, this.transform.rotation, this.transform);
        }
    }

    // Destroy the instance of this object in the room
    public void Clear()
    {
        Destroy(instantiatedObject);
    }
}
