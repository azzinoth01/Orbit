using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEditorStatusDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    public Text dmgText;
    public Text dmgNameText;
    public Text reloadTimeText;
    public Text reloadTimeNameText;
    public Image patternIcon;
    public Text parrernIconName;
    public Text itemName;

    public Text ownedMoney;

    private Item currentItem;
    private LoadAssets loader;
    private bool itemChanged;
    void Start() {
        MoneyChanged();
        currentItem = Globals.currentItem;
        itemName.text = "";
        loader = new LoadAssets();
        itemChanged = false;
    }

    // Update is called once per frame
    void Update() {
        if (itemChanged == true) {
            itemChanged = false;

            itemName.text = currentItem.Name;
            if (currentItem is WeaponInfo) {
                WeaponInfo wep = (WeaponInfo)currentItem;

                GameObject g = loader.loadGameObject(wep.skill);
                Skill skill = g.GetComponent<Skill>();

                float totalDmg = 0;
                foreach (BulletInfo b in skill.bulletInfoList) {
                    totalDmg = totalDmg + ((wep.additionalDmg + b.BulletBaseDmg) * wep.dmgModifier);
                }
                int totalDmgRound = (int)totalDmg;
                dmgText.text = totalDmgRound.ToString();
                reloadTimeText.text = wep.reloadTime.ToString() + "s";
                patternIcon.sprite = loader.loadSprite(wep.PatternIcon);
                patternIcon.enabled = true;

                dmgNameText.text = "TDMG";
                reloadTimeNameText.text = "Reload Time";
                parrernIconName.text = "Pattern";


            }
            else {
                Parts part = (Parts)currentItem;
                patternIcon.enabled = false;

                reloadTimeNameText.text = "Shield Rate";
                reloadTimeText.text = "+" + part.ShieldRefreshValueBoost.ToString();


                dmgNameText.text = "Health";
                dmgText.text = "+" + part.HealthBoost.ToString();
                parrernIconName.text = "";



            }
        }
    }
    private void OnDestroy() {
        loader.releaseAllHandle();
    }

    public void MoneyChanged() {
        ownedMoney.text = Globals.money.ToString();
    }

    public void changeInfoDispaly(Item item) {

        if (item != null) {
            currentItem = item;
            itemChanged = true;
        }

    }
}
