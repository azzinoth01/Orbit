using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// win condition handler classe
/// </summary>
public class Win_condition : MonoBehaviour
{

    public int enemysToKill;
    public BoxCollider2D boxcollider;
    public SpriteRenderer sp;
    public Light2D lighting;

    public bool rotate;
    public float rotateSpeed;

    public AudioSource audios;

    public float spawnDelay;

    private bool alreadyActive;
    private bool alreadyTriggerd;

    /// <summary>
    /// setzt die wincondition in den Globalen variablen
    /// </summary>
    void Start() {
        Globals.currentWinCondition = this;
        alreadyActive = false;
        alreadyTriggerd = false;
    }

    /// <summary>
    /// activiert das level end portal
    /// </summary>
    public void activateLevelFinishPortal() {
        alreadyActive = true;
        boxcollider.enabled = true;
        sp.enabled = true;
        lighting.enabled = true;

        if (rotate == true) {
            StartCoroutine(startRotating());
        }

        if (audios != null) {
            audios.Play();
        }

    }
    private IEnumerator delayPortalSpawning() {



        yield return new WaitForSeconds(spawnDelay);
        activateLevelFinishPortal();
    }



    /// <summary>
    /// erhöht der enemy kill counter um 1
    /// </summary>
    public void enemyKilled() {
        enemysToKill = enemysToKill - 1;

        if (enemysToKill <= 0 && alreadyActive == false) {
            //Globals.menuHandler.levelFinishedUI.SetActive(true);

            //moved to spawn delay
            //activateLevelFinishPortal();

            StartCoroutine(delayPortalSpawning());
        }
    }

    private IEnumerator startRotating() {

        while (true) {
            if (Globals.pause == true) {
                yield return null;
            }
            else {
                Vector3 nextAngle = transform.rotation.eulerAngles + new Vector3(0, 0, 30);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(nextAngle), rotateSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    /// <summary>
    /// activiert das level finish UI
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {

        if (Globals.player == collision.gameObject) {
            if (alreadyTriggerd == false) {
                Globals.menuHandler.Playtime = Time.time - Globals.player.GetComponent<Player>().Timestamp;
                Globals.menuHandler.setLevelFinish();
                alreadyTriggerd = true;
            }

            // Destroy(gameObject);
            // Globals.player.GetComponent<Player>().disableControlls = true;
        }
    }

}
