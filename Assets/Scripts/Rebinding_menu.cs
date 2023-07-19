using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


/// <summary>
/// class which manages the rebinding of controls
/// </summary>
public class Rebinding_menu : MonoBehaviour {
    private Controlls controll;

    /// <summary>
    /// scroll view to place rebinding controls
    /// </summary>
    public GameObject ScrollView;
    /// <summary>
    /// object to contain 1 rebind prefab
    /// </summary>
    public GameObject itemHolder;
    /// <summary>
    /// rebind action name prefab
    /// </summary>
    public GameObject actionName;
    /// <summary>
    /// rebind keybind text prefab
    /// </summary>
    public GameObject actionKeybind;
    /// <summary>
    /// button for the rebinding prefab
    /// </summary>
    public GameObject buttonRebind;
    /// <summary>
    /// button for the default rebinding prefab
    /// </summary>
    public GameObject buttonDefault;
    /// <summary>
    /// save alert prefab
    /// </summary>
    public GameObject saveAlert;
    /// <summary>
    /// button back 
    /// </summary>
    public GameObject buttonBack;
    /// <summary>
    /// button to save
    /// </summary>
    public GameObject buttonSave;

    /// <summary>
    /// view panel
    /// </summary>
    public GameObject viewPanel;


    private InputActionRebindingExtensions.RebindingOperation rebind;
    private List<Text> checkKeybindList;
    private List<Button> buttonList;
    private List<Text> actionNameList;

    private string[] compositNames;

    private List<GameObject> gamepadControls;
    private List<GameObject> mouseAndKeyControls;


    /// <summary>
    /// sets the rebinding to the default value
    /// </summary>
    /// <param name="action"> the action which will be rebinded</param>
    /// <param name="index"> the index of the binding which will be rebinded</param>
    /// <param name="displayKeybind"> the text display for the keybind</param>
    private void defaultButtonFunction(InputAction action, int index, Text displayKeybind) {
        //int index = action.GetBindingIndex(binding);
        action.RemoveBindingOverride(index);
        action.GetBindingDisplayString(index, out string device, out string path);
        if (device == "Keyboard") {

            path = Keyboard.current[path].displayName;

        }
        //displayKeybind.text = device + ": " + path;

        displayKeybind.text = path;
        checkKeybinds();
    }

    /// <summary>
    /// changes the rebind to the input of the player after clicking the button
    /// esc stops the rebinding
    /// </summary>
    /// <param name="action"> the action which will be rebinded</param>
    /// <param name="index"> the index of the binding which will be rebinded</param>
    /// <param name="displayKeybind"> the text display for the keybind</param>
    private void remapButtonFunction(InputAction action, int index, Text displayKeybind) {
        //Debug.Log("test");
        ////int index = action.GetBindingIndex(binding);
        //Debug.Log(index);

        //Debug.Log(index);

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

            // displayKeybind.text = device + ": " + path;
            displayKeybind.text = path;
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
            // displayKeybind.text = device + ": " + path;
            displayKeybind.text = path;
            rebind.Dispose();
            rebind = null;
            checkKeybinds();

        });
        rebind.Start();



    }

    public void ResetRebinding() {
        Controlls resetControls = new Controlls();
        string json = resetControls.asset.SaveBindingOverridesAsJson();
        using (FileStream file = File.Create(Application.persistentDataPath + "/controllsSave.json")) {
            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(json);

            }
        }

        if (Globals.virtualMouse != null) {
            Globals.virtualMouse.loadNewRebinds();
        }

    }
    private void SaveSettingsSwitch(string json) {

        Debug.Log("Saving data");

        nn.Result result;

        UnityEngine.Switch.Notification.EnterExitRequestHandlingSection();

        string filePath = string.Format("{0}:/{1}", "OrbitSaveFiles", "saveControlls.sav");


        nn.fs.FileHandle handle = new nn.fs.FileHandle();



        byte[] data = Encoding.UTF8.GetBytes(json);

        while (true) {
            // Attempt to open the file in write mode.
            result = nn.fs.File.Open(ref handle, filePath, nn.fs.OpenFileMode.Write);
            // Check if file was opened successfully.
            if (result.IsSuccess()) {
                // Exit the loop because the file was successfully opened.
                break;
            }
            else {
                if (nn.fs.FileSystem.ResultPathNotFound.Includes(result)) {
                    // Create a file with the size of the encoded data if no entry exists.
                    result = nn.fs.File.Create(filePath, data.Length);

                    // Check if the file was successfully created.
                    if (!result.IsSuccess()) {
                        Debug.LogErrorFormat("Failed to create {0}: {1}", filePath, result.ToString());

                        return;
                    }
                    else {
                        break;
                    }
                }
                else {
                    // Generic fallback error handling for debugging purposes.
                    Debug.LogErrorFormat("Failed to open {0}: {1}", filePath, result.ToString());

                    return;
                }
            }


        }

        result = nn.fs.File.SetSize(handle, data.LongLength);

        if (nn.fs.FileSystem.ResultUsableSpaceNotEnough.Includes(result)) {
            Debug.LogErrorFormat("Insufficient space to write {0} bytes to {1}", data.LongLength, filePath);
            nn.fs.File.Close(handle);

            return;
        }


        result = nn.fs.File.Write(handle, 0, data, data.LongLength, nn.fs.WriteOption.Flush);

        // You do not need to handle this error here if you are not using nn.fs.OpenFileMode.AllowAppend.
        if (nn.fs.FileSystem.ResultUsableSpaceNotEnough.Includes(result)) {
            Debug.LogErrorFormat("Insufficient space to write {0} bytes to {1}", data.LongLength, filePath);
        }

        // The file must be closed before committing.
        nn.fs.File.Close(handle);

        // Verify that the write operation was successful before committing.
        if (!result.IsSuccess()) {
            Debug.LogErrorFormat("Failed to write {0}: {1}", filePath, result.ToString());
            return;
        }

        nn.fs.FileSystem.Commit("OrbitSaveFiles");
        UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection();
        return;
    }


    /// <summary>
    /// saves the rebinding in a json file user specific on the pc
    /// it can only be saved if all rebinds are unique
    /// </summary>
    public void saveRebinding() {

        if (checkKeybinds() == true) {

            string json = controll.asset.SaveBindingOverridesAsJson();

#if UNITY_SWITCH
            SaveSettingsSwitch(json);
#else
            using (FileStream file = File.Create(Application.persistentDataPath + "/controllsSave.json")) {
                using (StreamWriter writer = new StreamWriter(file)) {
                    writer.Write(json);

                }
            }
#endif
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
    /// loads the rebinds into the controler
    /// </summary>
    /// <param name="cont"> the controler on which the rebinds are to be sete</param>
    /// <returns> returns the controler with the set rebinds</returns>
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

    public string LoadSwitchRebinding() {
        Debug.Log("load data");
        PlayerSave s = new PlayerSave();
        nn.Result result;
        //switch
        string filePath = string.Format("{0}:/{1}", "OrbitSaveFiles", "savePlayer.sav");


        nn.fs.FileHandle handle = new nn.fs.FileHandle();

        // Attempt to open the file in read-only mode.
        result = nn.fs.File.Open(ref handle, filePath, nn.fs.OpenFileMode.Read);
        if (!result.IsSuccess()) {
            if (nn.fs.FileSystem.ResultPathNotFound.Includes(result)) {
                Debug.LogFormat("File not found: {0}", filePath);

                return "";
            }
            else {
                Debug.LogErrorFormat("Unable to open {0}: {1}", filePath, result.ToString());

                return "";
            }
        }

        long fileSize = 0;
        nn.fs.File.GetSize(ref fileSize, handle);
        // Allocate a buffer that matches the file size.
        byte[] data = new byte[fileSize];
        // Read the save data into the buffer.
        nn.fs.File.Read(handle, 0, data, fileSize);
        // Close the file.
        nn.fs.File.Close(handle);
        // Decode the UTF8-encoded data and store it in the string buffer.
        string saveData = Encoding.UTF8.GetString(data);

        return saveData;
    }

    /// <summary>
    /// closes the alert window if the saving was not succesful
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
    /// checks the keybind and markes them red if they are not unique
    /// </summary>
    /// <returns></returns>
    private bool checkKeybinds() {
        bool check = true;
        foreach (Text t1 in checkKeybindList) {
            t1.color = Color.black;
            foreach (Text t2 in checkKeybindList.FindAll(x => x.text == t1.text && x != t1).ToArray()) {
                if (t2.gameObject.activeInHierarchy == true && t1.gameObject.activeInHierarchy == true) {
                    t2.color = Color.red;
                    t1.color = Color.red;
                    check = false;
                }

            }
        }
        return check;
    }

    /// <summary>
    /// creates the rebinding list
    /// </summary>
    private void OnEnable() {


        compositNames = new string[4];

        compositNames[0] = "UP";
        compositNames[1] = "DOWN";
        compositNames[2] = "LEFT";
        compositNames[3] = "RIGHT";

        if (controll == null) {

            actionNameList = new List<Text>();

            checkKeybindList = new List<Text>();
            buttonList = new List<Button>();
            controll = new Controlls();

            mouseAndKeyControls = new List<GameObject>();
            gamepadControls = new List<GameObject>();

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

                            //Debug.Log(b.groups);

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

                            if (b.groups == Control_schemes_enum.gamepad.ToString()) {
                                gamepadControls.Add(g);
                            }
                            else if (b.groups == Control_schemes_enum.mouseAndKeyboard.ToString()) {
                                mouseAndKeyControls.Add(g);
                            }
                            else {
                                gamepadControls.Add(g);
                                mouseAndKeyControls.Add(g);
                            }
                            GameObject obj = Instantiate(actionName, g.transform);


                            if (b.isPartOfComposite == true) {
                                obj.GetComponent<Text>().text = (a.name + " " + (i + 1).ToString() + " " + compositNames[comp] + ":").Replace("_", " ");

                                //Debug.Log(compositNames[comp]);
                            }
                            else {
                                obj.GetComponent<Text>().text = (a.name + " " + (i + 1).ToString() + ":").Replace("_", " ");
                            }
                            obj.GetComponent<Text>().text = obj.GetComponent<Text>().text.TrimEnd();
                            actionNameList.Add(obj.GetComponent<Text>());

                            GameObject actionDisplay = Instantiate(actionKeybind, g.transform);

                            a.GetBindingDisplayString(index, out string device, out string path);



                            foreach (InputDevice inputDevice in InputSystem.devices) {
                                if (inputDevice.name == device) {
                                    path = inputDevice[path].displayName;


                                    //Debug.Log(path);
                                    break;
                                }
                            }



                            // actionDisplay.GetComponent<Text>().text = device + ": " + path;

                            actionDisplay.GetComponent<Text>().text = path;

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

                        if (a.bindings[0].groups == Control_schemes_enum.gamepad.ToString()) {
                            gamepadControls.Add(g);
                        }
                        else if (a.bindings[0].groups == Control_schemes_enum.mouseAndKeyboard.ToString()) {
                            mouseAndKeyControls.Add(g);
                        }
                        else {
                            gamepadControls.Add(g);
                            mouseAndKeyControls.Add(g);
                        }

                        GameObject obj = Instantiate(actionName, g.transform);

                        obj.GetComponent<Text>().text = (a.name + ":").Replace("_", " ");

                        obj.GetComponent<Text>().text = obj.GetComponent<Text>().text.TrimEnd();

                        actionNameList.Add(obj.GetComponent<Text>());

                        GameObject actionDisplay = Instantiate(actionKeybind, g.transform);

                        a.GetBindingDisplayString(0, out string device, out string path);

                        foreach (InputDevice inputDevice in InputSystem.devices) {
                            if (inputDevice.name == device) {
                                path = inputDevice[path].displayName;
                                //Debug.Log(path);
                                break;
                            }
                        }

                        // actionDisplay.GetComponent<Text>().text = device + ": " + path;

                        actionDisplay.GetComponent<Text>().text = path;
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


            foreach (GameObject g in gamepadControls) {
                g.SetActive(false);
            }
            foreach (GameObject g in mouseAndKeyControls) {
                g.SetActive(false);
            }

        }


    }

    /// <summary>
    /// starts the renaming action of the actions
    /// </summary>
    private void Start() {
        renameActionName();
    }

    ///// <summary>
    ///// cleart die rebindingliste aus dem Scrollview
    ///// </summary>
    //private void OnDisable() {
    //    //foreach (Transform t in transform) {
    //    //    Destroy(t.gameObject);
    //    //}
    //    //controll.Dispose();
    //    //controll = null;
    //    //checkKeybindList.Clear();
    //    //buttonList.Clear();
    //}


    /// <summary>
    /// disposes the controler 
    /// </summary>
    private void OnDestroy() {
        controll.Dispose();
        controll = null;
    }

    /// <summary>
    /// opens the gamepad rebinding menu
    /// </summary>
    public void onClickGamepad() {
        Globals.menuHandler.onClickMenuItem(gameObject);

        Globals.menuHandler.onClickActivateGameobnect(viewPanel);
        gameObject.SetActive(true);

        foreach (GameObject g in mouseAndKeyControls) {
            g.SetActive(false);
        }
        foreach (GameObject g in gamepadControls) {
            g.SetActive(true);
        }
    }

    /// <summary>
    /// opens the mouse and keyboard rebinding menu
    /// </summary>
    public void onClickMouseAndKey() {

        Globals.menuHandler.onClickMenuItem(gameObject);

        Globals.menuHandler.onClickActivateGameobnect(viewPanel);

        gameObject.SetActive(true);
        foreach (GameObject g in gamepadControls) {
            g.SetActive(false);
        }
        foreach (GameObject g in mouseAndKeyControls) {
            g.SetActive(true);
        }
    }

    /// <summary>
    /// renames the actions
    /// </summary>
    private void renameActionName() {

        //foreach (Text test in actionNameList) {
        //    Debug.Log(test.text);
        //}

        LoadAssets loader = new LoadAssets();


        TextAsset text = loader.loadText("Assets/Catalog/rebinding_rennaming.txt");

        //Debug.Log(text.text);

        string[] textArray = Regex.Split(text.text, "\r\n");

        string[] checkNames = new string[textArray.Length];
        string[] newNames = new string[textArray.Length];

        int counter = 0;
        foreach (string s in textArray) {

            if (s == "") {
                continue;
            }
            //Debug.Log("next index");

            int pos1 = s.IndexOf("\"") + 1;
            int pos2 = s.IndexOf("\"", pos1);
            string name = s.Substring(pos1, pos2 - pos1);

            checkNames[counter] = name;

            //Debug.Log(name);

            pos1 = s.IndexOf("\"", pos2 + 1) + 1;
            pos2 = s.IndexOf("\"", pos1 + 1);

            name = s.Substring(pos1, pos2 - pos1);

            newNames[counter] = name;
            //Debug.Log(name);

            counter = counter + 1;

        }
        //Debug.Log("test");

        foreach (Text t in actionNameList) {

            int index = Array.FindIndex(checkNames, x => x == t.text);
            //Debug.Log(index);
            //  Debug.Log(index);
            if (index == -1) {
                continue;
            }
            t.text = newNames[index];


        }

        loader.releaseAllHandle();
    }

}
