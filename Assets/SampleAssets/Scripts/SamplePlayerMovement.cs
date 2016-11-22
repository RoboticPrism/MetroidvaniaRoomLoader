using UnityEngine;
using System.Collections;

public class SamplePlayerMovement : MonoBehaviour {

    public float speed = 5.0f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float jump_speed = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            jump_speed = 0.5f;
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, rb.velocity.y+jump_speed);
	}
}
