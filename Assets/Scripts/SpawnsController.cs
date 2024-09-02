using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsController : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float spawnInterval = 2.25f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        //infinite loop
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            //randomly select enemy from array
            int randomEnemyIndex = Random.Range(0, enemies.Length);
            GameObject selectedEnemy = enemies[randomEnemyIndex];

            //randomly select spawn point from array
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedSpawnPoint = spawnPoints[randomSpawnIndex];

            Instantiate(selectedEnemy, selectedSpawnPoint.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
