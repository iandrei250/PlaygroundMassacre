using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyAI enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxEnemies;
    [SerializeField] private Player player;

    private List<EnemyAI> spawnedEnemies = new List<EnemyAI>();
    private float timeSinceLastSpawn ;
    

    private void Start() {
        timeSinceLastSpawn = spawnInterval;
    }

    private void Update() {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn > spawnInterval){
           timeSinceLastSpawn = 0f; 
           if(spawnedEnemies.Count < maxEnemies)
           {
               SpawnEnemy();
           }
        }

    }

    private void SpawnEnemy() {
        EnemyAI enemy = Instantiate(enemyPrefab,transform.position,transform.rotation);
        int spawnPointIndex = spawnedEnemies.Count % spawnPoints.Length;
        enemy.Init(player, spawnPoints[spawnPointIndex]);
        spawnedEnemies.Add(enemy);
    }
}
