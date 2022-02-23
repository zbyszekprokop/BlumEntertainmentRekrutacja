using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]int startingHealth=2;
    EnemyController enemyController;
    public Animator animator;
    public AudioSource enemySFX;
    public AudioClip enemyHit;
    public AudioClip enemyDies;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        currentHealth = startingHealth;
    }
    public void EnemyTakeDamage(int damage)
    {
        currentHealth-=damage;
        enemySFX.PlayOneShot(enemyHit);
        animator.SetTrigger("Hit");
        if(currentHealth < 1)
        {
            Death();
        }
    }
    void Death()
    {
        animator.SetBool("isDead", true);
        enemySFX.PlayOneShot(enemyDies);
        enemyController.enabled=false;
        GetComponent<BoxCollider2D>().enabled=false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        this.enabled=false;
    }
}
