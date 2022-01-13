using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[Serializable]
public class PlayerSave
{
    [SerializeField] private int money;
    [SerializeField] private WeaponInfo mainWeapon;
    [SerializeField] private WeaponInfo secondaryWeapon;
    [SerializeField] private WeaponInfo secondaryWeapon1;
    [SerializeField] private Parts shieldPart;
    [SerializeField] private List<string> boughtItems;

    public int Money {
        get {
            return money;
        }

        set {
            money = value;
        }
    }

    public WeaponInfo MainWeapon {
        get {
            return mainWeapon;
        }

        set {
            mainWeapon = value;
        }
    }

    public WeaponInfo SecondaryWeapon {
        get {
            return secondaryWeapon;
        }

        set {
            secondaryWeapon = value;
        }
    }

    public WeaponInfo SecondaryWeapon1 {
        get {
            return secondaryWeapon1;
        }

        set {
            secondaryWeapon1 = value;
        }
    }

    public Parts ShieldPart {
        get {
            return shieldPart;
        }

        set {
            shieldPart = value;
        }
    }

    public List<string> BoughtItems {
        get {
            return boughtItems;
        }

        set {
            boughtItems = value;
        }
    }

    public PlayerSave() {
        money = 0;
        mainWeapon = null;
        secondaryWeapon = null;
        secondaryWeapon1 = null;
        shieldPart = null;
        boughtItems = new List<string>();
    }


    /// <summary>
    /// saves Player information
    /// </summary>
    public void savingSetting() {

        //  string json = JsonUtility.ToJson(this);
        using (FileStream file = File.Create(Application.persistentDataPath + "/savePlayer.sav")) {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, this);
        }

    }

    /// <summary>
    /// loads the player information from the saved data, if exists
    /// </summary>
    /// <returns> returns the saved player information </returns>
    public static PlayerSave loadSettings() {

        PlayerSave s = new PlayerSave();


        if (System.IO.File.Exists(Application.persistentDataPath + "/savePlayer.sav")) {

            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream file = File.Open(Application.persistentDataPath + "/savePlayer.sav", FileMode.Open)) {
                s = (PlayerSave)bf.Deserialize(file);
            }


            //string json = File.ReadAllText(Application.persistentDataPath + "/savePlayer.sav");

            //if (json == null || json == "") {
            //    return null;
            //}


            //s = JsonUtility.FromJson<PlayerSave>(json);

            if (s == null) {
                return null;
            }
            return s;

        }
        return null;

    }
}
