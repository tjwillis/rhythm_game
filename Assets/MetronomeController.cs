using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeController : MonoBehaviour
{
    private AudioSource click;

    public float bpm;
    float spm;
    float mpb;
    float spb;

    float t_since_last_beat;

    // Start is called before the first frame update
    void Start()
    {
        click = GetComponent<AudioSource>();
        CalculateBeatTimes();
        t_since_last_beat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateBeatTimes();
        t_since_last_beat += Time.deltaTime;

        if (t_since_last_beat > spb)
        {
            Debug.Log("beat");
            t_since_last_beat = 0;

            Click();

            if (gameObject.GetComponent<SpriteRenderer>().color.a == 1f)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }

            //if (Random.value > 0.5)
            //{
            //    NoteSpawner.noteSpawner.SpawnNote();
            //}
        }
    }

    void Click()
    {
        click.Play();
    }

    void CalculateBeatTimes()
    {
        spm = bpm / 60;
        mpb = 1 / bpm;
        spb = 1 / spm;
    }
}
