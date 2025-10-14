using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class to control the display of items
/// </summary>
public class ShipEditorStatusDisplay : MonoBehaviour
{

    /// <summary>
    /// total dmg display
    /// </summary>
    public Text dmgText;
    /// <summary>
    /// total dmg display field name
    /// </summary>
    public Text dmgNameText;
    /// <summary>
    /// reload time display
    /// </summary>
    public Text reloadTimeText;
    /// <summary>
    /// reload time display field name
    /// </summary>
    public Text reloadTimeNameText;
    /// <summary>
    /// pattern icon display
    /// </summary>
    public Image patternIcon;
    /// <summary>
    /// pattern icon display field name
    /// </summary>
    public Text parrernIconName;
    /// <summary>
    /// item name display
    /// </summary>
    public Text itemName;

    /// <summary>
    /// owned money display
    /// </summary>
    public Text ownedMoney;

    private Item currentItem;

    private bool itemChanged;


    /// <summary>
    /// sets base values
    /// </summary>
    void Start() {
        MoneyChanged();
        currentItem = Globals.currentItem;
        itemName.text = "";

        itemChanged = false;
    }


    /// <summary>
    /// checks if the selected item has changed and displays the new values
    /// </summary>
    void Update() {
        if(itemChanged == true) {
            itemChanged = false;

            itemName.text = currentItem.Name;
            if(currentItem is WeaponInfo) {
                WeaponInfo wep = (WeaponInfo) currentItem;

                GameObject g = LoadAssets.Instance.loadGameObject(wep.skill);
                Skill skill = g.GetComponent<Skill>();

                float totalDmg = 0;
                foreach(BulletInfo b in skill.bulletInfoList) {
                    totalDmg = totalDmg + ((wep.additionalDmg + b.BulletBaseDmg) * wep.dmgModifier);
                }
                int totalDmgRound = (int) totalDmg;
                dmgText.text = totalDmgRound.ToString();
                reloadTimeText.text = wep.reloadTime.ToString() + "s";
                patternIcon.sprite = LoadAssets.Instance.loadSprite(wep.PatternIcon);
                patternIcon.enabled = true;

                dmgNameText.text = "TDMG";
                reloadTimeNameText.text = "Reload Time";
                parrernIconName.text = "Pattern";


            }
            else {
                Parts part = (Parts) currentItem;
                patternIcon.enabled = false;

                reloadTimeNameText.text = "Shield Rate";
                reloadTimeText.text = "+" + part.ShieldRefreshValueBoost.ToString();


                dmgNameText.text = "Health";
                dmgText.text = "+" + part.HealthBoost.ToString();
                parrernIconName.text = "";



            }
        }
    }

    /// <summary>
    /// displays new money value
    /// </summary>
    public void MoneyChanged() {
        ownedMoney.text = Globals.money.ToString();
    }

    /// <summary>
    /// changes the item to display
    /// </summary>
    /// <param name="item"> new item to display</param>
    public void changeInfoDispaly(Item item) {

        if(item != null) {
            currentItem = item;
            itemChanged = true;
        }

    }
}
