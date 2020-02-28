using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CalibrationController : MonoBehaviour
{
    private List<float> values;
    private bool audioFinished;
    private bool allFinished;

    private GameObject exitButton;

    public static float audioLatency;
    public static float videoLatency;

    public AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        values = new List<float>();
        audioLatency = 0;
        videoLatency = 0;
        audioFinished = false;
        allFinished = false;
        exitButton = GameObject.Find("Canvas/ExitButton");
        exitButton.SetActive(false);

        // Set up beat listener
        UnityAction beatEvent = new UnityAction(OnBeat);
        EventManager.StartListening("Beat", beatEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!allFinished)
        {
            if (!audioFinished && !allFinished)
            {
                CalibrateAudio();
            }
            else
            {
                CalibrateVideo();
            }
        }
    }

    void OnBeat()
    {
        if (!allFinished)
        {
            if (audioFinished)
            {
                RectTransform rt = GameObject.Find("Canvas/BoxSprite").GetComponent<RectTransform>();
                rt.anchoredPosition *= new Vector2(-1, 1);
            }
            else
            {
                click.Play();
            }
        }
    }

    void CalibrateAudio()
    {
        float beats = ConductorController.conductorController.songPosInBeats;
        int beatsInt = ConductorController.conductorController.songPosInBeatsInt;
        float difference = (beats - beatsInt) / 2; // Difference in seconds. Make sure calibration is at 120 BPM

        if (Input.GetKeyDown("space"))
        {
            values.Add(difference);

            UpdateAverage();

            // When audio calibration is done
            if (values.Count > 7)
            {
                audioFinished = true;
                GameObject.Find("Canvas/HelpText").GetComponent<Text>().text = "Press Space when square moves";
                // Save Audio latency. Game settings?
                values.Clear();
            }
        }
    }

    void CalibrateVideo()
    {
        float beats = ConductorController.conductorController.songPosInBeats;
        int beatsInt = ConductorController.conductorController.songPosInBeatsInt;

        if (Input.GetKeyDown("space"))
        { 
            values.Add(beats - beatsInt);

            UpdateAverage();

            // When calibration is finished
            if (values.Count > 15)
            {
                GameObject.Find("Canvas/HelpText").GetComponent<Text>().text = "Finished!";
                allFinished = true;

                GameObject.Find("Canvas/BoxSprite").SetActive(false);

                // Make exit button visible
                exitButton.SetActive(true);
            }
        }
    }

    void UpdateAverage()
    {
        if (!audioFinished)
        {
            audioLatency = values.Average();

            Text text = GameObject.Find("Canvas/AudioText").GetComponent<Text>();
            text.text = "Audio Latency: " + audioLatency.ToString("0.###") + " seconds";
        }
        else
        {
            videoLatency = values.Average();

            Text text = GameObject.Find("Canvas/VideoText").GetComponent<Text>();
            text.text = "Video Latency: " + videoLatency.ToString("0.###") + " seconds";
        }
    }

    public void ExitCalibration()
    {
        Application.LoadLevel("main");
    }
}
