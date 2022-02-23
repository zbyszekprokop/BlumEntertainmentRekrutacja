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
    public AudioSource playerSFX;
    public AudioClip playerHit;
    public AudioClip playerDies;
    public AudioClip playerHealthUp;
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
            PlayerDies();
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
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Potion"))
        {
            if(currentHealth<3)
            {
                AddHeart();
                Destroy(other.gameObject);
            }
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
        playerSFX.PlayOneShot(playerHit);
    }
    void AddHeart()
    {
        playerSFX.PlayOneShot(playerHealthUp);
        hearts[currentHealth].enabled=true;
        currentHealth++;
    }
    void PlayerDies()
    {
            LoseHeart(1);
            playerSFX.PlayOneShot(playerDies);
            animator.SetBool("isDead", true);
            playerController.enabled = false;
            gameObject.GetComponent<Collider2D>().enabled=false;
            Invoke("RestartLevel", 2f);        
    }
}
