using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;

public class StoryIntro : MonoBehaviour
{

    private bool skip;
    public GameObject mainMenu;

    public List<GameObject> slides;

    private List<GameObject> privateSlides;

    //private IDisposable buttonEvent;

    private List<ButtonControl> pressedButtons;

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
        InputSystem.onEvent += anyButtonWasPressed;
        InputSystem.onEvent += anyButtonWasReleased;
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
        pressedButtons = new List<ButtonControl>();

        // buttonEvent = InputSystem.onAnyButtonPress.Call(nextSlide);


        //InputSystem.onEvent += (eventPtr, device) => {
        //    if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) {
        //        return;
        //    }
        //    ReadOnlyArray<InputControl> controls = device.allControls;
        //    float presspoint = InputSystem.settings.defaultButtonPressPoint;

        //    //Debug.Log("test1");

        //    foreach (InputControl inp in controls) {
        //        ButtonControl but = inp as ButtonControl;

        //        if (but == null || but.synthetic || but.noisy) {
        //            continue;
        //        }
        //        but.ReadValueFromEvent(eventPtr, out float value);

        //        //Debug.Log(pressedButtons.Contains(but));

        //        if (value >= presspoint && pressedButtons.Contains(but) == false) {
        //            Debug.Log("button pressed");
        //            pressedButtons.Add(but);
        //            break;
        //        }


        //    }

        //    return;
        //};



        //InputSystem.onEvent += (eventPtr, device) => {
        //    if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) {
        //        return;
        //    }
        //    ReadOnlyArray<InputControl> controls = device.allControls;
        //    float presspoint = InputSystem.settings.defaultButtonPressPoint;

        //    // Debug.Log("test2");
        //    foreach (InputControl inp in controls) {
        //        ButtonControl but = inp as ButtonControl;

        //        if (but == null || but.synthetic || but.noisy) {
        //            continue;
        //        }
        //        but.ReadValueFromEvent(eventPtr, out float value);
        //        //  Debug.Log(value);



        //        if (pressedButtons.Contains(but) == true && value <= presspoint) {
        //            pressedButtons.Remove(but);
        //            Debug.Log("button released");
        //            break;
        //        }


        //    }

        //    return;
        //};


        //    InputSystem.onEvent += (eventPtr, device) => {
        //    if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
        //        return;
        //    var controls = device.allControls;
        //    var buttonPressPoint = InputSytem.settings.defaultButtonPressPoint;
        //    for (var i = 0; i < controls.Count; ++i) {
        //        var control = controls[i] as ButtonControl;
        //        if (control == null || control.synthetic || control.noisy)
        //            continue;
        //        if (control.ReadValueFromEvent(eventPtr, out var value) && value >= buttonPressPoint) {
        //            m_ButtonPressed = true;
        //            break;
        //        }
        //    }
        //};


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

    private void anyButtonWasPressed(InputEventPtr eventPtr, InputDevice device) {
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) {
            return;
        }
        ReadOnlyArray<InputControl> controls = device.allControls;
        float presspoint = InputSystem.settings.defaultButtonPressPoint;

        //Debug.Log("test1");

        foreach (InputControl inp in controls) {
            ButtonControl but = inp as ButtonControl;

            if (but == null || but.synthetic || but.noisy) {
                continue;
            }
            but.ReadValueFromEvent(eventPtr, out float value);

            //Debug.Log(pressedButtons.Contains(but));

            if (value >= presspoint && pressedButtons.Contains(but) == false) {
                //  Debug.Log("button pressed");
                nextSlide();
                pressedButtons.Add(but);
                break;
            }


        }

        return;
    }

    private void anyButtonWasReleased(InputEventPtr eventPtr, InputDevice device) {
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>()) {
            return;
        }
        ReadOnlyArray<InputControl> controls = device.allControls;
        float presspoint = InputSystem.settings.defaultButtonPressPoint;

        // Debug.Log("test2");
        foreach (InputControl inp in controls) {
            ButtonControl but = inp as ButtonControl;

            if (but == null || but.synthetic || but.noisy) {
                continue;
            }
            but.ReadValueFromEvent(eventPtr, out float value);
            //  Debug.Log(value);



            if (pressedButtons.Contains(but) == true && value <= presspoint) {
                pressedButtons.Remove(but);
                //  Debug.Log("button released");
                break;
            }


        }

        return;
    }



    private void nextSlide() {




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

        //if (buttonEvent != null) {
        //    buttonEvent.Dispose();
        //    buttonEvent = null;
        //}


        InputSystem.onEvent -= anyButtonWasPressed;
        InputSystem.onEvent -= anyButtonWasReleased;
    }

}
