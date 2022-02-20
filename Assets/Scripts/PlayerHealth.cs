using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]int startingHealth =3;
    [SerializeField]Image[] hearts;
    public Animator animator;
    PlayerController playerController;
    Rigidbody2D rb;
    int currentHealth=3;
    float knockbackForce = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingHealth = currentHealth;
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage()
    {
        if(currentHealth >1)
        {
            LoseHeart(1);
        }
        else
        {
            LoseHeart(1);
            animator.SetBool("isDead", true);
            playerController.enabled = false;
            Invoke("RestartLevel", 2f);
        }
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        { 
            Vector2 direction = new Vector2(transform.position.x, transform.position.y) - new Vector2(other.transform.position.x, other.transform.position.y);
            rb.AddForce(new Vector2(direction.x*7 ,knockbackForce),ForceMode2D.Impulse);
            TakeDamage();
        }
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoseHeart(int damage)
    {
        currentHealth-=damage;
        hearts[currentHealth].enabled=false;
    }
}
