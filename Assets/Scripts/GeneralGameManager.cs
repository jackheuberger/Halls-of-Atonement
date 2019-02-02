using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralGameManager : MonoBehaviour {

    public static GeneralGameManager Instance;

    public UIManager ui;

    public Animator animator;
    public string scene;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public void FadeToChoice(string sname)
    {
        Debug.Log("Fading");
        animator.SetTrigger("FadeOut");
        scene = sname;
    }

    public void loadScene()
    {
        SceneManager.LoadScene(scene);
        Debug.Log("loaded");
        animator.SetTrigger("FadeIn");
    }

    public void restartButtonClick()
    {
        Debug.Log("reset");
        UIManager.Instance.uiReset();
        PlayerData.Instance.Reset();
        SceneManager.LoadScene(0);

    }
}
