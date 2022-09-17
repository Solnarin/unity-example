using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyScript : MonoBehaviour
{

    [Header("Enemy Stats")]


    private float enemySpeed;
    public float chaseSpeed;
    public float whileAttackingSpeed;
    public Transform startingPoint;
    public bool chase = false;


    [Header("Health Bar")]

    public HealthbarBehaviour Healthbar;
    public float currentHealth;
    public float maxHealth = 100;



    private GameObject player;
    private Animator anim;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        Healthbar.SetHealth(currentHealth, maxHealth);
        enemySpeed = chaseSpeed;
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


        if (currentHealth < maxHealth)
            Healthbar.SetHealth(currentHealth, maxHealth);


        if(currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0)
            Destroy(gameObject);

    }

    private void Chase()
    {
           
        transform.position=Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position,player.transform.position) <= 1.2)
        {
            enemySpeed = whileAttackingSpeed;
            anim.SetBool("canShoot", true);
        }
        
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, enemySpeed * Time.deltaTime);
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
        enemySpeed = chaseSpeed;
    }

    public void TakeHit(float attackDamage)
    {
        currentHealth -= attackDamage;
        anim.SetTrigger("damage");
    }
}
