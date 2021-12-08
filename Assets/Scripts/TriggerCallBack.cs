using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe um Trigger areas zu machen
/// </summary>
public class TriggerCallBack : MonoBehaviour
{

    public bool spawnTrigger;


    /// <summary>
    /// trigger check
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (Globals.player == collision.gameObject) {
            if (spawnTrigger == true) {

                foreach (Enemy_Spawner e in Globals.spawnerListe) {
                    e.checkSpawnTrigger(gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
}
