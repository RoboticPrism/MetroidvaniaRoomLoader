using UnityEngine;
using System.Collections;


public class CameraControl : MonoBehaviour {

	public GameObject player;
	[Header("Single Grid Space Size")]
	public float minX;
	public float minY;

	// Activate your position limitations for the Y axis by turning this on.

	[Header("Camera Movement Boundaries")]
	public bool limitCameraMovement = true;
	public bool limitCamXMovement = true;
	public bool limitCamYMovement = true;
	public float limitLeft;
	public float limitRight;
	public float limitBottom;
	public float limitTop;

	private float aspectRatio;
	private Vector3 cameraPosition;
	private Vector3 playerPosition;

	void Start () {
		aspectRatio = minX / minY;
		Camera.main.aspect = aspectRatio;

		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
		cameraPosition = transform.position;
	}


	void LateUpdate()
	{
		//Updates the camera position based on player location
		CameraUpdate();
	}

	void CameraUpdate ()
	{
		playerPosition = player.transform.position;
		cameraPosition = new Vector3 (playerPosition.x, playerPosition.y, transform.position.z);

		// Here we clamp the desired position into the area declared in the limit variables.
		if (limitCameraMovement) {
			cameraPosition.y = Mathf.Clamp (cameraPosition.y, limitBottom, limitTop);
			cameraPosition.x = Mathf.Clamp (cameraPosition.x, limitLeft, limitRight);
		} else if (limitCamXMovement) {
			cameraPosition.x = Mathf.Clamp (cameraPosition.x, limitLeft, limitRight);
		} else if (limitCamYMovement) {
			cameraPosition.y = Mathf.Clamp (cameraPosition.y, limitBottom, limitTop);
		}

		// and now we're updating the camera position using what came of all the calculations above.
		transform.position = cameraPosition;
	}


	// Public functions start here. These are for other objects/scripts to communicate with the camera.

	// Use this to change/activate level limits.
	public void SetNewLimits (float leftLimit, float rightLimit, float bottomLimit, float topLimit )
	{
		limitLeft = leftLimit;
		limitRight = rightLimit;
		limitBottom = bottomLimit;
		limitTop = topLimit;

		limitCameraMovement = true;
		limitCamXMovement = true;
		limitCamYMovement = true;
	}

	// No longer use limits.
	public void DeactivateLimits ()
	{
		limitCameraMovement = false;
	}

	public void DeactivateXLimits()
	{
		limitCamXMovement = false;
	}
	public void DeactivateYLimits()
	{
		limitCamYMovement = false;
	}
}