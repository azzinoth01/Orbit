using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMoneyIcon : MonoBehaviour
{
    private Image icon;
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start() {
        try {
            icon = gameObject.GetComponent<Image>();
        }
        catch {

        }
        try {
            render = gameObject.GetComponent<SpriteRenderer>();
        }
        catch {

        }
        if (icon != null) {
            icon.sprite = Globals.moneyIcon;
        }
        if (render != null) {
            render.sprite = Globals.moneyIcon;
        }
    }

}
