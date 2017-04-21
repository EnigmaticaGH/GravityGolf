using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour {
    public Transform exit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            c.transform.position = exit.position;
            StartCoroutine(ToggleCollider());
        }
    }

    IEnumerator ToggleCollider()
    {
        Debug.Log("Disabling Collider");
        exit.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        exit.GetComponent<CircleCollider2D>().enabled = true;
        Debug.Log("Enabling Collider");
    }
}
