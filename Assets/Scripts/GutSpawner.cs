using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GutSpawner : MonoBehaviour {

    public int maxGuts = 5;
    public int minGuts = 2;

    public GameObject guts;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Random.Range(minGuts, maxGuts); i++)
        {
            Instantiate(guts, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 5f);
	}

}
