using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accuracy : MonoBehaviour
{
    private float audioLatencyInBeats;

    // Start is called before the first frame update
    void Start()
    {
        audioLatencyInBeats = CalibrationController.audioLatency / ConductorController.conductorController.secPerBeat;
/*        Debug.Log("Audio Latency in Seconds: " + CalibrationController.audioLatency.ToString());
        Debug.Log("Beats per Second: " + ConductorController.conductorController.secPerBeat.ToString());
        Debug.Log("Audio Latency in Beats: " + audioLatencyInBeats.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            float accuracy = ConductorController.conductorController.songPosInBeats - ConductorController.conductorController.songPosInBeatsInt - audioLatencyInBeats;
            GameObject.Find("Canvas/ScoreText").GetComponent<Text>().text = accuracy.ToString();
        }
    }
}
