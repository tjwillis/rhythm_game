using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject arrow_prefab;

    public static NoteSpawner noteSpawner;

    // Start is called before the first frame update
    void Start()
    {
        noteSpawner = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNote(string direction = "")
    {
        GameObject new_arrow = Instantiate(arrow_prefab, transform);
        Vector3 new_position = new Vector3(0f, 0f, 0f);
        Quaternion new_rotation = new Quaternion(0f, 0f, 0f, 0f);
        KeyCode new_keycode = new KeyCode();

        // if no direction is specified, randomize it
        if (direction == "")
        {
            var directions = new List<string> { "left", "up", "down", "right" };

            direction = directions[Random.Range(0, directions.Count)];
        }

        switch (direction)
        {
            case "left":
                new_position = new Vector3(-1.5f, 0f, 0f);
                new_rotation = Quaternion.Euler(0f, 0f, 180f);
                new_keycode = KeyCode.LeftArrow;
                break;

            case "up":
                new_position = new Vector3(-0.5f, 0f, 0f);
                new_rotation = Quaternion.Euler(0f, 0f, 90f);
                new_keycode = KeyCode.UpArrow;
                break;

            case "down":
                new_position = new Vector3(0.5f, 0f, 0f);
                new_rotation = Quaternion.Euler(0f, 0f, -90f);
                new_keycode = KeyCode.DownArrow;
                break;

            case "right":
                new_position = new Vector3(1.5f, 0f, 0f);
                new_rotation = Quaternion.Euler(0f, 0f, 0f);
                new_keycode = KeyCode.RightArrow;
                break;
        }
        new_arrow.transform.position += new_position;
        new_arrow.transform.rotation = new_rotation;
        new_arrow.GetComponent<ArrowController>().keyCode = new_keycode;
    }
}
