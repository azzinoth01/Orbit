using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// sub class of item to manage iventory objects
/// </summary>
[Serializable]
public class Inventar_Object : Item
{

    [SerializeField] private int amount;

    public int Amount {
        get {
            return amount;
        }

        set {
            amount = value;
        }
    }
}
