using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour {

    //Represents the player that the enemy is targeting
    public Transform target;

    private Rigidbody2D rb;

    public float speed;

    Enemy enemyComponent;

    Animator animator;

    bool active = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = LevelManager.Instance.player;

        enemyComponent = GetComponent<Enemy>();
        animator = GetComponent<Animator>();

        speed = PlayerData.Instance.minotaurSpeed;
    }

    // Update is called once per frame
    void Update () {

        //Gets the distance to the target
        Vector2 distanceToTarget = (target.position - transform.position);

        if (distanceToTarget.magnitude <= 5f)
            active = true;

        if (!active)
            return;

        //Gets direction of player
        Vector2 directionOfTarget = distanceToTarget.normalized;
        //Gets the new direction of the enemy
        enemyComponent.direction = (int)Mathf.Sign(directionOfTarget.x);


        //Multiplies direction by the speed
        Vector2 newVelocity = directionOfTarget * speed;
        //Sets the y on the velocity to 0
        newVelocity.y = rb.velocity.y;

        //If the velocity is in the opposite direction of the desired velocity
        if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(newVelocity.x))
        {
            newVelocity.y = 0;
            //Add the velocity instead of setting it (this allows for smoother transition after a force is applied)
            rb.velocity += newVelocity;
        }
        else
        {
            //Sets the velocity to the new velocity
            rb.velocity = newVelocity;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If this hits the player
        if(collision.gameObject.tag == "Player")
        {
            //Play slashing animation
            animator.SetBool("isSlashing", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //If this stops hitting the player
        if (collision.gameObject.tag == "Player")
        {
            //Stop playing the slash animation
            animator.SetBool("isSlashing", false);
        }
    }

}
