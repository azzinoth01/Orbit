using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{


    public List<Enemy_Spawner_Info> enemysToSpawn;
    public int spawnLimit;
    private int currentSpawnCount;
    // Start is called before the first frame update
    void Start() {
        currentSpawnCount = 0;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnEnable() {
        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == false && e.SpawnStartet == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
    }
    private IEnumerator startSpawntimer(float wait, Enemy_Spawner_Info enemySpawnInfo) {



        yield return new WaitForSeconds(wait);
        // spawn
        if (spawnLimit >= currentSpawnCount || spawnLimit == 0) {

            currentSpawnCount = currentSpawnCount + 1;
            Instantiate(enemySpawnInfo.enemyPrefab, Vector3.zero, Quaternion.identity, transform);
            if (enemySpawnInfo.enemysToSpawn == 0 || enemySpawnInfo.enemysToSpawn > enemySpawnInfo.CurrentEnemysSpawned) {
                enemySpawnInfo.CurrentEnemysSpawned = enemySpawnInfo.CurrentEnemysSpawned + 1;
                StartCoroutine(startSpawntimer(wait, enemySpawnInfo));
            }
        }


    }

    public void checkSpawnTrigger(GameObject trigger) {
        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == true && trigger == e.triggerArea && e.SpawnStartet == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
    }
}
