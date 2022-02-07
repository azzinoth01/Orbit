using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// class to trigger tooltips
/// </summary>
public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;


    /// <summary>
    /// shows the tooltips if the cursor is over a button
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData) {
        TooltipSystem.Show(content, header);
    }

    /// <summary>
    /// hides the tooltip if the cursor leaves the button
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData) {
        TooltipSystem.Hide();

    }
}
