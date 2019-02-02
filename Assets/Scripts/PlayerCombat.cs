using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour {

    //Stores the arrow
    public GameObject arrow;
    public Transform shotPosition;

    public float shotRate = 1f;
    private float shotRateTimer;

    private PlayerData playerData;

    private Rigidbody2D rb;

    //Instantiated when attacked
    public GameObject blood;

    Animator animator;
    public UIManager ui;

    private void Start()
    {
        //Initiates the timer
        shotRateTimer = 1/shotRate;
        //Caches the playerdata singleton
        playerData = PlayerData.Instance;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        //If the health is less than or = to 0, die
        if(playerData.playerHealth <= 0)
        {
            Die();
        }

        //Decreases the timer (if its greater than 0)
        if (shotRateTimer > 0)
        {
            shotRateTimer -= Time.deltaTime;
        }

        //If space bar or A on controller is pressed
        if (Input.GetButtonDown("Jump") && shotRateTimer <=0)
        {
            animator.SetTrigger("Shoot");
            Shoot();
        }
	}

    /// <summary>
    /// Takes damage to the player after a collision
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(Collision2D collision)
    {
        //Blood effect
        Instantiate(blood, transform.position, Quaternion.identity);

        Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();

        if (hitEnemy.direction == -1)
        {
            collision.rigidbody.AddForce(hitEnemy.enemyKnockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            collision.rigidbody.AddForce(new Vector2(-hitEnemy.enemyKnockbackForce.x, hitEnemy.enemyKnockbackForce.y), ForceMode2D.Impulse);
        }

        rb.AddForce(hitEnemy.playerKnockbackForce, ForceMode2D.Impulse);

        playerData.LowerHealth(hitEnemy.damage);
    }

    public void TakeTriggerDamage(Collider2D collision)
    {
        //Blood effect
        Instantiate(blood, transform.position, Quaternion.identity);

        Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();

        rb.AddForce(hitEnemy.playerKnockbackForce, ForceMode2D.Impulse);

        playerData.LowerHealth(hitEnemy.damage);
    }

    /// <summary>
    /// Shoots an arrow
    /// </summary>
    void Shoot()
    {
        //Instantiates an arrow at the shot point
        GameObject shotArrow = Instantiate(arrow, shotPosition.position, Quaternion.identity);

        //Flips arrow accordingly
        shotArrow.transform.localScale = new Vector3(shotArrow.transform.localScale.x * GetComponent<PlayerController>().direction, shotArrow.transform.localScale.y);

        //Makes the bullet move in the direction that the player is facing
        shotArrow.GetComponent<Arrow>().speed *= GetComponent<PlayerController>().direction;

        //Destroys arrow after 2 seconds
        Destroy(shotArrow, 2);

        //Resets the timer for the next shot
        shotRateTimer = 1/shotRate;
    }

    /// <summary>
    /// Dies
    /// </summary>
    void Die()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        rb.gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;

        UIManager.Instance.onDeath();

        Destroy(GeneralGameManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
