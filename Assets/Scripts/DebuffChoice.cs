using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebuffChoice : MonoBehaviour
{
    private Button button1;
    public Button b1;
    private Button button2;
    public Button b2;
    public Canvas canvas;

    public GeneralGameManager ggm;

    private void Start()
    {
        PlayerData.Instance.oneJump = false;
        Debug.Log("Choosing");
        button1 = PlayerData.Instance.buttons[Random.Range(0, PlayerData.Instance.buttons.Count)];
        PlayerData.Instance.buttons.Remove(button1);
        button2 = PlayerData.Instance.buttons[Random.Range(0, PlayerData.Instance.buttons.Count)];
        PlayerData.Instance.buttons.Add(button1);
        Debug.Log(button1.name + " " + button2.name);
        b1 = Instantiate(button1, new Vector3((Screen.width / 2) - 150, Screen.height / 2, 0), Quaternion.identity, canvas.transform);
        b2 = Instantiate(button2, new Vector3((Screen.width / 2) + 150, Screen.height / 2, 0), Quaternion.identity, canvas.transform);
        Debug.Log(b1);
        Debug.Log(b2);
        Debug.Log(PlayerData.Instance.pressButton);

        ggm = GeneralGameManager.Instance;
    }

    private void Update()
    {

    }

    public void lowerHealth()
    {
        Debug.Log("press");
        Debug.Log(PlayerData.Instance.pressButton);
        if (PlayerData.Instance.pressButton == true)
        {
            PlayerData.Instance.playerStartingHealth--;
            if (PlayerData.Instance.playerStartingHealth == 0)
            {
                PlayerData.Instance.playerStartingHealth = 1;
                PlayerData.Instance.playerOneShot = true;
            }
            Debug.Log("New Health = " + PlayerData.Instance.playerStartingHealth);
            postClick();
        }

    }

    public void lowerAttackDamange()
    {
        Debug.Log("press");
        Debug.Log(PlayerData.Instance.pressButton);
        if (PlayerData.Instance.pressButton == true)
        {
            PlayerData.Instance.arrowDamage = -.2f;
            if (PlayerData.Instance.arrowDamage < .2f)
            {
                PlayerData.Instance.arrowDamage = .2f;
                PlayerData.Instance.lowAD = true;
            }
            Debug.Log("New Damage = " + PlayerData.Instance.arrowDamage);
            postClick();
        }
        
    }

    public void movementSpeedDown()
{
    Debug.Log("press");
    Debug.Log(PlayerData.Instance.pressButton);
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.moveSpeed--;
        if (PlayerData.Instance.moveSpeed < 1)
        {
            PlayerData.Instance.moveSpeed = 1;
            PlayerData.Instance.lowMS = true;
        }
        Debug.Log("New Speed = " + PlayerData.Instance.moveSpeed);
        postClick();
    }
}

public void dimLighting()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.lowLighting = true;
        Debug.Log("Dimlighting");
        postClick();
    }

}

public void loseJump()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.maxJumps = 1;
        PlayerData.Instance.oneJump = true;
        Debug.Log("Lost Jump");
        postClick();
    }

}

public void fireRateDown()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.shotRate -= 1f;
        if (PlayerData.Instance.shotRate < 1)
        {
            PlayerData.Instance.shotRate = 1;
            PlayerData.Instance.lowFire = true;
        }
        Debug.Log("New Shot Rate = " + PlayerData.Instance.shotRate);
        postClick();
    }

}

public void enemyDamageIncrease()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.enemyDamage++;
        Debug.Log("New Enemy Dmg = " + PlayerData.Instance.enemyDamage);
        postClick();
    }

}

/*public void lessGold()
{

}*/

public void oneHealth()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.playerStartingHealth = 1;
        PlayerData.Instance.playerOneShot = true;
        Debug.Log("One Shot");
        postClick();
    }

}

public void longerLevels()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        Debug.Log("Level Increase");
            PlayerData.Instance.levelCount++;
        postClick();
    }

}

public void slowerArrows()
{
    Debug.Log("press");
    if (PlayerData.Instance.pressButton == true)
    {
        PlayerData.Instance.arrowSpeed--;
        if (PlayerData.Instance.arrowSpeed < 1)
        {
            PlayerData.Instance.arrowSpeed = 1;
            PlayerData.Instance.slowArrow = true;
        }
        Debug.Log("New Arrow Speed = " + PlayerData.Instance.arrowSpeed);
        postClick();
    }

}


public void postClick()
{

    PlayerData.Instance.pressButton = false;



    //Checking for redundant PlayerData.Instance.buttons to remove

    Debug.Log("starting post");
    if (PlayerData.Instance.playerOneShot)
    {
        Debug.Log("OneSHot");
        foreach (Button b in PlayerData.Instance.buttons)
        {
            Debug.Log(PlayerData.Instance.buttons);
            if (b.gameObject.name == "OneShotButton" || b.gameObject.name == "LowerHealthButton")
            {
                Debug.Log("Removing " + b);
                PlayerData.Instance.buttonToRemove.Add(b);
            }
        }
    }

    if (PlayerData.Instance.lowAD)
{
    Debug.Log("LowAD");
    foreach (Button b in PlayerData.Instance.buttons)
    {
        if (b.gameObject.name == "LowerADButton")
        {
                    PlayerData.Instance.buttonToRemove.Add(b);
                }
    }
}

        if (PlayerData.Instance.lowMS)
        {
            Debug.Log("LowMS");
            foreach (Button b in PlayerData.Instance.buttons)
            {
                if (b.gameObject.name == "MoveSlowerButton")
                {
                    PlayerData.Instance.buttonToRemove.Add(b);
                }
            }
        }

        if (PlayerData.Instance.oneJump)
        {
            Debug.Log("oneJump");
            foreach (Button b in PlayerData.Instance.buttons)
            {
                if (b.gameObject.name == "LoseJumpButton")
                {
                    PlayerData.Instance.buttonToRemove.Add(b);
                }
            }
        }

        if (PlayerData.Instance.lowFire)
        {
            Debug.Log("lowFire");
            foreach (Button b in PlayerData.Instance.buttons)
            {
                if (b.gameObject.name == "FireRateDownButton")
                {
                    PlayerData.Instance.buttonToRemove.Add(b);
                }
            }
        }

        if (PlayerData.Instance.slowArrow)
        {
            Debug.Log("slowARrow");
            foreach (Button b in PlayerData.Instance.buttons)
            {
                if (b.gameObject.name == "MoveSlowerButton")
                {
                    PlayerData.Instance.buttonToRemove.Add(b);
                }
            }
        }


        foreach (Button b in PlayerData.Instance.buttonToRemove)
        {
            PlayerData.Instance.buttons.Remove(b);
        }
        Debug.Log("Transition");
        GeneralGameManager.Instance.FadeToChoice("Prototype");

    }

}
