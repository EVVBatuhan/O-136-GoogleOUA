using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public float movementSpeed = 3f;
    public int health = 100;
    public int damage = 10;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;

    private Transform player;
    private bool canAttack = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Düşmanın oyuncuya doğru hareket etmesi için
        transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        // Oyuncuyla olan mesafeyi kontrol etmek ve saldırı yapmak için
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        canAttack = false;
        player.GetComponent<Player>().TakeDamage(damage);
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Düşmanın ölüm işlemleri
        Destroy(gameObject);
    }
}
