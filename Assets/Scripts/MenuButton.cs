using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

    public Animator anim;

	public void fadeOut()
    {
        anim.SetTrigger("Click");
    }

    public void sceneChange()
    {
        SceneManager.LoadScene(1);
    }
}
