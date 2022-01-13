using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe um Trigger areas zu machen
/// </summary>
public class TriggerCallBack : MonoBehaviour
{

    public bool spawnTrigger;
    public bool spawnerActivationTrigger;
    public bool moneyDrop;

    private bool canDestory;
    private int moneyValue;

    public int MoneyValue {
        set {
            moneyValue = value;
        }
    }

    /// <summary>
    /// trigger check
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (Globals.player == collision.gameObject) {
            canDestory = true;
            if (spawnTrigger == true) {


                foreach (Enemy_Spawner e in Globals.spawnerListe) {

                    if (e.checkSpawnTrigger(gameObject) == false) {
                        canDestory = false;
                    }
                }
                if (canDestory == true) {
                    Destroy(gameObject);
                }

            }
            if (spawnerActivationTrigger == true) {
                foreach (Enemy_Spawner e in Globals.spawnerListe) {

                    if (e.checkSpawnerActivationTrigger(gameObject) == false) {
                        canDestory = false;
                    }

                    if (canDestory == true) {
                        Destroy(gameObject);
                    }
                }
            }
            if (moneyDrop == true) {
                Globals.money = Globals.money + moneyValue;
                //Debug.Log("Current Money: " + Globals.money.ToString());
                Destroy(gameObject);
            }

        }
    }
}
