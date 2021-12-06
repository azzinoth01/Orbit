using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private void Awake() {
        int i = 0;

        foreach (BulletInfo b in bulletInfoList) {
            GameObject g = new GameObject("bulletInfo" + i.ToString());

            g.transform.SetParent(transform, false);
            g.transform.localEulerAngles = new Vector3(0, 0, b.StartRotation);

            GameObject bullet = Instantiate(b.Bullet, g.transform);

            b.BulletScript = bullet.GetComponent<Bullet>();
            i = i + 1;


        }
    }

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

    private IEnumerator startDurationTimer(float wait) {
        yield return new WaitForSeconds(wait);
        isRunning = false;
        gameObject.SetActive(false);

    }

    public void layerChange() {
        foreach (BulletInfo b in bulletInfoList) {
            b.setLayer(gameObject.layer);
        }
    }

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

    private void OnEnable() {
        foreach (Transform t in transform) {
            t.gameObject.SetActive(true);
        }
        layerChange();
        time = 0;
        isRunning = false;

    }

    public void setDmgModifiers(int additionalDmg, float dmgModifier) {
        foreach (BulletInfo b in bulletInfoList) {
            b.AddBaseDmg = additionalDmg;
            b.DmgModifier = dmgModifier;
        }
    }

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
