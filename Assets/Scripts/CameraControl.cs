using UnityEngine;
using System.Collections;


public class CameraControl : MonoBehaviour {

	public GameObject player;
	[Header("Smallest Camera Size")]
	public float cameraMinWidth = 10;
	public float cameraMinHeight = 10;
    [Header("Largest Camera Size")]
    public float cameraMaxWidth = 20;
    public float cameraMaxHeight = 20;

	// Activate your position limitations for the Y axis by turning this on.

	[Header("Camera Movement Boundaries")]
	public bool limitCameraMovement = true;
	public bool limitCamXMovement = true;
	public bool limitCamYMovement = true;
    public Vector2 roomSizeMin;
    public Vector2 roomSizeMax;

    private Camera camera;


    void Start () {
        // Set the camera for calculation later
        camera = this.GetComponent<Camera>();
    }


	void LateUpdate()
	{
		//Updates the camera position based on player location
		CameraUpdate();
	}

	void CameraUpdate ()
	{
        // Calculate the ideal camera size for this room
        float roomWidth = roomSizeMax.x - roomSizeMin.x;
        float roomHeight = roomSizeMax.y - roomSizeMin.y;
        
        // Ensure camera doesn't get bigger or smaller than its min and max bounds
        if (roomWidth > cameraMaxWidth)
        {
            roomWidth = cameraMaxWidth;
        }
        else if (roomWidth < cameraMinWidth)
        {
            roomWidth = cameraMinWidth;
        }
        if (roomHeight > cameraMaxHeight)
        {
            roomHeight = cameraMaxHeight;
        }
        else if (roomHeight < cameraMinHeight)
        {
            roomHeight = cameraMinHeight;
        }

        // If aspect ratio on ideal room size doesn't match the current aspect ratio, change width or height to make it match
        float aspect = camera.aspect;
        
        // If width is too big, lower it to be in line with aspect ration
        if (roomWidth/roomHeight > aspect)
        {
            roomWidth = roomHeight * aspect;
        }

        // If height is too big, lower it to be in line with aspect ratio
        else if (roomWidth / roomHeight < aspect)
        {
            roomHeight = roomWidth / aspect;
        }

        // Set camera size
        camera.orthographicSize = roomHeight/2.0f;

        float cameraHeight = camera.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * camera.aspect;

        // Calculate the camera position in this room

        Debug.Log(cameraWidth + " " + cameraHeight);

        Vector3 playerPosition = player.transform.position;
		Vector3 cameraPosition = new Vector3 (playerPosition.x, playerPosition.y, transform.position.z);

		// Here we clamp the desired position into the area declared in the limit variables.
		if (limitCameraMovement) {
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, roomSizeMin.x + cameraWidth / 2.0f, roomSizeMax.x - cameraWidth / 2.0f);
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, roomSizeMin.y + cameraHeight / 2.0f, roomSizeMax.y - cameraHeight / 2.0f);
		} else if (limitCamXMovement) {
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, roomSizeMax.x, roomSizeMin.x);
        } else if (limitCamYMovement) {
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, roomSizeMax.y, roomSizeMin.y);
        }

		// and now we're updating the camera position using what came of all the calculations above.
		this.transform.position = Vector3.MoveTowards(transform.position, cameraPosition, 0.1f);
	}


	// Public functions start here. These are for other objects/scripts to communicate with the camera.

	// Use this to change/activate level limits.
	public void SetNewLimits (Vector2 min, Vector2 max)
	{
        roomSizeMin = min;
        roomSizeMax = max;

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