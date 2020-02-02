using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioSource music;
    public bool music_enabled;

    public GameObject metronome_sprite;

    public static GameController gameController;

    float t; // seconds

    // Start is called before the first frame update
    void Start()
    {
        gameController = this;
        NoteSpawner.noteSpawner.SpawnNote();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        checkMusic();
        checkMetronome();
    }

    void checkMusic()
    {
        if (music_enabled)
        {
            if (!music.isPlaying)
            {
                music.Play();
            }
        }
        else
        {
            music.Stop();
        }
    }

    void checkMetronome()
    {
       
    }

    public void NoteHit()
    {
        //Debug.Log("hit");
    }

    public void NoteMiss()
    {
       // Debug.Log("miss");
    }

    public void GenerateNote()
    {

    }
}
