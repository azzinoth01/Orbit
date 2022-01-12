using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Inventory_fill : MonoBehaviour
{
    public GameObject mainWeaponBox;
    public GameObject secondaryWeaponBox;
    public GameObject partsBox;
    public GameObject LockedBox;
    public GameObject inventory;
    private ItemCatalog catalog;

    // Start is called before the first frame update
    void Start() {
        catalog = ItemCatalog.loadSettings();

        foreach (Item i in catalog.ItemList) {
            if (i is WeaponInfo) {
                WeaponInfo wap = (WeaponInfo)i;

                if (wap.mainWeapon == true) {
                    GameObject g = Instantiate(mainWeaponBox, transform);
                    Image img = g.transform.GetChild(0).gameObject.GetComponent<Image>();
                    //img.sprite = (Sprite)Resources.Load(wap.Icon);


                }
                else {
                    GameObject g = Instantiate(secondaryWeaponBox, transform);
                }
            }
            else {
                Parts part = (Parts)i;
                GameObject g = Instantiate(partsBox, transform);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
