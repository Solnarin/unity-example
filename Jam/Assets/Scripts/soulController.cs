using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulController : MonoBehaviour
{
    public PlayerController playerController;

    public AudioSource au;
    public AudioClip soulCollectedClip;

    private void Awake()
    {
        playerController = Object.FindObjectOfType<PlayerController>();
        au = GetComponent<AudioSource>();


    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            au.PlayOneShot(soulCollectedClip);
            playerController.characterSoulAmount++;

            transform.localScale = Vector3.down * 0;
            Destroy(gameObject,1f);

        }
    }



}
