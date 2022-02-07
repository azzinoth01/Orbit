using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// class to control a scrolling background
/// </summary>
public class ScrollBackground : MonoBehaviour
{
    public Image[] images;
    public float yPosBorder;
    public float speed;

    public float maxXOffset;


    /// <summary>
    /// sets the scrolling background to a random position
    /// </summary>
    private void Start() {
        float startPosY = Random.Range(0f, yPosBorder);

        float startPosX = Random.Range(-maxXOffset, maxXOffset);

        foreach (Image i in images) {
            i.transform.position = new Vector3(i.transform.position.x + startPosX, i.transform.position.y + startPosY, transform.position.z);
        }

        // Debug.Log(startPosX);

    }

    /// <summary>
    /// continuously scrolls the background down
    /// </summary>
    void Update() {
        foreach (Image i in images) {
            i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y - speed, transform.position.z);
        }

        Image img = images[2];


        if (img.transform.localPosition.y < yPosBorder) {
            images[2] = images[1];
            images[1] = images[0];
            images[0] = img;
            RectTransform trans = img.gameObject.GetComponent<RectTransform>();
            img.transform.position = new Vector3(img.transform.position.x, img.transform.position.y + (trans.rect.height * 3));
        }
    }
}


