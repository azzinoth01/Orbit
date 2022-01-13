using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ItemCatalog
{
    [SerializeReference] private List<Item> itemList;

    public List<Item> ItemList {
        get {
            return itemList;
        }

        set {
            itemList = value;
        }
    }

    public ItemCatalog() {
        itemList = new List<Item>();
    }


    /// <summary>
    /// saves the itemCatalog
    /// </summary>
    public void savingSetting() {
        Debug.Log("save");
        string json = JsonUtility.ToJson(this);
        using (FileStream file = File.Create("Assets/Catalog/itemCatalog.json")) {
            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(json);
                Debug.Log("saveing");
            }
        }

    }

    /// <summary>
    /// loads the data from the itemCatalog
    /// </summary>
    /// <returns> gibt die gespeicherten Settings zurück</returns>
    public static ItemCatalog loadSettings() {

        ItemCatalog s = new ItemCatalog();
        //Debug.Log("still loading");
        LoadAssets load = new LoadAssets();
        TextAsset text = load.loadText("Assets/Catalog/itemCatalog.json");
        //Debug.Log("still loading");

        if (text != null) {
            //string json = File.ReadAllText("Assets/Catalog/itemCatalog.json");

            string json = text.text;

            //Debug.Log(json);
            if (json == null || json == "") {
                return null;
            }

            s = JsonUtility.FromJson<ItemCatalog>(json);
            return s;
        }


        return null;

    }
}
