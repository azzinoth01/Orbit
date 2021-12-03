using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_condition : MonoBehaviour
{

    public int enemysToKill;
    public BoxCollider2D boxcollider;
    public SpriteRenderer sp;

    // Start is called before the first frame update
    void Start() {
        Globals.currentWinCondition = this;
    }

    public void activateLevelFinishPortal() {
        boxcollider.enabled = true;
        sp.enabled = true;
    }


    public void enemyKilled() {
        enemysToKill = enemysToKill - 1;

        if (enemysToKill <= 0) {
            //Globals.menuHandler.levelFinishedUI.SetActive(true);
            activateLevelFinishPortal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (Globals.player == collision.gameObject) {
            Globals.menuHandler.setLevelFinish();
            // Destroy(gameObject);
            // Globals.player.GetComponent<Player>().disableControlls = true;
        }
    }

}
