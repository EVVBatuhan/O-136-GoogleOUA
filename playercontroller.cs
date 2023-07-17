using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        public Transform groundCheck;
        public LayerMask groundLayer;
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float fireRate = 0.5f;
        public float nextFireTime = 0f;
        public int maxHealth = 100;
        public int currentHealth;

        private Rigidbody2D rb;
        private bool isFacingRight = true;
        private bool isGrounded = false;

        // Health bar
        public HealthBar healthBar;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        void Update()
        {
            // Yerde olup olmadýðýný kontrol etmek için
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

            // Saða gitme
            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                if (!isFacingRight)
                    Flip();
            }
            // Sola gitme
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                if (isFacingRight)
                    Flip();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            // Zýplama
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            // Ateþ etme
            if (Input.GetMouseButton(0) && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                Shoot();
            }
        }

        void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        //ölüm durumu
        void Die()
        {
            SceneManager.LoadScene("GameOverScene");
        }
        void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

}
