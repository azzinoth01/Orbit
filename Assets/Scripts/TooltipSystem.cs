using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    public ToolTip tooltip;

    public void Start() {
        Globals.tooltip = tooltip;
        SaveSettings s = SaveSettings.loadSettings();

        tooltip.tooltipToogled = s.IsToogleOn;

        Debug.Log("tooltip is " + tooltip.tooltipToogled.ToString());
    }


    public static void Show(string content, string header = "") {
        if (Globals.tooltip != null) {
            if (Globals.tooltip.tooltipToogled == true) {
                Globals.tooltip.SetText(content, header);
                Globals.tooltip.gameObject.SetActive(true);
            }
            else {
                Hide();
            }
        }


    }

    public static void Hide() {
        if (Globals.tooltip != null) {
            Globals.tooltip.gameObject.SetActive(false);
        }
    }

}
