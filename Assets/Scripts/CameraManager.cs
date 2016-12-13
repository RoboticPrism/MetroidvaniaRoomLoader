using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	private BoxCollider2D cameraBox;
	private Transform player;

	// Use this for initialization
	void Start () {
		cameraBox = GetComponent<BoxCollider2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		FollowPlayer ();
	}

	void FollowPlayer() {
		BoxCollider2D boundaryBox = GameObject.Find ("Boundary").GetComponent<BoxCollider2D> ();
		Debug.Log (boundaryBox.bounds.max.y);
		Debug.Log (cameraBox.size.y / 2);
		Debug.Log ("///////////" + player.position.y);

		float camBoxX = cameraBox.size.x / 2;
		float camBoxY = cameraBox.size.y / 2;
		if (boundaryBox) {
			transform.position = new Vector3 (Mathf.Clamp (player.position.x, boundaryBox.bounds.min.x + camBoxX, boundaryBox.bounds.max.x - camBoxX),
											  Mathf.Clamp (player.position.y, boundaryBox.bounds.min.y + camBoxY, boundaryBox.bounds.max.y - camBoxY),
											  transform.position.z);
		}
	}
}
