﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Collider2D coll;
    protected AudioSource death;
    


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        death= GetComponent<AudioSource>();
        
        

    }

    public void JumpedOn()
    {
        anim.SetTrigger("Death");
        death.Play();

        
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
