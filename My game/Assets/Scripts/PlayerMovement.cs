using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public float checkradius;
    public float moveinput;
    private bool isgrounded;
    public Transform groundcheck;
    public LayerMask thisground;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public Animator animator;
    private float jumptimecounter;
    public float jumptime;
    public bool isjumping;
    //public float wallslidingspeed;
    //bool istouchwall;
    //public Transform wallcheck;
    //bool wallsliding;
    //bool walljumping;
    //public float xwallforce;
    //public float ywallforce;
    //public float walljumptime;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Flip();
        Groudcheck();
        moveinput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveinput * speed, rb.velocity.y);
    }
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveinput));
        if (isgrounded == true)
        {
            animator.SetBool("Isjumping", false);
            animator.SetBool("Isfalling", false);
        }
        if (isgrounded == false)
        {
            animator.SetBool("Isjumping", true);
        }
        if (rb.velocity.y < 0)
        {
            animator.SetBool("Isjumping", false);
            animator.SetBool("Isfalling", true);
        }
        Jump();
        //Slide();
    }

    void Jump()
    {
        if (isgrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isjumping = true;
            jumptimecounter = jumptime;
            rb.velocity = Vector2.up * jumpforce;
        }
        if (Input.GetKey(KeyCode.Space) && isjumping == true)
        {
            if (jumptimecounter > 0)
            {
                rb.velocity = Vector2.up * jumpforce;
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                isjumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isjumping = false;
        }
    }
    void Flip()
    {
        if (moveinput > 0)
        {
            sr.flipX = false;
        }
        else if (moveinput < 0)
        {
            sr.flipX = true;
        }

    }
    void Groudcheck()
    {
        isgrounded = Physics2D.OverlapCircle(groundcheck.position, checkradius, thisground);
    }
    //void Slide() // отпрыгивание и скольжение от стен,нормально не работает
    //{
    //    istouchwall = Physics2D.OverlapCircle(wallcheck.position, checkradius, thisground);
    //    if (istouchwall == true && isgrounded == false && moveinput != 0)
    //    {
    //        wallsliding = true;
    //    }
    //    else
    //    {
    //        wallsliding = false;
    //    }
    //    if (wallsliding)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallslidingspeed, float.MaxValue));
    //    }
    //    if (Input.GetButtonDown("Jump") && wallsliding == true)
    //    {
    //        walljumping = true;
    //        Invoke("Setwalljumpingtofalse", walljumptime);
    //    }
    //    if (walljumping == true)
    //    {
    //        rb.velocity = new Vector2(xwallforce * -moveinput, ywallforce);
    //    }
    //}
    //void Setwalljumpingtofalse()
    //{
    //    walljumping = false;
    //}
}
