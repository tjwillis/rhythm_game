using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatController : MonoBehaviour
{
    GameObject conductor;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log(GetBeat());
        }
    }

    string GetBeat()
    {
        int beat = conductor.gameObject.GetComponent<ConductorController>().songPosInBeatsInt;

        if (beat / 4 % 2 == 1)
        {
            return ("Even Measure.");
        }
        else
        {
            return ("Odd Measure.");
        }
        
    }
}
