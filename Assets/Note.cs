using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float default_speed;
    private float current_speed;

    private float videoLatency;

    public bool canBePressed;

    public KeyCode keyCode;

    public Vector3 SpawnPos = new Vector3();
    public Vector3 RemovePos = new Vector3();

    public float beat_number;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        current_speed = default_speed;
        videoLatency = CalibrationController.videoLatency;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            SpawnPos,
            RemovePos,
            (NoteController.noteController.beatsShownInAdvance - (beat_number - ConductorController.conductorController.songPosInBeats + videoLatency)) / NoteController.noteController.beatsShownInAdvance
        );

        if (Input.GetKeyDown(keyCode) && canBePressed)
        {
            gameObject.SetActive(false);
            GameController.gameController.NoteHit();
            Destroy(gameObject);
        }

        if (transform.position == RemovePos)
        {
            Destroy(gameObject);
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
}
