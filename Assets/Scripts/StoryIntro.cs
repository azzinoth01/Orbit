using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

public class StoryIntro : MonoBehaviour, Controlls.IBullet_hellActions
{

    private bool skip;
    public GameObject mainMenu;

    public List<GameObject> slides;

    private List<GameObject> privateSlides;

    private IDisposable buttonEvent;

    private Controlls controll;

    public void OnDoge(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    public void OnMove_down(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
    }

    public void OnMove_left(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
    }

    public void OnMove_up(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    public void OnPause_menu(InputAction.CallbackContext context) {
        if (context.started) {
            skip = true;
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

        if (controll == null) {
            controll = new Controlls();

            Rebinding_menu rebind = new Rebinding_menu();
            controll = rebind.loadRebinding(controll);

            controll.bullet_hell.Enable();
            controll.bullet_hell.SetCallbacks(this);



        }

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
        controll.Dispose();
        controll = null;
    }

}
