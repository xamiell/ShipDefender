using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    [SerializeField] float maxSpawnTime = 5f;
    [SerializeField] float minSpawnTime = 0.5f;
    [SerializeField] bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnEnemy));
    }

    private IEnumerator SpawnEnemy()
    {
        var enemy = default(int);

        while (canSpawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            var e = Instantiate(enemies[enemy], transform.position, Quaternion.identity);

            enemy = (enemy == 1) ? 0 : enemy += 1;
        }
    }
}
