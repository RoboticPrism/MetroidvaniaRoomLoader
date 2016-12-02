using UnityEngine;
using System.Collections;

public class SamplePlayerMovement : MonoBehaviour {

    public float speed = 5.0f;
    private Rigidbody2D rb;
    private bool hasControl = true;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (hasControl)
        {
            float jump_speed = 0f;
            if (Input.GetAxis("Vertical") > 0)
            {
                jump_speed = 0.5f;
            }
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y + jump_speed);
        }
	}

    // Teleport between doors
    public void TeleportBetweenRooms(Door door)
    {
        RoomManager rm = FindObjectOfType<RoomManager>();
        rm.SetCurrentRoom(door.GetDestinationDoor().GetMyRoom());
        this.gameObject.transform.position = door.GetDestinationDoor().GetMyDestination().position;
        rm.CleanUpRooms();
    }

    // Walk between doors
    public void WalkBetweenRooms(Door door)
    {
        // Remove player control
        hasControl = false;
        RoomManager rm = FindObjectOfType<RoomManager>();
        rm.SetCurrentRoom(door.GetDestinationDoor().GetMyRoom());
        this.gameObject.transform.position = door.GetDestinationDoor().GetMyDestination().position;
        rm.CleanUpRooms();

        // Reenable Control
        hasControl = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Door>() != null)
        {
            TeleportBetweenRooms(other.GetComponent<Door>());
        }
    }
}
