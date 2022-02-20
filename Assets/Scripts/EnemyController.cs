using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField]Transform checkGround;
    [SerializeField]LayerMask groundLayer;
    [SerializeField]bool isPatroling=true;
    bool shouldFlip;
    public Rigidbody2D rb;
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPatroling)
        {
            shouldFlip = !Physics2D.OverlapCircle(checkGround.position, 0.2f, groundLayer);
            Patrol();
            animator.SetBool("Patrol", true);
        }
        else
        {
            animator.SetBool("Patrol", false);
        }
    }
    void Patrol()
    {
        if(shouldFlip)
        {
            FlipEnemy();
        }
        rb.velocity=new Vector2(-speed, rb.velocity.y);
    }
    void FlipEnemy()
    {
        isPatroling=false;
        transform.localScale = new Vector2(transform.localScale.x* -1, transform.localScale.y);
        speed*=-1;
        isPatroling=true;
    }
}
