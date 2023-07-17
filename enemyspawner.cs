using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform target; 
    public float spawnInterval = 2f; // Düşmanların yeniden doğma aralığı
    public int maxEnemies = 5; // Aynı anda var olabilecek maksimum düşman sayısı

    private float spawnTimer; // Yeniden doğma süresini takip eden sayaç
    private int currentEnemyCount; // Mevcut düşman sayısı

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        // Yeniden doğma süresini takip eder
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval; 
        }
    }

    private void SpawnEnemy()
    {
        // Düşman prefab'inden yeni bir düşman oluşturur
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Oluşturulan düşmanı ana karakterin yönüne doğru yönlendirir
        Vector2 direction = (target.position - transform.position).normalized;
        newEnemy.GetComponent<EnemyMovement>().SetDirection(direction);

        // Düşman sayısını bir arttırır
        currentEnemyCount++;
    }

    public void EnemyDestroyed()
    {
        // Düşman yok edildiğinde düşman sayısını azaltır
        currentEnemyCount--;
    }
}
