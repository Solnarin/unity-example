using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_G : MonoBehaviour
{
    public float Health = 100f;

    public Animator anim;


    private void BackToIdle()
    {
        anim.SetBool("canAttack", false);
    }
}
