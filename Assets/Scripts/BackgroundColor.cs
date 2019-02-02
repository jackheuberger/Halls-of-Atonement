using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour {

    public Color[] colors;
    public int index = 0;
    private int nextIndex = 0;

    public float totalDuration = 3f;
    private float changeColorTime;
    private float timer = 0.0f;

    public Camera cam;

    private void Start()
    {
        if (colors == null || colors.Length < 2)
            Debug.Log("Missing colors");

        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        nextIndex = (index + 1) % colors.Length;
        changeColorTime = totalDuration / colors.Length;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > changeColorTime)
        {
            index = (index + 1) % colors.Length;
            nextIndex = (index + 1) % colors.Length;
            timer = 0.0f;

        }
        cam.backgroundColor = Color.Lerp(colors[index], colors[nextIndex], timer/changeColorTime);
    }
}
