using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    public static NoteController noteController;

    private SpriteRenderer spriteRenderer;
    private ConductorController conductor;

    //keep all the position-in-beats of notes in the song
    //notes is an array which keeps the entire position-in-beats of the notes in the song.For example, notes would be { 1f, 2f, 2.5f, 3f, 3.5f, 4.5f} for the note below:
    List<GameObject> notes;

    public float default_speed;
    private float current_speed;

    public float beatsShownInAdvance = 4;
    //the index of the next note to be spawned
    int nextIndex = 0;

    public bool isPlayerTurn;

    public bool canBePressed;

    public KeyCode keyCode;

    public Vector3 SpawnPos = new Vector3();
    public Vector3 RemovePos = new Vector3();

    public float beat_number;

    // Start is called before the first frame update
    void Start()
    {
        noteController = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
        current_speed = default_speed;

        notes = GenerateRandomNotes();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Note Controller Update Called");

        transform.position = Vector3.Lerp(
            SpawnPos,
            RemovePos,
            (beatsShownInAdvance - (beat_number - ConductorController.conductorController.songPosInBeats)) / beatsShownInAdvance
        );

        if (nextIndex < notes.Count && notes[nextIndex].GetComponent<Note>().beat_number < ConductorController.conductorController.songPosInBeats + beatsShownInAdvance)
        {
            NoteSpawner.noteSpawner.SpawnNote(notes[nextIndex]);

            nextIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canBePressed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBePressed = false;
        if (gameObject.activeInHierarchy)
        {
            GameController.gameController.NoteMiss();
        }

    }

    private List<GameObject> GenerateRandomNotes(int n = 100)
    {
        List<GameObject> notes = new List<GameObject>() { };

        for (int i = 0; i < n; i++)
        {
            GameObject note = NoteSpawner.noteSpawner.CreateNote(false);
            note.GetComponent<Note>().beat_number = i + 4;
            notes.Add(note);
        }

        return notes;
    }
}
