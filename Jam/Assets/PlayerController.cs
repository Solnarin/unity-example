using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{

    [Header("Player Stats")]

    [SerializeField]
    private float walkSpeed;
    private float horizontal;
    public float jumpForce;


    [Header("Double Jump")]
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayerMask;
    public float checkRadius;
    private bool canJump = true;

    private bool isAlive = true;
    private int soulCount = 0;

    private bool isLookingRight = true;




    Animator anim;
    Rigidbody2D rb;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayerMask);


        if (horizontal > 0 && !isLookingRight)
            Flip();
        else if (horizontal < 0 && isLookingRight)
            Flip();
        

        if(math.abs(horizontal)  > 0)
            anim.SetBool("canWalk", true);
        else
            anim.SetBool("canWalk", false);


        Jump();




    }


    void FixedUpdate()
    {
        Walk();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Soul"))
        {
            soulCount += 1;
            print(soulCount);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("void") && isAlive) 
        {
<<<<<<< HEAD
            if (col.CompareTag("Soul") && col.gameObject != null)
            {
                while (Vector2.Distance(transform.position, col.transform.position) > 1.4f && col.gameObject != null)
                {
                    col.transform.position = Vector2.Lerp(col.transform.position, transform.position, greedSoulPullSpeed * Time.deltaTime);
                    yield return new WaitForSeconds(0.02f);
                }
                characterSoulAmount++;
                Destroy(col.gameObject);


            }



=======
            isAlive = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
>>>>>>> eec46983b087466886861257c0e6c4d947c087d5
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded || Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            //anim.SetBool("isJumping", true);
            //anim.SetTrigger("takeOff");

        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && canJump || Input.GetKeyDown(KeyCode.W) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*2/3);
            canJump = false;

            // anim.SetBool("isJumping", true);

        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * jumpForce / 60, ForceMode2D.Impulse);

        }


        if (isGrounded)
        {
            canJump = true;
            // anim.SetBool("isJumping", false);
        }
        else
        {
            // anim.SetBool("isJumping", true);
        }


    }

    private void Walk()
    {
        rb.velocity = new Vector2(walkSpeed * horizontal * Time.deltaTime, rb.velocity.y);
    }

    private void Flip()
    {
        isLookingRight = !isLookingRight;
        transform.Rotate(0, 180f, 0);
    }


}
