using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    //
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float maxXLeft, maxXRight;
    public float delay = 3;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(delay);
        var spawnedEnemy = Instantiate(enemies[Random.Range(0, enemies.Length - 1)], enemyParent);
        spawnedEnemy.transform.position =
            new Vector3(Random.Range(maxXLeft, maxXRight), spawnedEnemy.transform.position.y, 0);
        //
        StartCoroutine(SpawnEnemy());
    }
}
