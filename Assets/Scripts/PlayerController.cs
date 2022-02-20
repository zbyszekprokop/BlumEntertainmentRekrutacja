using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField]float movementSpeed = 6f;
   [SerializeField]float jumpForce = 8f;
   [SerializeField]float attackRange =0.5f;
   [SerializeField]LayerMask enemyLayer;
   [SerializeField]LayerMask groundLayer;
   public Animator animator;
   public Transform attackCheck;
   
   private Rigidbody2D rb;
   private BoxCollider2D boxcoll;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxcoll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Attack();
        FlipHero();
        
    }
    void Move()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            // rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            rb.AddForce(new Vector2(-movementSpeed,0), ForceMode2D.Impulse);
            animator.SetBool("isMoving", true);
        }
    
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            // rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            rb.AddForce(new Vector2(movementSpeed,0), ForceMode2D.Impulse);
            animator.SetBool("isMoving", true);
        }
        else 
       {
        //    rb.velocity = new Vector2(0,rb.velocity.y);
           animator.SetBool("isMoving", false);
       }
  }        
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force); 
        }
        if(!isGrounded())
        {
            animator.SetFloat("yVelocity", rb.velocity.y);
            animator.SetBool("isJumping", true);
        }
        if(isGrounded())
        {
            animator.SetBool("isJumping", false);
        }        
    }
    void Attack()
    {
        if(Input.GetKeyDown("space"))
        {
            animator.SetBool("isAttacking", true);
            Collider2D[]enemyHit =  Physics2D.OverlapCircleAll(attackCheck.position, attackRange, enemyLayer);
            foreach(Collider2D enemy in enemyHit)
            {
                enemy.GetComponent<EnemyHealth>().EnemyTakeDamage(1);
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    void OnDrawGizmosSelected() 
    {
        if(attackCheck==null)
        return;
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
    }
    void FlipHero()
    {
        Vector3 characterScale = transform.localScale;
        if(Input.GetAxis("Horizontal")<0)
        {
            characterScale.x = -1;
        }
        if(Input.GetAxis("Horizontal")>0)
        {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    
}