using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    public Image[] images;
    public float yPosBorder;
    public float speed;
    // Start is called before the first frame update

    // Update is called once per frame
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


