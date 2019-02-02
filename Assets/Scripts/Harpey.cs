using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpey : MonoBehaviour {

    Rigidbody2D rb;

    Enemy enemyComponent;

    public float speed;

    public float height = 1f;

    private float startingY;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        enemyComponent = GetComponent<Enemy>();

        speed = PlayerData.Instance.harpeySpeed;

        enemyComponent.startingLocalScale.x *= -1;

        startingY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        rb.MovePosition(new Vector2(transform.position.x-speed, startingY + (Mathf.Sin(Time.time)*height)));
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemyComponent.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyComponent.Die();
        }
    }

}
