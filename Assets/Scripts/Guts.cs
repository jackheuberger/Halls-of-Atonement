using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guts : MonoBehaviour {

    public float maxForce = 7f;
    public float minForce = 3f;
    public float maxRotationalForce = 5;
    public float minRotationalForce = -5f;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();


        rb.AddForce(new Vector2(Random.Range(minForce, maxForce), Random.Range(minForce, maxForce)), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(minRotationalForce, maxRotationalForce), ForceMode2D.Impulse);

        Destroy(gameObject, 5f);
    }
	
}
