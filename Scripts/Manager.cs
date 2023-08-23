using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] EnemyPool enemypool;

    public int max_enemy_count = 5; // Set the maximum number of enemies to spawn

    public int current_enemy_count = 0;

    public GameObject EnemyPrefab; // Reference to the prefab of the enemy to be spawned

    public List<Vector3> SpawnPositions; // List of available spawn positions

    List<Vector3> AvailableSpawnPositions; // A separate list to keep track of available spawn positions

    System.Random random = new System.Random();

    public TMP_Text scoreText;
    public int score = 0;

    void Start()
    {
        // Copy the available spawn positions to the AvailableSpawnPositions list
        AvailableSpawnPositions = new List<Vector3>(SpawnPositions);
        StartCoroutine("Spawn");
        EnemyPrefab.SetActive(false);
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        if (current_enemy_count < max_enemy_count)
        {
            int randomIndex = random.Next(0, AvailableSpawnPositions.Count);
            Vector3 spawnPosition = AvailableSpawnPositions[randomIndex];
            EnemyController enemy = enemypool.GetEnemy();
            enemy.gameObject.transform.position = spawnPosition;
            //GameObject enemy =
            //    Instantiate(EnemyPrefab,
            //    spawnPosition,
            //    Quaternion.identity);
            // enemy.SetActive(true);
            enemy.manager = this;
            current_enemy_count++;
        }
        StartTimerAgain();
    }

    void StartTimerAgain()
    {
        StopCoroutine("Spawn");
        StartCoroutine("Spawn");
    }
}
