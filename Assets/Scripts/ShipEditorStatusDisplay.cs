using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEditorStatusDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    public Text dmgText;
    public Text reloadTimeText;
    public Image patternIcon;
    public Text itemName;

    public Text ownedMoney;

    private Item currentItem;
    private LoadAssets loader;
    void Start() {
        MoneyChanged();
        currentItem = Globals.currentItem;
        loader = new LoadAssets();
    }

    // Update is called once per frame
    void Update() {
        if (currentItem != Globals.currentItem && Globals.currentItem != null) {
            currentItem = Globals.currentItem;
            itemName.text = currentItem.Name;
            if (currentItem is WeaponInfo) {
                WeaponInfo wep = (WeaponInfo)currentItem;
                dmgText.text = ((wep.additionalDmg + 1) * wep.dmgModifier).ToString();
                reloadTimeText.text = wep.reloadTime.ToString();
                patternIcon.sprite = loader.loadSprite(wep.PatternIcon);
                patternIcon.enabled = true;
            }
            else {
                patternIcon.enabled = false;
                dmgText.text = "";
                reloadTimeText.text = "";

            }
        }
    }

    public void MoneyChanged() {
        ownedMoney.text = Globals.money.ToString();
    }
}
