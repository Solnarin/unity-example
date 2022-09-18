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


    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange;
    public float attackDamage;


    private GameObject player;
    private Animator anim;

    public Transform hitBox;


    public GameObject soul;

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
        {
            Instantiate(soul,transform.position,Quaternion.identity);

            Destroy(gameObject);
        }

    }


    private void Chase()
    {
           
        transform.position=Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position,player.transform.position) <= 0.2f)
        {
            enemySpeed = whileAttackingSpeed;
            anim.SetBool("canShoot", true);

            Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange);
            foreach (Collider2D col in attackPlayer)
            {
                if (col.CompareTag("Player") )
                {
                    col.GetComponent<playerStatsController>().Damage(attackDamage);
                }
            }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }







}
