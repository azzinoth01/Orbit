using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{

    private RectTransform backgroundRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("tooltipBackground").GetComponent<RectTransform>();
        textMeshPro = transform.Find("tooltipText").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        SetText("Hello World");
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(4, 4);

        backgroundRectTransform.sizeDelta = textSize + paddingSize;

    }

    /* private void Update()
     {
         rectTransform.anchoredPosition = Input.get_mousePosition();
    } */
}
