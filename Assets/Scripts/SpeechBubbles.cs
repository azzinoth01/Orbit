using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbles : MonoBehaviour
{
    public GameObject UiObject;
    public GameObject Trigger;
    // Start is called before the first frame update
    void Start()
    {
        UiObject.SetActive(false);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { UiObject.SetActive(true);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        UiObject.SetActive(false);
        Destroy(Trigger);
    }
}
