using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    public float orbitRadius;
    public float orbitSpeed;
    public float startAngle;
    //What object is this orbiting around?
    public Transform orbitingAround;

	// Use this for initialization
	void Start ()
    {
        transform.position = orbitingAround.position + new Vector3(orbitRadius * Mathf.Cos(Mathf.Deg2Rad*startAngle), orbitRadius * Mathf.Sin(Mathf.Deg2Rad*startAngle), 0);
	}
	
	void FixedUpdate ()
    {
        transform.RotateAround(orbitingAround.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        Vector2 desiredPosition = (transform.position - orbitingAround.position).normalized * orbitRadius + orbitingAround.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, 10);
	}
}