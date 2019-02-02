using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpvel;
    private Rigidbody2D rb;

    private float xInput = 0f;

    private bool isGrounded = false;
    private int jumpCount = 0;
    public int maxJumps;

    [HideInInspector]
    public int direction = 1;

    //Stores the local scale at the beginning
    Vector3 startingLocalScale;

    public GeneralGameManager ggm;
    private PlayerData pd;

    PlayerCombat playerCombat;

    Animator animator;

    public GameObject playerLight;

    // Use this for initialization
    void Start()
    {
        pd = PlayerData.Instance;
        playerCombat = GetComponent<PlayerCombat>();
        isGrounded = false;
        rb = GetComponent<Rigidbody2D>();
        startingLocalScale = transform.localScale;
        ggm = GeneralGameManager.Instance;

        //Retrieve vars from PlayerData
        speed = pd.moveSpeed;
        jumpvel = pd.jumpVel;
        maxJumps = pd.maxJumps;

        animator = GetComponent<Animator>();

        if (pd.lowLighting)
        {
            Vector3 position = transform.position;
            position.z = -1;
            Instantiate(playerLight, position, Quaternion.identity);
        }
    }

	// Update is called once per frame
	void Update () {

        //Get the horizontal input (A, D, left arrow, right arrow, stick left, and stick right)
        xInput = Input.GetAxis("Horizontal");
        if(xInput != 0)
        {
            if (xInput > 0)
                direction = 1;
            else if (xInput < 0)
                direction = -1;

            Vector3 newLocalScale = new Vector3(startingLocalScale.x * direction, startingLocalScale.y, 1);
            transform.localScale = newLocalScale;

            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (Input.GetKeyDown(KeyCode.W) && (isGrounded == true || jumpCount < maxJumps))
        {
            rb.velocity = Vector2.up * jumpvel;
            jumpCount++;
        }



    }

    private void FixedUpdate()
    {
        //Gets the amount it's gonna move
        Vector2 move = new Vector2(xInput, 0) * speed;
        //Sets the y of that amount to the velocity beforehand
        move.y = rb.velocity.y;

        //Sets the velocity to that
        rb.velocity = move;

        if (playerLight != null)
            playerLight.transform.position = new Vector3(playerLight.transform.position.x, playerLight.transform.position.y, -1);

    }

    //Colliders
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            playerCombat.TakeDamage(col);
        }


        if (col.gameObject.tag == "Shrine")
        {
            Debug.Log("Shrine get");
            ggm.FadeToChoice("Choice");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerCombat.TakeTriggerDamage(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

    }


}
