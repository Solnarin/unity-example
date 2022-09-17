using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public Player_G player;
    public Boss_G boss;

    public float timer;
    public bool stop;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

            }
        }

        if (startPlaying)
        {
            if (!stop)
            {
                timer += Time.deltaTime;
                if(timer > 7)
                {
                    theMusic.Play();
                    Debug.Log("Playing");
                    timer -= 6;
                }
                else if(timer < 3)
                {
                    stop = true;
                }
            }
        }
       
    }
       

    public void NoteHit()
    {
        Debug.Log("OLDU");
        boss.Health -= 5;
    }

    public void NoteMissed()
    {
        Debug.Log("BASAMADIN");
        player.Health -= 20;
    }
}
