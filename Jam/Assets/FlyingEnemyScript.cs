using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public bool chase = false;
    public Transform startingPoint;
    private Animator anim;
    public HealthbarBehaviour Healthbar;
    public float Hitpoints;
    public float MaxHitpoints = 100;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(player == null)
            return;
        else if (chase == true)
            Chase();
        else
            ReturnStartPoint();
            Flip();

    }

    private void Chase()
    {
           
        transform.position=Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,player.transform.position) <= 1.2)
        {
            speed = 0;
            anim.SetBool("canShoot", true);
        }
        
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0,0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void StopShooting()
    {
        anim.SetBool("canShoot", false);
        speed = 2;
    }

    public void TakeHit(float attackDamage)
    {
        Hitpoints -= attackDamage;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }

    }
}
