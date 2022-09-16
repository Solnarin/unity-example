using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;

    void Start()
    {
        beatTempo = beatTempo / 60f; //how fast arrows move per second based on 120 beats per min
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);

        }
    }
}
