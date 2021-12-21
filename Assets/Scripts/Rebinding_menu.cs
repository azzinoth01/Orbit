using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// classe die das Rebinden von controls verwaltet
/// </summary>
public class Rebinding_menu : MonoBehaviour
{
    private Controlls controll;

    public GameObject ScrollView;
    public GameObject itemHolder;
    public GameObject actionName;
    public GameObject actionKeybind;
    public GameObject buttonRebind;
    public GameObject buttonDefault;
    public GameObject saveAlert;
    public GameObject buttonBack;
    public GameObject buttonSave;


    private InputActionRebindingExtensions.RebindingOperation rebind;
    private List<Text> checkKeybindList;
    private List<Button> buttonList;

    private string[] compositNames;


    /// <summary>
    /// setzt das Rebinding auf den Default Wert zur�ck
    /// </summary>
    /// <param name="action"> die action die gerebindet werden soll </param>
    /// <param name="index"> der Index des Bindings das gerebindet werden soll</param>
    /// <param name="displayKeybind"> die Text Anzeige f�r das Keybind</param>
    private void defaultButtonFunction(InputAction action, int index, Text displayKeybind) {
        //int index = action.GetBindingIndex(binding);
        action.RemoveBindingOverride(index);
        action.GetBindingDisplayString(index, out string device, out string path);
        if (device == "Keyboard") {

            path = Keyboard.current[path].displayName;
            ;
        }
        displayKeybind.text = device + ": " + path;
        checkKeybinds();
    }

    /// <summary>
    /// �ndert das Rebinding auf den input vom Player nach klicken des Buttons
    /// mit esc wird das rebidning abgebrochen
    /// </summary>
    /// <param name="action">die action die gerebindet werden soll</param>
    /// <param name="index">der Index des Bindings das gerebindet werden soll</param>
    /// <param name="displayKeybind">die Text Anzeige f�r das Keybind</param>
    private void remapButtonFunction(InputAction action, int index, Text displayKeybind) {
        //Debug.Log("test");
        ////int index = action.GetBindingIndex(binding);
        //Debug.Log(index);

        Debug.Log(index);

        rebind = action.PerformInteractiveRebinding(index);

        rebind.WithCancelingThrough("<Keyboard>/escape");

        displayKeybind.text = "waiting for input";

        rebind.OnComplete(operation => {
            operation.action.GetBindingDisplayString(index, out string device, out string path);
            foreach (InputDevice inputDevice in InputSystem.devices) {
                if (inputDevice.name == device) {
                    path = inputDevice[path].displayName;
                    //Debug.Log(path);
                    break;
                }
            }

            displayKeybind.text = device + ": " + path;
            rebind.Dispose();
            rebind = null;
            checkKeybinds();

        });
        rebind.OnCancel(operation => {
            operation.action.GetBindingDisplayString(index, out string device, out string path);
            foreach (InputDevice inputDevice in InputSystem.devices) {
                if (inputDevice.name == device) {
                    path = inputDevice[path].displayName;
                    //Debug.Log(path);
                    break;
                }
            }
            displayKeybind.text = device + ": " + path;
            rebind.Dispose();
            rebind = null;
            checkKeybinds();

        });
        rebind.Start();

    }


    /// <summary>
    /// speichert das Rebinding in einer json datei Userspezifisch auf dem PC
    /// es darf auch nur gespeichert werden, wenn alle rebinds eindeutig sind
    /// </summary>
    public void saveRebinding() {

        if (checkKeybinds() == true) {

            string json = controll.asset.SaveBindingOverridesAsJson();

            using (FileStream file = File.Create(Application.persistentDataPath + "/controllsSave.json")) {
                using (StreamWriter writer = new StreamWriter(file)) {
                    writer.Write(json);

                }
            }

            if (Globals.virtualMouse != null) {
                Globals.virtualMouse.loadNewRebinds();
            }
        }
        else {


            saveAlert.SetActive(true);

            buttonBack.GetComponent<Button>().interactable = false;
            buttonSave.GetComponent<Button>().interactable = false;

            foreach (Button b in buttonList) {
                b.interactable = false;
            }
        }

    }
    /// <summary>
    /// ladet die Rebinds in den controller rein
    /// </summary>
    /// <param name="cont">der controlls auf den die Rebinds gesetzt werden sollen</param>
    /// <returns> gibt den controller zur�ck auf den die Rebinds gesetzt wurden </returns>
    public Controlls loadRebinding(Controlls cont) {

        if (System.IO.File.Exists(Application.persistentDataPath + "/controllsSave.json")) {
            string json = File.ReadAllText(Application.persistentDataPath + "/controllsSave.json");

            if (json == null || json == "") {
                return cont;
            }

            cont.asset.LoadBindingOverridesFromJson(json);
        }


        return cont;
    }

    /// <summary>
    /// schlie�t das Alert Fenster, wenn das Speichern nicht erfolgreich war
    /// </summary>
    public void onClickAlertButton() {
        saveAlert.SetActive(false);
        buttonBack.GetComponent<Button>().interactable = true;
        buttonSave.GetComponent<Button>().interactable = true;
        foreach (Button b in buttonList) {
            b.interactable = true;
        }

    }
    /// <summary>
    /// �berpr�ft die keybinds und markiert diese Rot, wenn sie doppelt vorkommen
    /// </summary>
    /// <returns></returns>
    private bool checkKeybinds() {
        bool check = true;
        foreach (Text t1 in checkKeybindList) {
            t1.color = Color.black;
            foreach (Text t2 in checkKeybindList.FindAll(x => x.text == t1.text && x != t1).ToArray()) {
                t2.color = Color.red;
                t1.color = Color.red;
                check = false;
            }
        }
        return check;
    }

    /// <summary>
    /// Rebinding liste wird erstellt
    /// </summary>
    private void OnEnable() {


        compositNames = new string[4];

        compositNames[0] = "UP";
        compositNames[1] = "DOWN";
        compositNames[2] = "LEFT";
        compositNames[3] = "RIGHT";

        if (controll == null) {
            checkKeybindList = new List<Text>();
            buttonList = new List<Button>();
            controll = new Controlls();

            controll = loadRebinding(controll);


            InputActionMap[] map = controll.asset.actionMaps.ToArray();




            foreach (InputActionMap m in map) {

                if (m.name == "UI") {
                    continue;
                }
                InputAction[] actions = m.actions.ToArray();
                foreach (InputAction a in actions) {



                    if (a.bindings.Count > 1) {
                        int i = 0;
                        int comp = 0;
                        int index = 0;
                        foreach (InputBinding b in a.bindings) {

                            if (b.isComposite == true) {
                                comp = 0;
                                index = index + 1;
                                continue;
                            }

                            if (m.name == "VirtualMouse") {
                                a.GetBindingDisplayString(index, out string deviceCheck, out string pathCheck);


                                bool bBreak = false;
                                foreach (InputDevice inputDevice in InputSystem.devices) {
                                    if (inputDevice.name == deviceCheck) {

                                        if (inputDevice is Mouse) {
                                            index = index + 1;
                                            bBreak = true;
                                            break;
                                        }


                                    }
                                }

                                if (bBreak == true) {
                                    continue;
                                }

                            }

                            GameObject g = Instantiate(itemHolder, ScrollView.transform);

                            GameObject obj = Instantiate(actionName, g.transform);

                            if (b.isPartOfComposite == true) {
                                obj.GetComponent<Text>().text = (a.name + " " + (i + 1).ToString() + " " + compositNames[comp] + ":").Replace("_", " ");
                                //Debug.Log(compositNames[comp]);
                            }
                            else {
                                obj.GetComponent<Text>().text = (a.name + " " + (i + 1).ToString() + ":").Replace("_", " ");
                            }


                            GameObject actionDisplay = Instantiate(actionKeybind, g.transform);

                            a.GetBindingDisplayString(index, out string device, out string path);



                            foreach (InputDevice inputDevice in InputSystem.devices) {
                                if (inputDevice.name == device) {
                                    path = inputDevice[path].displayName;


                                    //Debug.Log(path);
                                    break;
                                }
                            }



                            actionDisplay.GetComponent<Text>().text = device + ": " + path;

                            checkKeybindList.Add(actionDisplay.GetComponent<Text>());

                            obj = Instantiate(buttonRebind, g.transform);

                            // weil die listener nicht die value storen mit der sie erstellt wurden sondern die Referenz zur value
                            int indexValue = index;
                            InputAction actionForButton = a;
                            obj.GetComponent<Button>().onClick.AddListener(delegate {
                                remapButtonFunction(actionForButton, indexValue, actionDisplay.GetComponent<Text>());
                            });

                            buttonList.Add(obj.GetComponent<Button>());


                            obj = Instantiate(buttonDefault, g.transform);
                            obj.GetComponent<Button>().onClick.AddListener(delegate {
                                defaultButtonFunction(actionForButton, indexValue, actionDisplay.GetComponent<Text>());
                            });
                            buttonList.Add(obj.GetComponent<Button>());

                            if (b.isPartOfComposite == true) {

                                comp = comp + 1;
                            }
                            else {
                                i = i + 1;
                            }
                            index = index + 1;

                        }

                    }
                    else {

                        if (m.name == "VirtualMouse") {
                            a.GetBindingDisplayString(0, out string deviceCheck, out string pathCheck);


                            bool bBreak = false;
                            foreach (InputDevice inputDevice in InputSystem.devices) {
                                if (inputDevice.name == deviceCheck) {

                                    if (inputDevice is Mouse) {

                                        bBreak = true;
                                        break;
                                    }


                                }
                            }

                            if (bBreak == true) {
                                continue;
                            }

                        }

                        GameObject g = Instantiate(itemHolder, ScrollView.transform);

                        GameObject obj = Instantiate(actionName, g.transform);

                        obj.GetComponent<Text>().text = (a.name + ":").Replace("_", " ");

                        GameObject actionDisplay = Instantiate(actionKeybind, g.transform);

                        a.GetBindingDisplayString(0, out string device, out string path);

                        foreach (InputDevice inputDevice in InputSystem.devices) {
                            if (inputDevice.name == device) {
                                path = inputDevice[path].displayName;
                                //Debug.Log(path);
                                break;
                            }
                        }

                        actionDisplay.GetComponent<Text>().text = device + ": " + path;
                        checkKeybindList.Add(actionDisplay.GetComponent<Text>());
                        obj = Instantiate(buttonRebind, g.transform);


                        // weil die listener nicht die value storen mit der sie erstellt wurden sondern die Referenz zur value 
                        InputAction actionForButton = a;
                        obj.GetComponent<Button>().onClick.AddListener(delegate {
                            remapButtonFunction(actionForButton, 0, actionDisplay.GetComponent<Text>());
                        });
                        buttonList.Add(obj.GetComponent<Button>());

                        obj = Instantiate(buttonDefault, g.transform);
                        obj.GetComponent<Button>().onClick.AddListener(delegate {
                            defaultButtonFunction(actionForButton, 0, actionDisplay.GetComponent<Text>());
                        });
                        buttonList.Add(obj.GetComponent<Button>());

                    }



                }
            }

        }
    }

    /// <summary>
    /// cleart die rebindingliste aus dem Scrollview
    /// </summary>
    private void OnDisable() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
        controll.Dispose();
        controll = null;
        checkKeybindList.Clear();
        buttonList.Clear();
    }

}
