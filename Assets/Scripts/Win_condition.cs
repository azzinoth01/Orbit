using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// win condition handler classe
/// </summary>
public class Win_condition : MonoBehaviour
{

    public int enemysToKill;
    public BoxCollider2D boxcollider;
    public SpriteRenderer sp;

    /// <summary>
    /// setzt die wincondition in den Globalen variablen
    /// </summary>
    void Start() {
        Globals.currentWinCondition = this;
    }

    /// <summary>
    /// activiert das level end portal
    /// </summary>
    public void activateLevelFinishPortal() {
        boxcollider.enabled = true;
        sp.enabled = true;
    }

    /// <summary>
    /// erhöht der enemy kill counter um 1
    /// </summary>
    public void enemyKilled() {
        enemysToKill = enemysToKill - 1;

        if (enemysToKill <= 0) {
            //Globals.menuHandler.levelFinishedUI.SetActive(true);
            activateLevelFinishPortal();
        }
    }

    /// <summary>
    /// activiert das level finish UI
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {

        if (Globals.player == collision.gameObject) {
            Globals.menuHandler.setLevelFinish();
            // Destroy(gameObject);
            // Globals.player.GetComponent<Player>().disableControlls = true;
        }
    }

}
