using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadEquipmentSlots : MonoBehaviour
{
    public Image mainWeapon;
    public Image secondaryWeapon;
    public Image secondaryWeapon1;
    public Image shieldPart;
    private LoadAssets loader;

    // Start is called before the first frame update
    void Start() {
        loader = new LoadAssets();
        PlayerSave save = PlayerSave.loadSettings();
        if (save == null) {
            save = new PlayerSave();
        }

        if (save.MainWeapon != null) {
            mainWeapon.sprite = loader.loadSprite(save.MainWeapon.Icon);
            mainWeapon.enabled = true;
        }

        if (save.SecondaryWeapon != null) {
            secondaryWeapon.sprite = loader.loadSprite(save.SecondaryWeapon.Icon);
            secondaryWeapon.enabled = true;
        }

        if (save.SecondaryWeapon1 != null) {
            secondaryWeapon1.sprite = loader.loadSprite(save.SecondaryWeapon1.Icon);
            secondaryWeapon1.enabled = true;
        }

        if (save.ShieldPart != null) {
            shieldPart.sprite = loader.loadSprite(save.ShieldPart.Icon);
            shieldPart.enabled = true;
        }

    }

    private void OnDestroy() {
        loader.releaseAllHandle();
    }
}
