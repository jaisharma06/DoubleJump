﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private Blade enemyPrefab;
    [SerializeField]
    private List<Transform> spawnPoints;
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private int totalEnemies = 10;
    [SerializeField]
    private float spawnDuration = 2f;
    private List<Blade> enemyPool;

    private bool generate = true;

    public int enemiesSpawned = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        InstantiateEnemies();
		StartCoroutine(GenerateEnemies());
    }

    private void InstantiateEnemies()
    {
        if (enemyPool == null)
        {
            enemyPool = new List<Blade>();
        }
        for (int i = 0; i < totalEnemies; i++)
        {
            var enemy = Instantiate(enemyPrefab, enemyParent);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    private void SpawnEnemy(Transform spawnPoint)
    {
        var enemy = enemyPool.First(e => e.isActive == false);
        if (enemy)
        {
            enemy.position = spawnPoint.position;
            if (enemy.position.x > 0) { enemy.SetDirection(-1); } else { enemy.SetDirection(1); }
            enemy.SetActive(true);
            enemiesSpawned++;
        }
    }

    private IEnumerator GenerateEnemies()
    {
        while (generate)
        {
            yield return new WaitForSeconds(spawnDuration);
            SpawnEnemy(spawnPoints.GetRandom(0, 2));
        }
    }
}