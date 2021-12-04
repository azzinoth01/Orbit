using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public Enemy_Spawner_Info() {
        spawnStartet = false;
        currentEnemysSpawned = 0;
    }

    public bool SpawnStartet {
        get {
            return spawnStartet;
        }

        set {
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
}
