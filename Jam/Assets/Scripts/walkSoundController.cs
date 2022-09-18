using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class walkSoundController : MonoBehaviour
{
    PlayerController playerController;
    public AudioSource au;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        au = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (playerController.horizontal != 0 && playerController.isGrounded)
        {
            if (!au.isPlaying)
            {
                au.Play();
            }
        }
        else
            au.Stop();
    }


}
