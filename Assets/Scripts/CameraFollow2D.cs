using UnityEngine;
using System.Collections;

public class CameraFollow2D : MonoBehaviour {
	
	public float dampTime;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
    public float scrollSpeed;
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    bool disabled;
    bool paused;

    enum cameraModes
    {
        free,
        chase
    };
    cameraModes cameraMode;

    void OnDestroy()
    {
        EventHandler.win -= toggle;
        EventHandler.pause -= togglePause;
    }

    void Start ()
    {
        EventHandler.win += toggle;
        EventHandler.pause += togglePause;
        cameraMode = cameraModes.chase;
        disabled = false;
        paused = false;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if(!paused && !disabled)
        {
            //C to change camera modes
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (cameraMode == cameraModes.chase)
                    cameraMode = cameraModes.free;
                else
                    cameraMode = cameraModes.chase;
            }

            //Free cam!
            if (cameraMode == cameraModes.free)
            {
                Vector3 destination = transform.position
                    + Vector3.right * Input.GetAxis("Horizontal") * 10f
                    + Vector3.up * Input.GetAxis("Vertical") * 10f;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime * scrollSpeed);
            }
            //Chase cam!
            else if (target && cameraMode == cameraModes.chase)
            {
                Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
                Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
                Vector3 destination = transform.position + delta;
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            }

            //Zoom control, using mouse scroll wheel
            if (GetComponent<Camera>().orthographicSize > minZoom && GetComponent<Camera>().orthographicSize < maxZoom)
                GetComponent<Camera>().orthographicSize += (-Input.mouseScrollDelta.y * zoomSpeed);
            else if (GetComponent<Camera>().orthographicSize <= minZoom && -Input.mouseScrollDelta.y > 0)
                GetComponent<Camera>().orthographicSize += (-Input.mouseScrollDelta.y * zoomSpeed);
            else if (GetComponent<Camera>().orthographicSize >= maxZoom && -Input.mouseScrollDelta.y < 0)
                GetComponent<Camera>().orthographicSize += (-Input.mouseScrollDelta.y * zoomSpeed);
            else if (GetComponent<Camera>().orthographicSize <= minZoom)
                GetComponent<Camera>().orthographicSize = minZoom;
            else if (GetComponent<Camera>().orthographicSize >= maxZoom)
                GetComponent<Camera>().orthographicSize = maxZoom;
        }
	}

    void toggle()
    {
        disabled = !disabled;
    }
    void togglePause()
    {
        paused = !paused;
    }
}
