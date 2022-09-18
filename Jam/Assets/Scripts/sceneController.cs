using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    public int currentSceneSoulAmount;

    public PlayerController playerController;
    public GameObject player;
    public playerStatsController playerStatsController;
    public cameraController CameraController;

    private void Awake()
    {
        playerController = Object.FindObjectOfType<PlayerController>();
        playerStatsController = Object.FindObjectOfType<playerStatsController>();
        CameraController = Object.FindObjectOfType<cameraController>();
        player = GameObject.Find("Player");
        DontDestroyOnLoad(gameObject);

    }


    private void Update()
    {

        if (currentSceneSoulAmount == 0 && playerStatsController.currentHealth<=0)
        {
            player.transform.position = new Vector2(0.6f, 0f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;
        }
        if (currentSceneSoulAmount == 1 && playerStatsController.currentHealth <= 0)
        {

            player.transform.position = new Vector2(-2, 0.4f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;

        }
        if (currentSceneSoulAmount == 2 && playerStatsController.currentHealth <= 0)
        {
            player.transform.position = new Vector2(-11, 0.4f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;



        }
        if (currentSceneSoulAmount == 3 && playerStatsController.currentHealth <= 0)
        {
            player.transform.position = new Vector2(-7.5f, -5.7f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;



        }
        if (currentSceneSoulAmount == 4 && playerStatsController.currentHealth <= 0)
        {
            player.transform.position = new Vector2(-5.5f, 3f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;



        }
        if (currentSceneSoulAmount == 5 && playerStatsController.currentHealth <= 0)
        {
            player.transform.position = new Vector2(-5, 1.5f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;


        }
        if (currentSceneSoulAmount == 6 && playerStatsController.currentHealth <= 0)
        {
            player.transform.position = new Vector2(-2.5f, 0f);
            playerStatsController.currentHealth = playerStatsController.maxHealth;


        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.characterSoulAmount++;
            currentSceneSoulAmount++;

            if (currentSceneSoulAmount == 1)
            {
                playerController.canAnger = true;
                player.transform.position = new Vector2(-2,0.4f);
                gameObject.transform.position = new Vector2(207, -2.5f);
                
            }
            if (currentSceneSoulAmount == 2)
            {
                playerController.canSleep = true;
                player.transform.position = new Vector2(-11, 0.4f);
                gameObject.transform.position = new Vector2(207, 2.5f);


            }
            if (currentSceneSoulAmount == 3)
            {
                playerController.canJealous = true;
                player.transform.position = new Vector2(-7.5f, -5.7f);
                gameObject.transform.position = new Vector2(242, 2.9f);


            }
            if (currentSceneSoulAmount == 4)
            {
                playerController.canGreed = true;
                player.transform.position = new Vector2(-5.5f, 3f);
                gameObject.transform.position = new Vector2(183, -20.5f);


            }
            if (currentSceneSoulAmount == 5)
            {
                playerController.canLust = true;
                player.transform.position = new Vector2(-5, 1.5f);
                gameObject.transform.position = new Vector2(204, -32.5f);


            }
            if (currentSceneSoulAmount == 6)
            {
                playerController.canGlutton = true;
                player.transform.position = new Vector2(-2.5f, 0f);
                gameObject.transform.position = new Vector2(255, 2.1f);

            }
            if (currentSceneSoulAmount == 7)
            {
                playerController.canArrogance = true;
                Destroy(player);
                Destroy(CameraController);
                Destroy(gameObject);


            }
            SceneManager.LoadScene(currentSceneSoulAmount);




        }





    }





}
