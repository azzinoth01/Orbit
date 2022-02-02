using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public ToolTip tooltip;



    public void Awake() {
        current = this;
    }

    public static void Show(string content, string header = "") {
        if (current.tooltip.tooltipToogled == true) {
            current.tooltip.SetText(content, header);
            current.tooltip.gameObject.SetActive(true);
        }
        else {
            Hide();
        }

    }

    public static void Hide() {
        current.tooltip.gameObject.SetActive(false);
    }

}
