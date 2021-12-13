using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// skill class beschreibt wie sich jedes bulletobject auf der classe verhält
/// </summary>
public class Skill : MonoBehaviour
{
    public List<BulletInfo> bulletInfoList;
    public int maxDuration;
    private float time;
    private bool isRunning;
    private Coroutine timer;

    private float timestamp;

    public float Timestamp {
        get {
            return timestamp;
        }

        set {
            timestamp = value;
        }
    }

    /// <summary>
    /// erstelltung der Bulletobjecte aus der bulletInfoList
    /// </summary>
    private void Awake() {
        int i = 0;

        foreach (BulletInfo b in bulletInfoList) {
            GameObject g = new GameObject("bulletInfo" + i.ToString());

            g.transform.SetParent(transform, false);
            g.transform.localEulerAngles = new Vector3(0, 0, b.StartRotation);

            GameObject bullet = Instantiate(b.Bullet, g.transform);
            if (b.StartEffect != null) {
                GameObject effect = Instantiate(b.StartEffect, g.transform);


                b.InstantStartEffect = effect;
            }

            b.BulletScript = bullet.GetComponent<Bullet>();
            i = i + 1;


        }
    }

    /// <summary>
    /// startet den max Duration Timer
    /// </summary>
    private void Update() {

        if (Globals.pause == true) {
            return;
        }
        else {
            if (isRunning == false) {
                isRunning = true;
                timer = StartCoroutine(startDurationTimer(maxDuration));
            }
        }

    }

    /// <summary>
    /// max Duration Timer
    /// </summary>
    /// <param name="wait"> duration time in Seconds</param>
    /// <returns></returns>
    private IEnumerator startDurationTimer(float wait) {
        yield return new WaitForSeconds(wait);
        isRunning = false;
        gameObject.SetActive(false);

    }

    /// <summary>
    /// ändern des Layers der Bullets falls sich der Layer des skills ändern sollte
    /// </summary>
    public void layerChange() {
        foreach (BulletInfo b in bulletInfoList) {
            b.setLayer(gameObject.layer);
        }
    }


    /// <summary>
    /// gibt den skill dem bulletpool zurück 
    /// resetet alle modifier on den bullets 
    /// stoped den duration timer
    /// setzt einen Timestap für bulletpool clean up
    /// </summary>
    private void OnDisable() {

        Globals.bulletPool.Add(this);
        foreach (BulletInfo b in bulletInfoList) {
            b.resetModifiers();
        }
        if (timer != null) {
            StopCoroutine(timer);
        }
        timestamp = Time.time;
    }

    /// <summary>
    /// beim Destroyen aus dem bullet pool mit entfernen
    /// </summary>
    private void OnDestroy() {
        Globals.bulletPool.Remove(this);
    }


    /// <summary>
    /// setzt alle child objecte active
    /// setzt den layer neu
    /// </summary>
    private void OnEnable() {
        foreach (Transform t in transform) {
            t.gameObject.SetActive(true);
        }
        layerChange();
        time = 0;
        isRunning = false;
        effectEnable();
    }

    /// <summary>
    /// enables the particle Effect object
    /// </summary>
    private void effectEnable() {
        foreach (BulletInfo b in bulletInfoList) {
            b.enableEffects();
        }
    }


    /// <summary>
    /// setzt modifiers auf die bullet.
    /// </summary>
    /// <param name="additionalDmg"> erhöhten den schaden der bullet direkt über diesen Wert</param>
    /// <param name="dmgModifier"> nach hinzufügen des additionalDmg modifiers wir der dmg mit diesem Wert multipliziert</param>
    public void setDmgModifiers(int additionalDmg, float dmgModifier) {
        foreach (BulletInfo b in bulletInfoList) {
            b.AddBaseDmg = additionalDmg;
            b.DmgModifier = dmgModifier;
        }
    }


    /// <summary>
    /// checkt ob alle child objecte inactive sind, um den skill zu deactivieren
    /// wird aufgerufen, wenn eine bullet sich deactiviert
    /// </summary>
    public void checkDisabled() {
        int i = transform.childCount;
        int counter = 0;

        foreach (Transform t in transform) {
            if (t.gameObject.activeSelf == false) {
                counter = counter + 1;
            }
        }

        if (counter == i) {
            //gameObject.name = "test deactivation";
            gameObject.SetActive(false);
        }
    }

}
