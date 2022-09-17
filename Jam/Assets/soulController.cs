using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soulController : MonoBehaviour
{
    public PlayerController playerController;

    private void Awake()
    {
        playerController = Object.FindObjectOfType<PlayerController>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            playerController.characterSoulAmount++;
            transform.localScale = Vector3.down * 0;
            Destroy(gameObject,1f);

        }
    }



}
