using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe die das Spawnen von Enemys controlliert
/// </summary>
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

            // spawnen neustartet nachdem sie gestopped sind, wenn das spawnlimit erreicht wurde
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

    /// <summary>
    /// startet alle spawn timer die nicht auf trigger areas angewiesen sind
    /// </summary>
    private void OnEnable() {
        Globals.spawnerListe.Add(this);
        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == false && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
    }

    /// <summary>
    /// entfernt den spawner aus der Globalen Spawner liste
    /// </summary>
    private void OnDisable() {
        Globals.spawnerListe.Remove(this);
    }

    /// <summary>
    /// Spawner Timer der enemys spawnt
    /// </summary>
    /// <param name="wait"> zeit in Sekunden um den enemy zu spawnen</param>
    /// <param name="enemySpawnInfo"> information welchen enemy und wie viel davon zu spawnen sind</param>
    /// <returns> corutine</returns>
    private IEnumerator startSpawntimer(float wait, Enemy_Spawner_Info enemySpawnInfo) {



        yield return new WaitForSeconds(wait);
        // spawn
        if (spawnLimit > currentSpawnCount || spawnLimit == 0) {

            currentSpawnCount = currentSpawnCount + 1;
            GameObject g = Instantiate(enemySpawnInfo.enemyPrefab, transform);
            // callback setzten, um spawncounter zu verringern
            g.GetComponentInChildren<Enemy>(true).SpawnerCallback = this;
            if (enemySpawnInfo.enemysToSpawn == 0 || enemySpawnInfo.enemysToSpawn > enemySpawnInfo.CurrentEnemysSpawned + 1) {
                enemySpawnInfo.CurrentEnemysSpawned = enemySpawnInfo.CurrentEnemysSpawned + 1;
                StartCoroutine(startSpawntimer(wait, enemySpawnInfo));
            }
            else {
                // spawner fertig
                enemySpawnInfo.SpawnConditonFulfilled = false;

            }
        }
        else {
            enemySpawnInfo.SpawnStartet = false;
        }


    }

    /// <summary>
    /// check Spawntimer die auf trigger areas reagiert
    /// </summary>
    /// <param name="trigger"> trigger area die ausgelöst worden ist </param>
    public void checkSpawnTrigger(GameObject trigger) {
        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == true && trigger == e.triggerArea && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
    }

    /// <summary>
    /// veringert den spawn count nachdem ein enemy zerstört worden ist
    /// </summary>
    public void spawnKilled() {
        currentSpawnCount = currentSpawnCount - 1;
    }
}
