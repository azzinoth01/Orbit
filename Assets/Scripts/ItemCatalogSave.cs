using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemCatalog", order = 1)]
[Serializable]
public class ItemCatalogSave : ScriptableObject {
    [SerializeField] private List<WeaponInfo> weaponInfo;
    [SerializeField] private List<Parts> parstInfos;

    public List<WeaponInfo> WeaponInfo {
        get {
            return weaponInfo;
        }

        set {
            weaponInfo = value;
        }
    }

    public List<Parts> ParstInfos {
        get {
            return parstInfos;
        }

        set {
            parstInfos = value;
        }
    }
}
