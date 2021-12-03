using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCallBack : MonoBehaviour
{

    public bool spawnTrigger;



    private void OnTriggerEnter2D(Collider2D collision) {
        if (Globals.player == collision.gameObject) {
            if (spawnTrigger == true) {

                foreach (Enemy_Spawner e in Globals.spawnerListe) {
                    e.checkSpawnTrigger(gameObject);
                }
            }
        }
    }
}
