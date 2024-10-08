using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Object Pools")]
    [SerializeField] private List<GameObject> obstacleList;
    [SerializeField] private List<GameObject> powerUpList;
    [Header("Spawners")]
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    [SerializeField] private Transform deletePoint;

    private List<GameObject> obstacles = new List<GameObject>();
    private float obstacleSpawnTime = 1.5f;
    private float timeUntilObstacleSpawn;
    private float obstacleSpeed = 5f;
    private int obstaclePoolSize = 50;

    private List<GameObject> powerUps = new List<GameObject>();
    private float powerUpSpawnTime = 10f;
    private float timeUntilPowerUpSpawn;
    private float powerUpSpeed = 5f;
    private int powerUpPoolSize = 10;

    private void Awake()
    {
        for (int i = 0; i < obstaclePoolSize; i++)
        {
            GameObject selectedPrefab = obstacleList[Random.Range(0, obstacleList.Count)];
            GameObject obstacle = Instantiate(selectedPrefab, spawnPoint1.position, Quaternion.identity);
            obstacles.Add(obstacle);
            obstacle.SetActive(false);
        }

        for (int i = 0; i < powerUpPoolSize; i++)
        {
            GameObject powerUpJump = Instantiate(powerUpList[0], spawnPoint1.position, Quaternion.identity);
            powerUps.Add(powerUpJump);
            powerUpJump.SetActive(false);
        }
    }

    private void Update()
    {
        HandleObstacleSpawn();
        HandlePowerUpSpawn();
        DeactivateObject();
    }

    private void HandleObstacleSpawn()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn > obstacleSpawnTime)
        {
            SpawnObstacle();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void HandlePowerUpSpawn()
    {
        timeUntilPowerUpSpawn += Time.deltaTime;

        if (timeUntilPowerUpSpawn > powerUpSpawnTime)
        {
            SpawnPowerUp();
            timeUntilPowerUpSpawn = 0f;
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacleToActivate;
        int attempts = 0;
        bool isBlockedPosition = false;
        float radius = 1f;

        do
        {
            isBlockedPosition = false;
            obstacleToActivate = obstacles[Random.Range(0, obstacles.Count)];
            attempts++;

            // Evitar un bucle infinito si no hay ninguno desactivado.
            if (attempts > 200)
            {
                Debug.LogWarning("No se encontró un obstáculo desactivado en 200 intentos.");
                return;
            }

            foreach (GameObject powerUp in powerUps)
            {
                if (powerUp.activeInHierarchy)
                {
                    float distance = Vector3.Distance(powerUp.transform.position, spawnPoint1.position);
                    if (distance < radius * 2)
                    {
                        isBlockedPosition = true;
                        break;
                    }
                }
            }

        } while (obstacleToActivate.activeInHierarchy || isBlockedPosition);

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

    private void SpawnPowerUp()
    {
        GameObject powerUpToActivate;
        bool isBlockedPosition = false;
        float radius = 1f;
        int attempts = 0;

        do
        {
            isBlockedPosition = false;
            powerUpToActivate = powerUps[Random.Range(0, powerUps.Count)];
            attempts++;

            if (attempts > 200)
            {
                Debug.LogWarning("No se encontró un power-up desactivado en 200 intentos.");
                return;
            }

            foreach (GameObject obstacle in obstacles)
            {
                if (obstacle.activeInHierarchy)
                {
                    float distance = Vector3.Distance(obstacle.transform.position, spawnPoint1.position);
                    if (distance < radius * 2)
                    {
                        isBlockedPosition = true;
                        break;
                    }
                }
            }

        } while (powerUpToActivate.activeInHierarchy || isBlockedPosition);

        powerUpToActivate.transform.position = spawnPoint1.position;
        powerUpToActivate.SetActive(true);

        Rigidbody2D powerUpRb2D = powerUpToActivate.GetComponent<Rigidbody2D>();
        powerUpRb2D.velocity = Vector2.left * powerUpSpeed;
    }

    private void DeactivateObject()
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

        foreach (GameObject powerUp in powerUps)
        {
            if (powerUp.activeInHierarchy && powerUp.transform.position.x < deletePoint.position.x)
            {
                powerUp.SetActive(false);
                Rigidbody2D powerUpRb2D = powerUp.GetComponent<Rigidbody2D>();
                powerUpRb2D.velocity = Vector2.zero;
            }
        }
    }

    public void DeactivateAllObjects()
    {
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
            Rigidbody2D obstacleRb2D = obstacle.GetComponent<Rigidbody2D>();
            obstacleRb2D.velocity = Vector2.zero;
        }

        foreach (GameObject powerUp in powerUps)
        {
            powerUp.SetActive(false);
            Rigidbody2D powerUpRb2D = powerUp.GetComponent<Rigidbody2D>();
            powerUpRb2D.velocity = Vector2.zero;
        }
    }
}
