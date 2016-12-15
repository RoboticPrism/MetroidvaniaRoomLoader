using UnityEngine;
using System.Collections;

public class BoundaryManager : MonoBehaviour {

	private BoxCollider2D managerBox;
	private Transform player;
	public GameObject boundary;

	// Use this for initialization
	void Start () {
		managerBox = GetComponent<BoxCollider2D> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		ManageBoundary ();
	}

	void ManageBoundary() {
		Bounds mBounds = managerBox.bounds;
		if (mBounds.min.x < player.position.x && player.position.x < mBounds.max.x &&
		    mBounds.min.y < player.position.y && player.position.y < mBounds.max.y) {
			boundary.SetActive (true);
		} else {
			boundary.SetActive (false);
		}
	}
}
