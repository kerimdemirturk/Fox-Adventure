using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy 
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumplength=10f;
    [SerializeField] private float jumpHeight=15f;
    [SerializeField] private LayerMask ground;

    private Collider2D coll;
    private Rigidbody2D rb;
    


    private bool facingLeft = true;
    protected override void Start()
    {
       base.Start();
      coll= GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        //transation from jump to fall
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
                
            }
        }

        //transation from fall to ıdle
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            if (rb.velocity.x < .1)
            {
                anim.SetBool("Falling", false);
                
            }
        }
    }

    private void move()
    {
        if (facingLeft)
        {

            if (transform.position.x > leftCap)
            {
                //make sure sprite facing right location and if its not then face the right direction 
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                //test to see if ı am on the ground if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    //jump
                    rb.velocity = new Vector2(-jumplength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }

        }
        else
        {
            if (transform.position.x < rightCap)
            {
                //make sure sprite facing right location and if its not then face the right direction 
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                //test to see if ı am on the ground if so jump
                if (coll.IsTouchingLayers(ground))
                {
                    //jump
                    rb.velocity = new Vector2(jumplength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }

        }
    }

    public void JumpedOn()
    {
        anim.SetTrigger("Death");
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
