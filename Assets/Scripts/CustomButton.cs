using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public Image buttonimage;
    // Start is called before the first frame update
    void Start()
    {
        buttonimage.alphaHitTestMinimumThreshold = 0.2f;
    }

    
}
