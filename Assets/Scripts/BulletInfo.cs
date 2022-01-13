using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// container classe die Bullets beschreibt
/// </summary>
[Serializable]
public class BulletInfo
{
    [SerializeField] private float startRotation;
    [SerializeField] private int bulletBaseDmg;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject startEffect;
    [SerializeField] private GameObject sfxSound;

    private GameObject instantStartEffect;

    private int addBaseDmg;
    private float dmgModifier;

    private Bullet bulletScript;
    // Start is called before the first frame update

    /// <summary>
    /// standardconstruktor setzt basiswerte
    /// </summary>
    public BulletInfo() {
        //  Debug.Log("construktor called1");
        //bulletScript = bullet.GetComponent<Bullet>();
        addBaseDmg = 0;
        dmgModifier = 1;


    }
    /// <summary>
    /// construktor um werte zu setzten
    /// </summary>
    /// <param name="startRotation"> bestimmt die flugrichtung vom basisobject</param>
    /// <param name="bulletBaseDmg"> bestimmt bullet base dmg</param>
    /// <param name="bullet"> bestimmt bullet prefab</param>
    /// <param name="startEffect"> bestimmt particel system start prefab</param>
    /// <param name="sfxSound"> bestimmt Sound Effect prefab</param>
    public BulletInfo(float startRotation, int bulletBaseDmg, GameObject bullet, GameObject startEffect, GameObject sfxSound) {
        Debug.Log("construktor called");
        this.startRotation = startRotation;
        this.bulletBaseDmg = bulletBaseDmg;
        this.bullet = bullet;
        this.startEffect = startEffect;
        this.sfxSound = sfxSound;
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

    /// <summary>
    /// resetet die Dmg modifier
    /// </summary>
    public void resetModifiers() {
        addBaseDmg = 0;
        dmgModifier = 1;
        setBulletDmg();
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
            setBulletDmg();

        }
    }

    public int AddBaseDmg {


        set {
            addBaseDmg = value;
            setBulletDmg();
        }
    }

    public float DmgModifier {


        set {
            dmgModifier = value;
            setBulletDmg();
        }
    }

    public GameObject StartEffect {
        get {
            return startEffect;
        }

        set {
            startEffect = value;
        }
    }

    public GameObject InstantStartEffect {
        get {
            return instantStartEffect;
        }

        set {
            instantStartEffect = value;
        }
    }

    public GameObject SfxSound {
        get {
            return sfxSound;
        }

        set {
            sfxSound = value;
        }
    }

    /// <summary>
    /// sets den bullet dmg auf der Bullet
    /// </summary>
    public void setBulletDmg() {
        if (bulletScript != null) {
            bulletScript.BulletDmg = (bulletBaseDmg + addBaseDmg) * dmgModifier;
        }

    }

    /// <summary>
    /// sets den layer der Bullet
    /// </summary>
    /// <param name="layer"> layer in integer</param>
    public void setLayer(int layer) {
        bulletScript.gameObject.layer = layer;
    }


    /// <summary>
    /// enables the bullet particle effect
    /// </summary>
    public void enableEffects() {
        if (instantStartEffect != null) {
            instantStartEffect.SetActive(true);
        }

    }

    //private void OnEnable() {
    //    bullet.GetComponent<Bullet>().BulletDmg = bulletBaseDmg; // + modifier 
    //}
}
