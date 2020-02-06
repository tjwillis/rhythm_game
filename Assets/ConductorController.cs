using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConductorController : MonoBehaviour
{
    public static ConductorController conductorController;
    public AudioSource kick;
    public AudioSource snare;

    //the current position of the song (in seconds)
    float songPosition;

    //the current position of the song (in beats)
    public float songPosInBeats;

    public int songPosInBeatsInt;

    //the duration of a beat
    float secPerBeat;

    //how much time (in seconds) has passed since the song started
    float dsptimesong;

    //beats per minute of a song
    public float bpm;

    //keep all the position-in-beats of notes in the song
    //notes is an array which keeps the entire position-in-beats of the notes in the song.For example, notes would be { 1f, 2f, 2.5f, 3f, 3.5f, 4.5f} for the note below:
    List<GameObject> notes;

    //the index of the next note to be spawned
    int nextIndex = 0;

    public float beatsShownInAdvance = 4;

    // Start is called before the first frame update
    void Start()
    {
        conductorController = this;

        notes = GenerateRandomNotes();

        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = 60f / bpm;

        //record the time when the song starts
        dsptimesong = (float)AudioSettings.dspTime;

        //start the song
        //GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate the position in seconds
        songPosition = (float) (AudioSettings.dspTime - dsptimesong);

        //calculate the position in beats
        songPosInBeats = songPosition / secPerBeat;

        if (nextIndex < notes.Count && notes[nextIndex].GetComponent<NoteController>().beat_number < songPosInBeats + beatsShownInAdvance)
        {
            NoteSpawner.noteSpawner.SpawnNote(notes[nextIndex]);

            nextIndex++;
        }

        // if a beat just occurred
        if ((int)songPosInBeats > songPosInBeatsInt)
        {
            Debug.Log("beat");
            songPosInBeatsInt = (int)songPosInBeats;
            if (((songPosInBeatsInt % 4) == 1) || ((songPosInBeatsInt % 4) == 3))
            {
                kick.Play();
            }
            else if (((songPosInBeatsInt % 4) == 0) || ((songPosInBeatsInt % 4) == 2))
            {
                snare.Play();
            }
        }
    }

    private List<GameObject> GenerateRandomNotes(int n = 100)
    {
        List<GameObject> notes = new List<GameObject>() { };

        for (int i=0; i <= n; i++)
        {
            GameObject note = NoteSpawner.noteSpawner.CreateNote();
            note.GetComponent<NoteController>().beat_number = i+4;
            notes.Add(note);
        }

        return notes;
    }

}
