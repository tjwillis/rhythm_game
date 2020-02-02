using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metadata : MonoBehaviour
{
    public float bpm;
    public float mpb;
    public float bps;
    public float spb;

    // Start is called before the first frame update
    void Start()
    {
        mpb = 1 / bpm;
        bps = bpm / 60;
        spb = 1 / bps; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
