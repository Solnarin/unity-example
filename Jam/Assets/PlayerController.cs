using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private bool isLookingRight = true;


    [Header("Player Stats")]

    public float characterSpeed;
    public float characterPower;
    public float characterJumpForce;





    [Header("Double Jump")]
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayerMask;
    public float checkRadius;
    public bool canJump = true;







    Animator anim;
    Rigidbody2D rb;
    public playerStatsController playerStatsController;

    //---------------------------------------------------------------------------------------

    [Header("Arrogance")]

    public float characterNormalPower;
    public float characterArrogancePower;


    public float characterNormalSpeed;
    public float characterArroganceSpeed;

    public float characterNormalJumpForce;
    public float characterArroganceJumpForce;



    public float arroganceCooldown;
    public float arroganceTime;
    public bool canArrogance;

    //---------------------------------------------------------------------------------------

    [Header("Jealous")]


    public bool canJealous;
    public float jealousCooldown;
    public float jealousAmount;

    public float jealousRadius;
    public float jealousDistanceDraw;


    //---------------------------------------------------------------------------------------

    [Header("Lust")]



    public bool canLust;
    public float lustForce;
    public float lustEffectTime;
    public float lustCooldown;

    public float lustRadius;
    public float lustDistanceDraw;

    //---------------------------------------------------------------------------------------

    [Header("Greed")]


    public bool canGreed;
    public float greedCooldown;


    public float greedSoulPullSpeed;


    public float greedRadius;
    public float greedDistanceDraw;

    //---------------------------------------------------------------------------------------

    [Header("Sleep")]


    public bool canSleep;
    private bool isSleep;
    public float sleepCooldown;

    public float sleepHeal;
    private float sleepTime;

    private bool wakeUp;


    //---------------------------------------------------------------------------------------

    [Header("Glutton")]


    public bool canGlutton;

    public float gluttonPowerAmount;
    public float gluttonCooldown;

    //---------------------------------------------------------------------------------------

    [Header("Attack")]

    public bool canAttack;
    public float attackCooldown;


    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;


    private void Awake()
    {
        canGlutton = true;
        canSleep = true;
        canLust = true;
        canGreed = true;
        canJealous = true;
        canArrogance = true;
        canAttack = true;
        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (isSleep)
        {
            sleepTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wakeUp = true;
            }
            horizontal = 0;
            return;

        }


        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayerMask);


        if (horizontal > 0 && !isLookingRight)
            Flip();
        else if (horizontal < 0 && isLookingRight)
            Flip();


        if (math.abs(horizontal) > 0)
            anim.SetBool("canWalk", true);
        else
            anim.SetBool("canWalk", false);




        if (Input.GetKeyDown(KeyCode.Alpha1) && canArrogance)
        {
            StartCoroutine(ArroganceMode());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && canJealous)
        {
            StartCoroutine(JealousMode());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && canLust)
        {
            StartCoroutine(LustMode());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && canGreed)
        {
            StartCoroutine(GreedMode());
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && canSleep)
        {
            StartCoroutine(SleepMode());
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && canGlutton)
        {
            StartCoroutine(GluttonMode());
        }

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackCooldown());
            Attack();
        }

        Jump();



    }



    void FixedUpdate()
    {
        Walk();


    }

    public void Attack()
    {
        anim.SetTrigger("playerAttack");
        Collider2D[] attackedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D col in attackedEnemies)
        {
            if(col.CompareTag("Enemy") || col.CompareTag("flyEnemy"))
            {

            }
        }





    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    IEnumerator GluttonMode()
    {
        canGlutton = false;


        //soul--
        characterPower += gluttonPowerAmount;

        yield return new WaitForSeconds(gluttonCooldown);
        canGlutton = true;
    }

    IEnumerator SleepMode()
    {
        canSleep = false;
        isSleep = true;


        anim.SetTrigger("playerSleep");

        yield return new WaitForSeconds(0.5f);

        anim.SetTrigger("playerSleeping");



        yield return new WaitUntil(() => wakeUp);

        yield return new WaitForSeconds(1f);

        playerStatsController.Damage(-(sleepHeal * sleepTime));
        wakeUp = false;
        isSleep = false;
        sleepTime = 0;
        anim.SetTrigger("playerWakeUp");
        anim.ResetTrigger("playerSleeping");

        yield return new WaitForSeconds(sleepCooldown);
        canSleep = true;


    }


    IEnumerator GreedMode()
    {
        canGreed = false;

        Collider2D[] colliderList = Physics2D.OverlapCircleAll(transform.position, greedRadius);
        foreach (Collider2D col in colliderList)
        {
            if (col.CompareTag("Soul"))
            {
                while (Vector2.Distance(transform.position, col.transform.position) > 1f)
                {
                    col.transform.position = Vector2.Lerp(col.transform.position, transform.position, greedSoulPullSpeed * Time.deltaTime);
                    yield return new WaitForSeconds(0.02f);
                }
                Destroy(col.gameObject);


            }



        }

        yield return new WaitForSeconds(greedCooldown);
        canGreed = true;


    }

    IEnumerator LustMode()
    {
        canLust = false;


        Collider2D[] colliderList = Physics2D.OverlapCircleAll(transform.position, lustRadius);
        List<Collider2D> enemyList = new List<Collider2D>();
        List<float> enemyDistance = new List<float>();

        foreach (Collider2D col in colliderList)
        {
            if (col.CompareTag("Enemy"))
                enemyList.Add(col);

        }

        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)
                enemyDistance.Add(Vector2.Distance(transform.position, enemyList[i].transform.position));


            Collider2D closestEnemy = enemyList[enemyDistance.IndexOf(enemyDistance.Min())];


            float j = 0;
            while (j <= lustEffectTime * 10)
            {
                closestEnemy.transform.position
                    = Vector2.MoveTowards(closestEnemy.transform.position,
                    transform.position, lustForce * Time.deltaTime);
                yield return new WaitForSeconds(0.05f);
                j++;
            }
        }






        yield return new WaitForSeconds(lustCooldown);
        canLust = true;

    }



    IEnumerator JealousMode()
    {
        canJealous = false;

        Collider2D[] colliderList = Physics2D.OverlapCircleAll(transform.position, jealousRadius);
        foreach (Collider2D col in colliderList)
        {
            if (col.CompareTag("Enemy"))
            {

                // col.gameObject.GetComponent<enemyController>().damage(jealousAmount);
                playerStatsController.Damage(-jealousAmount);

            }



        }


        yield return new WaitForSeconds(jealousCooldown);
        canJealous = true;
    }

    IEnumerator ArroganceMode()
    {

        canArrogance = false;
        characterPower = characterArrogancePower;
        characterSpeed = characterArroganceSpeed;
        characterJumpForce = characterArroganceJumpForce;

        /*
         COOLDOWN UI
         ARROGANCE EFFECT
        */

        yield return new WaitForSeconds(arroganceTime);

        characterPower = characterNormalPower;
        characterSpeed = characterNormalSpeed;
        characterJumpForce = characterNormalJumpForce;


        yield return new WaitForSeconds(arroganceCooldown);
        canArrogance = true;

    }







    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, jealousDistanceDraw);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lustDistanceDraw);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, greedDistanceDraw);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded || Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, characterJumpForce);


        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump || Input.GetKeyDown(KeyCode.W) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, characterJumpForce * 2/3);
            canJump = false;


        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * characterJumpForce / 60, ForceMode2D.Impulse);

        }


        if (isGrounded)
        {
            canJump = true;
            anim.SetBool("playerJumping", false);
            anim.SetBool("playerFalling", false);


        }
        else
        {
            anim.SetBool("playerJumping", true);
        }

        if (rb.velocity.y < 0)
        {
            anim.SetBool("playerJumping", false);
            anim.SetBool("playerFalling", true);
        }






    }

    private void Walk()
    {
        rb.velocity = new Vector2(characterSpeed * horizontal * Time.deltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        isLookingRight = !isLookingRight;
        transform.Rotate(0, 180f, 0);
    }


}   
