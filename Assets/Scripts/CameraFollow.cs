using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (player != null)
        {
            this.transform.position = Vector3.MoveTowards(new Vector3(this.transform.position.x,
                                                                      this.transform.position.y,
                                                                      this.transform.position.z),
                                                          new Vector3(player.transform.position.x,
                                                                      player.transform.position.y,
                                                                      this.transform.position.z), 
                                                          1.0f);
        }
	}
}
