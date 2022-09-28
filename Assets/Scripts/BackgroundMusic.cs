using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    static BackgroundMusic instance = null;

    public AudioClip StartMusic;
    public AudioClip LevelMusic;
    public AudioClip EndMusic;

    private AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            print("Self Destruction called!");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = StartMusic;
            music.loop = true;
            music.Play();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Background Music: Loaded Level " + level);
        music.Stop();
        if(level == 1)
        {
            music.clip = StartMusic;
        }
        if (level == 2)
        {
            music.clip = LevelMusic;
        }
        if (level == 4)
        {
            music.clip = EndMusic;
        }
        music.loop = true;
        music.Play();
    }

}
