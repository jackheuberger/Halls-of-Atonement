using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {

    public PlayerData pd;
    public Image heart;
    public Canvas canvas;
    public List<Image> hearts;
    public TextMeshProUGUI deathText;
    public Button deathButton;
    public TextMeshProUGUI deathButtonText;

    public static UIManager Instance;


    // Use this for initialization
    void Start () {
        pd = PlayerData.Instance;
        deathText.enabled = false;
        deathButton.enabled = false;
        deathButtonText.enabled = false;
        for (int i = 0; i < pd.playerHealth; i++)
        {
            hearts.Add(Instantiate(heart, new Vector3(24.019f + (48.05f * i), Screen.height-30f), Quaternion.identity, canvas.transform));
        }

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateHearts()
    {
        Destroy(hearts[hearts.Count - 1]);
        hearts.Remove(hearts[hearts.Count - 1]);   
    }
    public void resetHearts(){
        foreach(Image h in hearts){
            Destroy(h);
            hearts.Remove(h);
        }
        for (int i = 0; i < pd.playerHealth; i++)
        {
            hearts.Add(Instantiate(heart, new Vector3(24.019f + (48.05f * i), Screen.height-30f), Quaternion.identity, canvas.transform));
        }
    }
    public void onDeath()
    {
        deathText.enabled = true;
        deathButton.enabled = true;
        deathButtonText.enabled = true;
    }

    public void uiReset()
    {
        Debug.Log("disabling");
        deathText.enabled = false;
        deathButton.enabled = false;
        deathButtonText.enabled = false;
    }
}
