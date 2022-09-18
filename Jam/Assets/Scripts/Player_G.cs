using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_G : MonoBehaviour
{
    public float Health = 100f;

    public Animator anim;


    private void Update()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
    
   
}
