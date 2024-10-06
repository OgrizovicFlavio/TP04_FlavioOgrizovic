using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    [SerializeField] private Transform deletePoint;

    private List<GameObject> obstacles = new List<GameObject>();
    private float obstacleSpawnTime = 2f;
    private float timeUntilObstacleSpawn;
    private float obstacleSpeed = 5f;
    private float poolSize = 30;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject selectedPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
            GameObject obstacle = Instantiate(selectedPrefab, spawnPoint1.position, Quaternion.identity);
            obstacles.Add(obstacle);
            obstacle.SetActive(false);
        }
    }

    private void Update()
    {
        SpawnLoop();
        DeactivateObstacle();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn > obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToActivate;
        int attempts = 0;

        do
        {
            obstacleToActivate = obstacles[Random.Range(0, obstacles.Count)];
            attempts++;

            // Evitar un bucle infinito si no hay ninguno desactivado.
            if (attempts > 100)
            {
                Debug.LogWarning("No se encontró un obstáculo desactivado en 100 intentos.");
                return;
            }

        } while (obstacleToActivate.activeInHierarchy);

        Transform selectedRandomSpawnPoint = GetRandomSpawnPoint();
        obstacleToActivate.transform.position = selectedRandomSpawnPoint.position;
        obstacleToActivate.SetActive(true);

        Rigidbody2D obstacleRb2D = obstacleToActivate.GetComponent<Rigidbody2D>();
        obstacleRb2D.velocity = Vector2.left * obstacleSpeed;
    }

    private Transform GetRandomSpawnPoint()
    {
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                return spawnPoint1;
            case 1:
                return spawnPoint2;
            case 2:
                return spawnPoint3;
            default:
                return spawnPoint1;
        }
    }

    private void DeactivateObstacle()
    {
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.activeInHierarchy && obstacle.transform.position.x < deletePoint.position.x)
            {
                obstacle.SetActive(false);
                Rigidbody2D obstacleRb2D = obstacle.GetComponent<Rigidbody2D>();
                obstacleRb2D.velocity = Vector2.zero;
            }
        }
    }
}
