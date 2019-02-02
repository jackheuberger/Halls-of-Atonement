using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;

    public int damage;

    Rigidbody2D rb;

    public int direction = 1;

    public Vector3 playerKnockbackForce;
    public Vector3 enemyKnockbackForce;

    //Stores the localscale at Start()
    public Vector3 startingLocalScale;

    //The gameobject instantiated on death
    public GameObject deathParticle;

    //Instantiated when hit with an arrow
    public GameObject blood;

    GameObject playerLight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingLocalScale = transform.localScale;

    }

    private void Start()
    {

        if (PlayerData.Instance.lowLighting)
        {
            Vector3 position = transform.position;
            position.z = -0.5f;
            playerLight = Instantiate(PlayerData.Instance.playerLight, position, Quaternion.identity, transform);
        }
    }

    void Update ()
    {
        if(health <= 0)
        {
            Die();
        }

        //Sets the new localScale based on the direction
        transform.localScale = new Vector3(-direction * startingLocalScale.x, startingLocalScale.y, startingLocalScale.z);


        if (playerLight != null)
            playerLight.transform.position = new Vector3(playerLight.transform.position.x, playerLight.transform.position.y, -1);

    }

    /// <summary>
    /// Kills the enemy
    /// </summary>
    public void Die()
    {
        //Instantiates the guts
        Instantiate(deathParticle, transform.position, Quaternion.identity);

        PlayerData.Instance.score++;
        //Destroys this object
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            //Subtracts health based on the damage of the arrow
            Arrow arrowComponent = collision.GetComponent<Arrow>();
            health -= arrowComponent.damage;


            if (rb.velocity.magnitude != 0 && GetComponent<Minotaur>() != null)
            {
                //Adds a force at the hitpoint
                rb.AddForce(new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x * 100, 0), ForceMode2D.Force);
            }

            //Instantiates the blood
            GameObject instantiatedBlood = Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(instantiatedBlood, 2f);
        }
    }

}
