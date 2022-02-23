using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField]Transform checkGround;
    [SerializeField]LayerMask groundLayer;
    [SerializeField]bool isPatroling=true;
    [SerializeField]float enemyRange =1f;
    [SerializeField]float attackCooldown=0.5f;
    [SerializeField]LayerMask playerLayer;
    public Transform target;
    public Transform enemyAttackCheck;
    public Rigidbody2D rb;
    public Animator animator;
    EnemyHealth enemyHealth;
    bool shouldFlip;
    public float timeToAttack;
    float knockbackForce=3f;

    void Start()
    {
        enemyHealth=GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if(timeToAttack >0)
        {
            timeToAttack -= Time.deltaTime;
        }
        else if(timeToAttack <=0)
        {
            EnemyAttack();
            timeToAttack=attackCooldown;
        }
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
    void EnemyAttack()
    {
        if(enemyAttackCheck==null)return;
        Collider2D[]playerHit =  Physics2D.OverlapCircleAll(enemyAttackCheck.position, enemyRange, playerLayer);
        foreach(Collider2D player in playerHit)
        {
            Vector2 direction = new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            target.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x*4 ,knockbackForce),ForceMode2D.Impulse);
            player.GetComponent<PlayerHealth>().TakeDamage();
            animator.SetTrigger("Attack");
        }
    }
    void OnDrawGizmosSelected() 
    {
        if(enemyAttackCheck==null)
        return;
        Gizmos.DrawWireSphere(enemyAttackCheck.position, enemyRange);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player")&&enemyHealth.currentHealth>1)
        {
            animator.SetTrigger("Stomp");
            Vector2 direction = new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(transform.position.x, transform.position.y);
            target.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x ,knockbackForce*4),ForceMode2D.Impulse);
        }
    }
}
