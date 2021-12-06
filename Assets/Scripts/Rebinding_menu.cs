using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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



    private void defaultButtonFunction(InputAction action, int index, Text displayKeybind) {
        //int index = action.GetBindingIndex(binding);
        action.RemoveBindingOverride(index);
        action.GetBindingDisplayString(index, out string device, out string path);
        displayKeybind.text = device + ": " + path;
        checkKeybinds();
    }

    private void remapButtonFunction(InputAction action, int index, Text displayKeybind) {
        //Debug.Log("test");
        ////int index = action.GetBindingIndex(binding);
        //Debug.Log(index);

        rebind = action.PerformInteractiveRebinding(index);

        rebind.WithCancelingThrough("<Keyboard>/escape");

        displayKeybind.text = "waiting for input";

        rebind.OnComplete(operation => {
            operation.action.GetBindingDisplayString(index, out string device, out string path);
            displayKeybind.text = device + ": " + path;
            rebind.Dispose();
            rebind = null;
            checkKeybinds();

        });
        rebind.OnCancel(operation => {
            operation.action.GetBindingDisplayString(index, out string device, out string path);
            displayKeybind.text = device + ": " + path;
            rebind.Dispose();
            rebind = null;
            checkKeybinds();

        });
        rebind.Start();

    }



    public void saveRebinding() {

        if (checkKeybinds() == true) {

            string json = controll.asset.SaveBindingOverridesAsJson();

            using (FileStream file = File.Create(Application.persistentDataPath + "/controllsSave.json")) {
                using (StreamWriter writer = new StreamWriter(file)) {
                    writer.Write(json);

                }
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


    public void onClickAlertButton() {
        saveAlert.SetActive(false);
        buttonBack.GetComponent<Button>().interactable = true;
        buttonSave.GetComponent<Button>().interactable = true;
        foreach (Button b in buttonList) {
            b.interactable = true;
        }

    }

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

    private void OnEnable() {
        if (controll == null) {
            checkKeybindList = new List<Text>();
            buttonList = new List<Button>();
            controll = new Controlls();

            controll = loadRebinding(controll);


            InputActionMap[] map = controll.asset.actionMaps.ToArray();




            foreach (InputActionMap m in map) {
                InputAction[] actions = m.actions.ToArray();
                foreach (InputAction a in actions) {



                    if (a.bindings.Count > 1) {
                        int i = 0;
                        foreach (InputBinding b in a.bindings) {
                            GameObject g = Instantiate(itemHolder, ScrollView.transform);

                            GameObject obj = Instantiate(actionName, g.transform);

                            obj.GetComponent<Text>().text = (a.name + " " + (i + 1).ToString() + ":").Replace("_", " ");

                            GameObject actionDisplay = Instantiate(actionKeybind, g.transform);

                            a.GetBindingDisplayString(i, out string device, out string path);

                            actionDisplay.GetComponent<Text>().text = device + ": " + path;

                            checkKeybindList.Add(actionDisplay.GetComponent<Text>());

                            obj = Instantiate(buttonRebind, g.transform);

                            // weil die listener nicht die value storen mit der sie erstellt wurden sondern die Referenz zur value
                            int index = i;
                            InputAction actionForButton = a;
                            obj.GetComponent<Button>().onClick.AddListener(delegate {
                                remapButtonFunction(actionForButton, index, actionDisplay.GetComponent<Text>());
                            });

                            buttonList.Add(obj.GetComponent<Button>());


                            obj = Instantiate(buttonDefault, g.transform);
                            obj.GetComponent<Button>().onClick.AddListener(delegate {
                                defaultButtonFunction(actionForButton, index, actionDisplay.GetComponent<Text>());
                            });
                            buttonList.Add(obj.GetComponent<Button>());

                            i = i + 1;
                        }

                    }
                    else {

                        GameObject g = Instantiate(itemHolder, ScrollView.transform);

                        GameObject obj = Instantiate(actionName, g.transform);

                        obj.GetComponent<Text>().text = (a.name + ":").Replace("_", " ");

                        GameObject actionDisplay = Instantiate(actionKeybind, g.transform);

                        a.GetBindingDisplayString(0, out string device, out string path);
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
