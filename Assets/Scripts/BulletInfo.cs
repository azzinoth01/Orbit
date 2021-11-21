using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletInfo
{
    [SerializeField] private float startRotation;
    [SerializeField] private int bulletBaseDmg;
    [SerializeField] private GameObject bullet;

    private int addBaseDmg;
    private float dmgModifier;

    private Bullet bulletScript;
    // Start is called before the first frame update


    public BulletInfo() {
        //  Debug.Log("construktor called1");
        //bulletScript = bullet.GetComponent<Bullet>();
        addBaseDmg = 0;
        dmgModifier = 1;


    }
    public BulletInfo(float startRotation, int bulletBaseDmg, GameObject bullet) {
        Debug.Log("construktor called");
        this.startRotation = startRotation;
        this.bulletBaseDmg = bulletBaseDmg;
        this.bullet = bullet;
        //bulletScript = bullet.GetComponent<Bullet>();
        addBaseDmg = 0;
        dmgModifier = 1;

    }

    public float StartRotation {
        get {
            return startRotation;
        }

        set {
            startRotation = value;
        }
    }




    public GameObject Bullet {
        get {
            return bullet;
        }

        set {
            bullet = value;
        }
    }

    public Bullet BulletScript {


        set {
            bulletScript = value;
        }
    }

    public void setLayer(int layer) {
        bulletScript.gameObject.layer = layer;
    }


    //private void OnEnable() {
    //    bullet.GetComponent<Bullet>().BulletDmg = bulletBaseDmg; // + modifier 
    //}
}
