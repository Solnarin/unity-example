using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

 //   public Canvas canvas;

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
            //    canvas.enabled = false;
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
        boss.Health -= 1;
    }

    public void NoteMissed()
    {
        player.Health -= 20;
    }
}
