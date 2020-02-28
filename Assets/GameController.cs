using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
    }

    public void NoteHit()
    {
        float audioLatencyInBeats = CalibrationController.audioLatency / ConductorController.conductorController.secPerBeat;
        float accuracy = ConductorController.conductorController.songPosInBeats - ConductorController.conductorController.songPosInBeatsInt - audioLatencyInBeats;
        string descriptor;

        if (accuracy < -0.2)
        {
            descriptor = "Very early";
        }
        else if (accuracy < -0.1)
        {
            descriptor = "A little early";
        }
        else if (accuracy < 0.1)
        {
            descriptor = "Perfect!";
        }
        else if (accuracy < 0.2)
        {
            descriptor = "A little late";
        }
        else
        {
            descriptor = "Very late";
        }

        GameObject.Find("Canvas/ScoreText").GetComponent<Text>().text = accuracy.ToString("0.##") + " - " + descriptor;
    }

    public void NoteMiss()
    {
       // Debug.Log("miss");
    }
}
