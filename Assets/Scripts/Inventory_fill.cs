using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory_fill : MonoBehaviour
{
    public GameObject mainWeaponBox;
    public GameObject secondaryWeaponBox;
    public GameObject partsBox;
    public GameObject LockedBox;
    public Image dragAndDrop;

    private ItemCatalog catalog;
    private LoadAssets assetLoader;

    // Start is called before the first frame update
    void Start() {
        catalog = ItemCatalog.loadSettings();

        assetLoader = new LoadAssets();

        foreach (Item i in catalog.ItemList) {
            if (i is WeaponInfo) {
                WeaponInfo wap = (WeaponInfo)i;

                if (wap.mainWeapon == true) {
                    GameObject g = Instantiate(mainWeaponBox, transform);
                    Image img = g.transform.GetChild(0).gameObject.GetComponent<Image>();
                    img.sprite = assetLoader.loadSprite(wap.Icon);

                    Button b = g.GetComponent<Button>();

                    b.onClick.AddListener(delegate {
                        inventoryButton(wap);
                    });


                }
                else {
                    GameObject g = Instantiate(secondaryWeaponBox, transform);
                    Image img = g.transform.GetChild(0).gameObject.GetComponent<Image>();
                    img.sprite = assetLoader.loadSprite(wap.Icon);

                    Button b = g.GetComponent<Button>();
                    b.onClick.AddListener(delegate {
                        inventoryButton(wap);
                    });
                }
            }
            else {
                Parts part = (Parts)i;
                GameObject g = Instantiate(partsBox, transform);

                Image img = g.transform.GetChild(0).gameObject.GetComponent<Image>();
                img.sprite = assetLoader.loadSprite(part.Icon);

                Button b = g.GetComponent<Button>();


                b.onClick.AddListener(delegate {
                    inventoryButton(part);
                });
            }
        }
    }

    private void inventoryButton(Item i) {
        dragAndDrop.enabled = true;

        dragAndDrop.sprite = assetLoader.loadSprite(i.Icon);
        Globals.currentItem = i;
    }

    public void mainWeaponSlotClicked(Image button) {
        if (Globals.currentItem is WeaponInfo) {
            WeaponInfo wep = (WeaponInfo)Globals.currentItem;
            if (wep.mainWeapon == true) {
                button.sprite = assetLoader.loadSprite(wep.Icon);
                button.enabled = true;
                Globals.currentItem = null;
                dragAndDrop.enabled = false;

                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.MainWeapon = wep;

                save.savingSetting();
            }
        }

    }

    public void secondaryWeaponSlotClicked(Image button) {
        if (Globals.currentItem is WeaponInfo) {
            WeaponInfo wep = (WeaponInfo)Globals.currentItem;
            if (wep.mainWeapon == false) {
                button.sprite = assetLoader.loadSprite(wep.Icon);
                button.enabled = true;
                Globals.currentItem = null;
                dragAndDrop.enabled = false;

                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.SecondaryWeapon = wep;

                save.savingSetting();
            }
        }
    }
    public void secondaryWeaponSlotTwoClicked(Image button) {
        if (Globals.currentItem is WeaponInfo) {
            WeaponInfo wep = (WeaponInfo)Globals.currentItem;
            if (wep.mainWeapon == false) {
                button.sprite = assetLoader.loadSprite(wep.Icon);
                button.enabled = true;
                Globals.currentItem = null;
                dragAndDrop.enabled = false;

                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.SecondaryWeapon1 = wep;

                save.savingSetting();
            }
        }
    }
    public void shieldSlotClicked(Image button) {
        if (Globals.currentItem is Parts) {
            Parts part = (Parts)Globals.currentItem;

            button.sprite = assetLoader.loadSprite(part.Icon);
            button.enabled = true;
            Globals.currentItem = null;
            dragAndDrop.enabled = false;

            PlayerSave save = PlayerSave.loadSettings();
            if (save == null) {
                save = new PlayerSave();
            }
            save.ShieldPart = part;

            save.savingSetting();

        }
    }


    // Update is called once per frame
    void Update() {
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            dragAndDrop.enabled = false;
            Globals.currentItem = null;
        }
    }
}
