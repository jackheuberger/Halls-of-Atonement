using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //Code mostly taken from https://www.youtube.com/watch?v=MFQhpwc6cKE

    public float smoothing = 0.125f;
    public Vector3 offset;

    Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 newPos = LevelManager.Instance.player.position + offset;
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothing);

        transform.position = new Vector3(smoothedPos.x, smoothedPos.y, -100);
    }

}
