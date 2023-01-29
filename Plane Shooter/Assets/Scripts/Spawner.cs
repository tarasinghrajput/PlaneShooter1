using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject []enemy;
    public float respawnTime = 2f;
    public int enemySpawnCount = 10;
    public GameController gameController;
    private bool lastEnemySpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (lastEnemySpawned&&FindObjectOfType<EnemyScript>() == null)
        {
            StartCoroutine(gameController.LevelComplete());
        }
    }

    IEnumerator EnemySpawner()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
        yield return new WaitForSeconds(respawnTime);
        SpawnEnemy(); 
        }
        lastEnemySpawned = true;
    }

    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length);
        int randomXPosition = Random.Range(-2, 2);
        Instantiate(enemy[randomValue], new Vector2(randomXPosition, transform.position.y), Quaternion.identity);
    }
}
