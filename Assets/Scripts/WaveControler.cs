using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControler : MonoBehaviour
{

    public int minEnemys;
    public int maxEnemys;

    private int currentWave;

    private float curentHealthUpgrade;


    private Player player;


    public int CurrentWave {
        get {
            return currentWave;
        }

        set {
            currentWave = value;
        }
    }



    // Start is called before the first frame update
    void Start() {
        player = Globals.player.GetComponent<Player>();
        Globals.waveControler = this;
        curentHealthUpgrade = 0;

        StartCoroutine(delayStart(0.5f));
    }



    private IEnumerator delayStart(float delay) {


        yield return new WaitForSeconds(delay);

        startNextWave();
    }


    public void startNextWave() {
        currentWave = currentWave + 1;






        if (currentWave % 5 == 0) {
            player.additionalDmg = player.additionalDmg + 0.25f;

        }
        if (currentWave % 10 == 0) {
            player.dmgModifier = player.dmgModifier + 1.1f;

            curentHealthUpgrade = curentHealthUpgrade * 2.8f;
        }

        Globals.currentWinCondition.enemysToKill = 0;


        player.CurrentHealth = player.maxBaseHealth;


        int enemysToSpawn = Random.Range(minEnemys, maxEnemys);

        List<Enemy_Spawner> spawner = new List<Enemy_Spawner>(Globals.infityWaveSpawner);

        while (enemysToSpawn != 0) {

            if (spawner.Count == 0) {
                break;
            }

            int i = Random.Range(0, spawner.Count);

            if (spawner[i].gameObject.activeSelf == true) {
                spawner.RemoveAt(i);
                continue;
            }

            spawner[i].modifyAddHealth = curentHealthUpgrade;

            spawner[i].gameObject.SetActive(true);

            spawner.RemoveAt(i);
            enemysToSpawn = enemysToSpawn - 1;

            Globals.currentWinCondition.enemysToKill = Globals.currentWinCondition.enemysToKill + 1;
            Globals.menuHandler.onChangedScore();
        }
    }


    public void waveFinished() {

        curentHealthUpgrade = curentHealthUpgrade + 0.3f;
        minEnemys = minEnemys + 1;
        maxEnemys = maxEnemys + 1;

        startNextWave();
    }

    private void OnDestroy() {
        Globals.waveControler = null;
    }
}
