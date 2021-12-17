using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    //start()variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    

    

    //fsm
    private enum State { idle,running,jumping,falling,hurt}
    private State state = State.idle;
    
    //ınspector varıables
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 6f;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private int health;
    [SerializeField] private Text healthAmount;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        permauı.perm.healthAmount.text =permauı.perm.health.ToString();
        
    }
    private void Update()
    {
        if(state != State.hurt)
        {
            movement();
        }
        
        AnimationState();
        anim.SetInteger("state", (int)state);//sets animation based on Enumator state
    }

    private void movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        //moving left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        //moving right
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }

        //jumping VİDEO 34 COMMENT CODE
        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1.3f, Ground);
            if (hit.collider != null)
                Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jumping;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            cherry.Play();
            Destroy(collision.gameObject);
            permauı.perm.cherries += 1;
            permauı.perm.cherryText.text = permauı.perm.cherries.ToString();
        }
        if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            jumpforce = 25f;
            GetComponent<SpriteRenderer>().color = Color.green;
            StartCoroutine(ResetPower());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")  
        
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy
                >();
            if (state == State.falling)
            {

                enemy.JumpedOn();
                Jump();
                
                
                
            }
            else
            {
                state = State.hurt;
                HandleHealth();//deals with helath updating uı,and will reset level if health is <=0

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //enemy is to my right therefore ı should be damaged and move left.
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //enemy is to my left therefore ı should be damaged and move right.
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void HandleHealth()
    {
        permauı.perm.health -= 1;
        permauı.perm.healthAmount.text = permauı.perm.health.ToString();
        if (permauı.perm.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void AnimationState()
    {
        if(state==State.jumping)
        {
            if(rb.velocity.y<.1f)
            {
                state = State.falling;
            }
            

        }
        else if (state==State.falling)
        {
            if(coll.IsTouchingLayers(Ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }

        
        else if(Mathf.Abs(rb.velocity.x)>4f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(8);
        jumpforce = 18;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    

    


    

    
    

    
}
