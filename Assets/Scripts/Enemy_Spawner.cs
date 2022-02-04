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

    public float delay;
    public GameObject triggerArea;
    public bool useTriggerArea;
    private bool isActive;


    public bool useRandomSpawnInfo;

    public bool waveSpawner;

    public bool useModification;

    public List<Vector2> modifyMoveIn;
    public List<Vector2> modifyMoveOut;
    public List<Vector2> modifyWaypoints;

    //public float modifyHealth;
    public float modifyAddHealth;
    public float modifyAddDmg;
    public bool modifyDoNotUseForce;



    /// <summary>
    /// startet alle gestoppten spawner neu,wenn diese neugestartet werden können
    /// </summary>
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else if (isActive == true) {
            if (useRandomSpawnInfo == true) {

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
    }

    /// <summary>
    /// startet den Timer für den Spawner selbst
    /// </summary>
    private void OnEnable() {
        isActive = false;
        currentSpawnCount = 0;

        Globals.spawnerListe.Add(this);


        if (useTriggerArea == true) {
            return;
        }
        else {

            StartCoroutine(spawnerActivationTimer(delay));
        }
    }


    /// <summary>
    /// startet alle spawn timer die nicht auf trigger areas angewiesen sind
    /// </summary>
    private void activateSpawning() {

        isActive = true;

        if (useRandomSpawnInfo == true) {
            if (enemysToSpawn.Count != 0) {
                int i = Random.Range(0, enemysToSpawn.Count);

                StartCoroutine(startSpawntimer(enemysToSpawn[i].delay, enemysToSpawn[i]));

            }

        }
        else {
            foreach (Enemy_Spawner_Info e in enemysToSpawn) {
                if (e.useTriggerArea == false && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                    StartCoroutine(startSpawntimer(e.delay, e));
                    e.SpawnStartet = true;
                }

            }
        }
    }


    /// <summary>
    /// entfernt den spawner aus der Globalen Spawner liste
    /// </summary>
    private void OnDisable() {
        Globals.spawnerListe.Remove(this);
        isActive = false;
        currentSpawnCount = 0;
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

            if (useModification == true) {
                try {
                    g.GetComponent<Move_in_out_Scene>().moveInWaypoints = modifyMoveIn;
                    g.GetComponent<Move_in_out_Scene>().moveOutWaypoints = modifyMoveOut;
                }
                catch {

                }
                g.transform.GetChild(0).gameObject.GetComponent<Enemy>().waypoints = modifyWaypoints;
                //g.transform.GetChild(0).gameObject.GetComponent<Enemy>().health = modifyHealth;
                g.transform.GetChild(0).gameObject.GetComponent<Enemy>().health = g.transform.GetChild(0).gameObject.GetComponent<Enemy>().health + modifyAddHealth;
                try {
                    g.transform.GetChild(0).gameObject.GetComponent<Enemy_skills>().additionalDmg = g.transform.GetChild(0).gameObject.GetComponent<Enemy_skills>().additionalDmg + modifyAddDmg;
                }
                catch {

                }

                g.transform.GetChild(0).gameObject.GetComponent<Enemy>().doNotUseForceToMove = modifyDoNotUseForce;
            }

            g.layer = (int)Layer_enum.enemy;
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
    /// timer um den Spawner an sich zu starten
    /// </summary>
    /// <param name="wait">zeit um den Spawner zu starten in sekunden</param>
    /// <returns></returns>
    private IEnumerator spawnerActivationTimer(float wait) {

        if (wait != 0) {
            yield return new WaitForSeconds(wait);
        }
        activateSpawning();

    }




    /// <summary>
    /// check Spawntimer die auf trigger areas reagiert
    /// </summary>
    /// <param name="trigger"> trigger area die ausgelöst worden ist </param>
    /// <returns> true, wenn der spawner active ist und der check entgegengenommen wurde</returns>
    public bool checkSpawnTrigger(GameObject trigger) {
        if (isActive == false) {

            return false;
        }

        foreach (Enemy_Spawner_Info e in enemysToSpawn) {
            if (e.useTriggerArea == true && trigger == e.triggerArea && e.SpawnStartet == false && e.SpawnConditonFulfilled == false) {
                StartCoroutine(startSpawntimer(e.delay, e));
                e.SpawnStartet = true;
            }

        }
        return true;
    }

    /// <summary>
    /// checkt ob der Spawner activiert worden ist und activiert ihn
    /// </summary>
    /// <param name="trigger"> trigger area die ausgelöst worden ist</param>
    /// <returns> true, wenn der spawner activiert worden ist</returns>
    public bool checkSpawnerActivationTrigger(GameObject trigger) {
        if (isActive == false) {
            if (useTriggerArea == true && triggerArea == trigger) {
                StartCoroutine(spawnerActivationTimer(delay));
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// veringert den spawn count nachdem ein enemy zerstört worden ist
    /// </summary>
    public void spawnKilled() {
        currentSpawnCount = currentSpawnCount - 1;

        if (waveSpawner == true) {
            gameObject.SetActive(false);
        }
    }
}
