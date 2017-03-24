using UnityEngine;
using System.Collections;

public class NBodyGravity : MonoBehaviour
{
	public bool isAntiGravity;
    //The actual g-force constant! Modified to fit game scale
	const float G = 6.67f;
	float objMass;
	float plyMass;
	float distance;
    public float density;
    float solarMasses;
    bool launched;
	Vector2 force;
	Vector2 direction;
	GameObject player;
	Rigidbody2D playerRigidBody;

    void OnDestroy()
    {
        EventHandler.launch -= launch;
        EventHandler.reset -= noLaunch;
        //EventHandler.pause -= togglePause;
    }

    // Use this for initialization
    void Start ()
	{
        EventHandler.launch += launch;
        EventHandler.reset += noLaunch;
        //EventHandler.pause += togglePause;
        launched = false;
        //Gravity only affects the player
        player = GameObject.FindGameObjectWithTag ("Player");
		playerRigidBody = player.GetComponent<Rigidbody2D> ();
		plyMass = playerRigidBody.mass;
        //Determine mass from size and density
        solarMasses = density * GetComponent<CircleCollider2D>().radius * getRadius(transform.localScale);
        //The sun's actual mass is 1.988e+30, but we need game scale!
        objMass = solarMasses * 1.988e+2f;
	}

	void FixedUpdate ()
	{
		//get the direction of the force
		direction = (gameObject.transform.position - player.transform.position).normalized;
		//get the distance from the object and player
		distance = Vector2.Distance (gameObject.transform.position, player.transform.position);
		//create force vector using gravity equation
		force = direction * ((objMass * plyMass * G) / Mathf.Pow(distance, 2));

        //If we haven't launched our ball OR we're paused, don't affect us with gravity!
		if (!launched)
			force = Vector2.zero;

		if (isAntiGravity)
			force = -force;

		playerRigidBody.AddForce(force);
	}

    //Not using this... yet. It returns the size in actual units of an object
    Vector2 getActualDimension()
    {
        return new Vector2(transform.localScale.x * GetComponent<SpriteRenderer>().sprite.bounds.size.x, transform.localScale.y * GetComponent<SpriteRenderer>().sprite.bounds.size.y);
    }

    float getRadius(Vector2 v)
    {
        return (v.x + v.y) / 2f;
    }

    void launch()
    {
        launched = true;
    }
    void noLaunch()
    {
        launched = false;
    }
}