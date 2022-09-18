using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int count;
    public KeyCode keyToPress;
    public Animator anim;
    public Animator laserAnim;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            count++;
        }

        if(count == 5)
        {
            PlayAttack();
        }

    }

    private void PlayAttack()
    {
        count = 0;
        laserAnim.SetBool("canFire?", true);
    }
}
