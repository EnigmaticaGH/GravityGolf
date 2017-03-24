using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform pointer;
    public float mouseSensitivity;
    public float moveForce;
	public float maxForce;
    public float maxLaunchForce;
	bool launched;
    bool paused;
    Vector2 launchForce;
    Vector2 force;
	Vector2 startPos;
	Quaternion startRot;
    float xDist, yDist;
    float xxDist, yyDist;

    void OnDestroy()
    {
        EventHandler.reset -= respawn;
        EventHandler.pause -= togglePause;
        EventHandler.win -= disable;
        EventHandler.launch -= launch;
    }

    // Use this for initialization
    void Start ()
	{
        EventHandler.reset += respawn;
        EventHandler.pause += togglePause;
        EventHandler.win += disable;
        EventHandler.launch += launch;
        //Initalize some variables
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		startPos = transform.position;
		startRot = transform.rotation;
        launchForce = Vector2.zero;
		launched = false;
        paused = false;
        xDist = 0f;
        yDist = 0f;
        xxDist = 0f;
        yyDist = 0f;
    }
	
	// Update is called once per frame
	void Update ()
	{
        //Listen for left mouse button
        if (Input.GetMouseButtonDown(0) && !launched && !paused)
        {
            EventHandler.Launch();
            //Add launch force
            GetComponent<Rigidbody2D>().AddForce(launchForce);
        }
		    
        if(!launched && !paused)
        {
            //Create visual for ball trajectory
            pointer.GetComponent<SpriteRenderer>().enabled = true;
            xDist += Input.GetAxis("Mouse X") * mouseSensitivity * 50f;
            yDist += Input.GetAxis("Mouse Y") * mouseSensitivity * 50f;
            launchForce = new Vector2(xDist, yDist);

            if(launchForce.magnitude > maxLaunchForce)
            {
                xDist = maxLaunchForce * launchForce.x / launchForce.magnitude;
                yDist = maxLaunchForce * launchForce.y / launchForce.magnitude;
                launchForce = new Vector2(xDist, yDist);
            }

            pointer.localScale = new Vector3(launchForce.magnitude / 80f, 1f, pointer.localScale.y);

            if(launchForce.magnitude != 0f)
            {
                if(launchForce.x > 0)
                    pointer.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Asin(launchForce.y / launchForce.magnitude)));
                else
                    pointer.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f - Mathf.Rad2Deg * Mathf.Asin(launchForce.y / launchForce.magnitude)));
            }     
        }

        if(launched && Input.GetMouseButton(0))
        {
            xxDist += Input.GetAxis("Mouse X") * mouseSensitivity;
            yyDist += Input.GetAxis("Mouse Y") * mouseSensitivity;
            force = new Vector2(xxDist, yyDist);
            if (force.magnitude > maxForce)
            {
                xxDist = maxForce * force.x / force.magnitude;
                yyDist = maxForce * force.y / force.magnitude;
                launchForce = new Vector2(xxDist, yyDist);
            }
            GetComponent<Rigidbody2D>().AddForce(force);

            pointer.localScale = new Vector3(force.magnitude / 2f, 1f, pointer.localScale.y);
            if (force.x > 0 && force.magnitude > 0)
                pointer.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Asin(force.y / force.magnitude)));
            else if(force.magnitude > 0)
                pointer.localRotation = Quaternion.Euler(new Vector3(0, 0, 180f - Mathf.Rad2Deg * Mathf.Asin(force.y / force.magnitude)));
        }
        if(Input.GetMouseButtonUp(0))
        {
            xxDist = 0f;
            yyDist = 0f;
            pointer.localScale = new Vector3(0f, 1f, pointer.localScale.y);
        }

        //Debug.Log(GetComponent<Rigidbody2D>().velocity.magnitude + "km/s");

        if (Input.GetKeyDown(KeyCode.R) && !paused)
        {
            EventHandler.Reset();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            EventHandler.Pause();
        }
	}

	void FixedUpdate ()
	{
        
	}

	void respawn()
	{
        //Reset all necessary values and variables, and put player back at start position.
        launched = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
		gameObject.transform.rotation = startRot;
		gameObject.transform.position = startPos;
        pointer.GetComponent<SpriteRenderer>().enabled = true;
	}

    void disable()
    {
        //GetComponent<Rigidbody2D>().Sleep();
        pointer.GetComponent<SpriteRenderer>().enabled = false;
    }

    void togglePause()
    {
        paused = !paused;
    }

	void OnCollisionEnter2D(Collision2D c) 
	{
        //Oops, looks like you smashed into a planet! Better luck next time...
        if(c.collider.CompareTag("Enemy"))
        {
            EventHandler.Reset();
        }
        if (c.collider.CompareTag("Win Condition"))
        {
            //Congrats, you win.
            GetComponent<Rigidbody2D>().Sleep();
            EventHandler.Win();
        }
	}

	void OnTriggerEnter2D(Collider2D c)
	{
        if (c.CompareTag("Win Condition"))
		{
            //Congrats, you win.
            GetComponent<Rigidbody2D>().Sleep();
            EventHandler.Win();
		}
	}
    void launch()
    {
        launched = true;
    }
}
