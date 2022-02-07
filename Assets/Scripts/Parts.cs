using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class to describe ship parts sub class of inventory objects
/// </summary>
[Serializable]
public class Parts : Inventar_Object
{
    [SerializeField] private int healthBoost;
    [SerializeField] private float shieldRefreshValueBoost;

    public int HealthBoost {
        get {
            return healthBoost;
        }

        set {
            healthBoost = value;
        }
    }

    public float ShieldRefreshValueBoost {
        get {
            return shieldRefreshValueBoost;
        }

        set {
            shieldRefreshValueBoost = value;
        }
    }
}
