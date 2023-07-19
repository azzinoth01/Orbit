using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

/// <summary>
/// class to load the sprites of the current equipment of the ship
/// </summary>
public class LoadEquipmentSlots : MonoBehaviour {
    /// <summary>
    /// main Weapon image
    /// </summary>
    public Image mainWeapon;
    /// <summary>
    /// secondary weapon image
    /// </summary>
    public Image secondaryWeapon;
    /// <summary>
    /// second secondary weapon image
    /// </summary>
    public Image secondaryWeapon1;
    /// <summary>
    /// ship part image
    /// </summary>
    public Image shieldPart;
    //private LoadAssets loader;

    /// <summary>
    /// loads the current ship equipment and sets the sprites on the right slot
    /// </summary>
    void Start() {
        //loader = new LoadAssets();
        PlayerSave save = PlayerSave.loadSettings();
        if (save == null) {
            save = new PlayerSave();
        }

        if (save.MainWeapon != null) {
            mainWeapon.sprite = Resources.Load(save.MainWeapon.Icon) as Sprite;

            mainWeapon.enabled = true;
        }

        if (save.SecondaryWeapon != null) {
            secondaryWeapon.sprite = Resources.Load(save.SecondaryWeapon.Icon) as Sprite;

            secondaryWeapon.enabled = true;
        }

        if (save.SecondaryWeapon1 != null) {
            secondaryWeapon1.sprite = Resources.Load(save.SecondaryWeapon1.Icon) as Sprite;

            secondaryWeapon1.enabled = true;
        }

        if (save.ShieldPart != null) {
            shieldPart.sprite = Resources.Load(save.ShieldPart.Icon) as Sprite;


            shieldPart.enabled = true;
        }

    }

    /// <summary>
    /// releases all handels of the loaded sprites
    /// </summary>
    private void OnDestroy() {
        //  loader.releaseAllHandle();
    }
}
