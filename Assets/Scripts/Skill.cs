using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour
{
    public List<BulletInfo> bulletInfoList;
    public int maxDuration;
    private float time;
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
        if (maxDuration <= time) {
            time = 0;
            gameObject.SetActive(false);

        }

        time = time + Time.deltaTime;
    }

    public void layerChange() {
        foreach (BulletInfo b in bulletInfoList) {
            b.setLayer(gameObject.layer);
        }
    }

    private void OnDisable() {
        Globals.bulletPool.Add(gameObject);
    }

    private void OnEnable() {
        foreach (Transform t in transform) {
            t.gameObject.SetActive(true);
        }
        layerChange();

    }

}
