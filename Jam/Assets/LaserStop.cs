using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStop : MonoBehaviour
{
    public Animator laserAnim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Stop()
    {
        laserAnim.SetBool("canFire?", false);
    }
}
