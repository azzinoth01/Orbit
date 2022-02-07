using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;


/// <summary>
/// class to show tooltips
/// </summary>
[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{
    public Text headerField;

    public Text contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    InputSystemUIInputModule inputModule;

    public bool tooltipToogled = false;



    /// <summary>
    /// sets the tootip tiext
    /// </summary>
    /// <param name="content"> text to display in the content text field</param>
    /// <param name="header"> text to display in the header text field</param>
    public void SetText(string content, string header = "") {
        if (tooltipToogled == true) {

            if (header == "" && content == "") {
                gameObject.SetActive(false);
            }

            if (string.IsNullOrEmpty(header)) {
                headerField.gameObject.SetActive(false);
            }
            else {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }

            if (contentField != null) {
                contentField.text = content;
            }


            int headerLength = headerField.text.Length;
            int contentLength = 0;
            if (contentField != null) {
                contentLength = contentField.text.Length;
            }


            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
        }
    }
}
