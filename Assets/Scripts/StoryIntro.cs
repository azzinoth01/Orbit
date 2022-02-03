using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

public class StoryIntro : MonoBehaviour
{

    private bool skip;
    public GameObject mainMenu;

    public List<GameObject> slides;

    private List<GameObject> privateSlides;

    private IDisposable buttonEvent;



    public bool Skip {
        get {
            return skip;
        }

        set {
            skip = value;
        }
    }



    public void OnShoot(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }


    private void OnEnable() {
        skip = false;
        privateSlides = new List<GameObject>(slides);

        if (Globals.skipStartCutscene == true) {
            skip = true;
            if (skip == true) {
                foreach (GameObject g in slides) {
                    g.SetActive(false);

                }
                mainMenu.SetActive(true);
                return;
            }
        }


        buttonEvent = InputSystem.onAnyButtonPress.Call(nextSlide);



    }
    // Update is called once per frame
    void Update() {

        if (skip == true) {
            foreach (GameObject g in slides) {
                g.SetActive(false);

            }
            Globals.skipStartCutscene = true;
            mainMenu.SetActive(true);
            enabled = false;
        }


    }



    private void nextSlide(InputControl button) {
        if (privateSlides.Count != 0) {
            privateSlides[0].SetActive(false);
            privateSlides.RemoveAt(0);
            if (privateSlides.Count != 0) {
                privateSlides[0].SetActive(true);

            }
            else {
                mainMenu.SetActive(true);
                enabled = false;
            }
        }

    }
    private void OnDisable() {
        Globals.skipStartCutscene = true;

        if (buttonEvent != null) {
            buttonEvent.Dispose();
            buttonEvent = null;
        }

    }

}
