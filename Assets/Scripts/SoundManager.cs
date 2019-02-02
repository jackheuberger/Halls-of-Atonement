using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    public AudioSource backGroundMusic;


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

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        PlayClip(backGroundMusic);
    }

    public void PlayClip(AudioSource clip)
    {
        clip.Play();
    }

}
