using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance;

    //Player Stats
    public int playerStartingHealth = 5;
    public int playerHealth;
    public float moveSpeed = 5f;
    public float jumpVel = 8.5f;
    public int maxJumps = 2;
    //Arrow Stats
    public float arrowSpeed = 5f;
    public float arrowDamage = 1f;
    public float hitTimer = 1;
    public float shotRate = 4f;
    //Enemy Stats
    public int enemyDamage = 2;

    public bool pressButton = true;

    public float minotaurSpeed = 5f;
    public float harpeySpeed = 0.01f;

    public UIManager ui;

    public bool lowLighting = false;

    //Instantiated under low light
    public GameObject playerLight;

    //Debuff Choice vars
    public bool playerOneShot = false;
    public bool lowAD = false;
    public bool lowMS = false;
    public bool oneJump = false;
    public bool lowFire = false;
    public bool slowArrow = false;
    public int levelCount = 5;

    public List<Button> buttons = new List<Button>();
    public List<Button> buttonToRemove = new List<Button>();


    public int score = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerHealth = playerStartingHealth;
    }


    private void Update()
    {
        if (hitTimer >= 0)
        {
            hitTimer -= Time.deltaTime;
        }

    }


    public void LowerHealth(int amount)
    {
        if(hitTimer<=0)
            playerHealth -= amount;

        hitTimer = 0.1f;

        for (int i = 0; i < amount; i++)
        {
            ui.UpdateHearts();
        }
    }

    public void Reset()
    {
        Debug.Log("Resetting PlayerData");
        playerStartingHealth = 5;
        moveSpeed = 5f;
        jumpVel = 8.5f;
        maxJumps = 2;
    //Arrow Stats
        arrowSpeed = 5f;
        arrowDamage = 1f;
        hitTimer = 1;
        shotRate = 4f;
    //Enemy Stats
        enemyDamage = 2;

        pressButton = true;

        minotaurSpeed = 5f;
        harpeySpeed = 0.01f;
        lowLighting = false;

    //Debuff Choice vars
        playerOneShot = false;
        lowAD = false;
        lowMS = false;
        oneJump = false;
        lowFire = false;
        slowArrow = false;
        levelCount = 5;

    }
}
