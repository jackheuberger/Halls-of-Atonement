using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Vector3 position = LevelManager.Instance.player.position;
        position.z = -0.5f;

        transform.position = position;
	}
}
