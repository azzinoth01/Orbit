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
        if (Globals.pause == true) {
            return;
        }
        else {
            if (spawnLimit > currentSpawnCount || spawnLimit == 0) {
                foreach (Enemy_Spawner_Info e in enemysToSpawn) {
                    if (e.SpawnConditonFulfilled == true && e.SpawnStartet == false) {
                        StartCoroutine(startSpawntimer(e.delay, e));
                        e.SpawnStartet = true;
                    }
                }
            }
        }
    }

    private void OnEnable() {
        Globals.spawnerListe.Add(this);
        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == false && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
    }
    private void OnDisable() {
        Globals.spawnerListe.Remove(this);
    }
    private IEnumerator startSpawntimer(float wait, Enemy_Spawner_Info enemySpawnInfo) {



        yield return new WaitForSeconds(wait);
        // spawn
        if (spawnLimit > currentSpawnCount || spawnLimit == 0) {

            currentSpawnCount = currentSpawnCount + 1;
            GameObject g = Instantiate(enemySpawnInfo.enemyPrefab, transform);
            g.GetComponentInChildren<Enemy>(true).SpawnerCallback = this;
            if (enemySpawnInfo.enemysToSpawn == 0 || enemySpawnInfo.enemysToSpawn > enemySpawnInfo.CurrentEnemysSpawned + 1) {
                enemySpawnInfo.CurrentEnemysSpawned = enemySpawnInfo.CurrentEnemysSpawned + 1;
                StartCoroutine(startSpawntimer(wait, enemySpawnInfo));
            }
            else {
                enemySpawnInfo.SpawnConditonFulfilled = false;

            }
        }
        else {
            enemySpawnInfo.SpawnStartet = false;
        }


    }

    public void checkSpawnTrigger(GameObject trigger) {
        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == true && trigger == e.triggerArea && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
    }

    public void spawnKilled() {
        currentSpawnCount = currentSpawnCount - 1;
    }
}
