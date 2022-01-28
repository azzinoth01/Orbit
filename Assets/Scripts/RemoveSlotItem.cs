using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RemoveSlotItem : MonoBehaviour
{
    private bool isMouseOver;
    private Image image;

    public bool isMainWeapon;
    public bool isSecondaryWeapon;
    public bool isSecondaryWeapon1;
    public bool isShipPart;

    public AudioSource audios;

    public Image Image {
        get {
            return image;
        }

        set {
            image = value;
        }
    }

    public bool IsMouseOver {
        get {
            return isMouseOver;
        }

        set {
            isMouseOver = value;
        }
    }



    // Start is called before the first frame update
    void Start() {
        isMouseOver = false;
        image = transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        if (Globals.virtualMouse.VirtualMouseProperty.rightButton.wasPressedThisFrame && isMouseOver == true && Globals.currentItem == null) {
            if (isMainWeapon == true) {
                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.MainWeapon = null;

                save.savingSetting();
                image.sprite = null;
                image.enabled = false;
            }
            if (isSecondaryWeapon == true) {
                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.SecondaryWeapon = null;

                save.savingSetting();
                image.sprite = null;
                image.enabled = false;
            }
            if (isSecondaryWeapon1 == true) {
                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.SecondaryWeapon1 = null;

                save.savingSetting();
                image.sprite = null;
                image.enabled = false;
            }
            if (isShipPart == true) {
                PlayerSave save = PlayerSave.loadSettings();
                if (save == null) {
                    save = new PlayerSave();
                }
                save.ShieldPart = null;

                save.savingSetting();
                image.sprite = null;
                image.enabled = false;
            }

            if (audios != null) {
                audios.Play();
            }
        }
    }


    public void setMouseOver(bool value) {
        isMouseOver = value;
    }


}
