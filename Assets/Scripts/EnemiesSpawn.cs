using System.Collections;
using UnityEngine;

public class EnemiesSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public int maxEnemies = 3;
    public Transform mainCameraTransform;

    private int currentEnemies = 0;
    private readonly float spawnRadius = 5f;
    private readonly float spawnDelay = 3f;

    public static EnemiesSpawn Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void OnEnemyDestroyed()
    {
        currentEnemies--;
        StartCoroutine(SpawnEnemyWithDelay(spawnDelay));
    }

    private IEnumerator SpawnEnemyWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies) return;

        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new(playerTransform.position.x + randomPoint.x, 0, playerTransform.position.z + randomPoint.y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies++;
    }
}