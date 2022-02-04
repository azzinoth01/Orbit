using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// container classe for spawner variablen
/// </summary>
[Serializable]
public class Enemy_Spawner_Info
{

    public GameObject triggerArea;
    public bool useTriggerArea;
    public float delay;
    public int enemysToSpawn;
    public GameObject enemyPrefab;
    private bool spawnStartet;
    private int currentEnemysSpawned;
    private bool spawnConditonFulfilled;


    /// <summary>
    /// base constructor sets standard values
    /// </summary>
    public Enemy_Spawner_Info() {
        spawnStartet = false;
        currentEnemysSpawned = 0;
        spawnConditonFulfilled = false;
    }

    public bool SpawnStartet {
        get {
            return spawnStartet;
        }

        set {
            if (value == true) {
                spawnConditonFulfilled = true;
            }
            spawnStartet = value;
        }
    }

    public int CurrentEnemysSpawned {
        get {
            return currentEnemysSpawned;
        }

        set {
            currentEnemysSpawned = value;
        }
    }

    public bool SpawnConditonFulfilled {
        get {
            return spawnConditonFulfilled;
        }
        set {
            spawnConditonFulfilled = value;
        }
    }
}
