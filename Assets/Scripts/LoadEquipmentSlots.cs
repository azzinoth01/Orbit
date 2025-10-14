using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class to load the sprites of the current equipment of the ship
/// </summary>
public class LoadEquipmentSlots : MonoBehaviour
{
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

    /// <summary>
    /// loads the current ship equipment and sets the sprites on the right slot
    /// </summary>
    void Start() {

        PlayerSave save = PlayerSave.loadSettings();
        if(save == null) {
            save = new PlayerSave();
        }

        if(save.MainWeapon != null) {
            mainWeapon.sprite = LoadAssets.Instance.loadSprite(save.MainWeapon.Icon);
            mainWeapon.enabled = true;
        }

        if(save.SecondaryWeapon != null) {
            secondaryWeapon.sprite = LoadAssets.Instance.loadSprite(save.SecondaryWeapon.Icon);
            secondaryWeapon.enabled = true;
        }

        if(save.SecondaryWeapon1 != null) {
            secondaryWeapon1.sprite = LoadAssets.Instance.loadSprite(save.SecondaryWeapon1.Icon);
            secondaryWeapon1.enabled = true;
        }

        if(save.ShieldPart != null) {
            shieldPart.sprite = LoadAssets.Instance.loadSprite(save.ShieldPart.Icon);
            shieldPart.enabled = true;
        }

    }
}
