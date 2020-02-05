using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject metronome_sprite;
    public GameObject noteSpawner;

    public static GameController gameController;

    float t; // seconds

    // Start is called before the first frame update
    void Start()
    {
        gameController = this;
        //noteSpawner.GetComponent<NoteSpawner>().SpawnNote();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
    }

    public void NoteHit()
    {
        //Debug.Log("hit");
    }

    public void NoteMiss()
    {
       // Debug.Log("miss");
    }
}
