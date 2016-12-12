using UnityEngine;
using System.Collections;

public class SamplePlayerMovement : MonoBehaviour {

    public float speed = 5.0f;
    private Rigidbody2D rb;
    private bool hasControl = true;
    public SpriteRenderer blackout;

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

    // Walk between doors
    public void WalkBetweenRooms(Door door)
    {
        // if the player doesn't have control, they're probably already moving between rooms
        if (hasControl)
        {
            StartCoroutine("WalkBetweenRoomsCoroutine", door);
        }
    }

    // Fades in the blackout object, waits a bit, then moves the player to the new room
    IEnumerator WalkBetweenRoomsCoroutine(Door door)
    {
        // Remove player control
        hasControl = false;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        
        // Assign new current room
        RoomManager rm = FindObjectOfType<RoomManager>();
        rm.SetCurrentRoom(door.GetDestinationDoor().GetMyRoom());

        // Drop the blackout object over the camera
        while (blackout.color.a < 1.0f)
        {
            blackout.color = new Color(0.0f, 0.0f, 0.0f, blackout.color.a + 0.05f);
            yield return null;
        }

        // Set the position to move towards (Note that we use the player's z location)
        Vector3 goToPosition = new Vector3(door.GetDestinationDoor().GetMyDestination().position.x,
                                           door.GetDestinationDoor().GetMyDestination().position.y,
                                           this.gameObject.transform.position.z); 

        // Move player to new room
        while (Vector3.Distance(this.gameObject.transform.position, goToPosition) > 0.01f)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position,
                                                                     goToPosition,
                                                                     speed/100.0f);
            yield return null;
        }
        

        // Pull the blackout object off the camera
        while (blackout.color.a > 0.0f)
        {
            blackout.color = new Color(0.0f, 0.0f, 0.0f, blackout.color.a - 0.01f);
            yield return null;
        }

        // Now that the old room is offscree we can clean it up
        rm.CleanUpRooms();

        // Reenable Control
        hasControl = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Door>() != null)
        {
            WalkBetweenRooms(other.GetComponent<Door>());
        }
    }
}
