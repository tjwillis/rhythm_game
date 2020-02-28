using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConductorController : MonoBehaviour
{
    public static ConductorController conductorController;
    public AudioSource kick;
    public AudioSource snare;
    public AudioSource click;

    //the current position of the song (in seconds)
    float songPosition;

    //the current position of the song (in beats)
    public float songPosInBeats;

    public int songPosInBeatsInt;

    //the duration of a beat
    public float secPerBeat;

    //how much time (in seconds) has passed since the song started
    float dsptimesong;

    //beats per minute of a song
    public float bpm;

    public UnityEngine.Events.UnityEvent BeatEvent;

    // Start is called before the first frame update
    void Start()
    {
        conductorController = this;

        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = 60f / bpm;

        //record the time when the song starts
        dsptimesong = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        //calculate the position in seconds
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        //calculate the position in beats
        songPosInBeats = songPosition / secPerBeat;

        // if a beat just occurred
        if ((int)songPosInBeats > songPosInBeatsInt)
        {
            songPosInBeatsInt = (int)songPosInBeats;
            EventManager.TriggerEvent("Beat");
        }
    }
}
