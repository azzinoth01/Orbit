using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class to store the weapon creation info
/// </summary>
[Serializable]
public class WeaponInfo : Inventar_Object
{
    [SerializeField] public bool mainWeapon;
    [SerializeField] public string skill;
    [SerializeField] public float reloadTime;

    [SerializeField] public int shootsToCreate;

    [SerializeField] public int additionalDmg;
    [SerializeField] public float dmgModifier;
}
