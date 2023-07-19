using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


/// <summary>
/// container classe to save settings
/// </summary>
[Serializable]
public class SaveSettings {
    [SerializeField] private bool isMute;
    [SerializeField] private float backgroundVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private float masterVolume;
    [SerializeField] private bool isToogleOn;


    /// <summary>
    /// returns and sets mute
    /// </summary>
    public bool IsMute {
        get {
            return isMute;
        }

        set {
            isMute = value;
        }
    }
    /// <summary>
    /// returns and sets background volume
    /// </summary>
    public float BackgroundVolume {
        get {
            return backgroundVolume;
        }

        set {
            backgroundVolume = value;
        }
    }
    /// <summary>
    /// returns and sets sfx volume
    /// </summary>
    public float SfxVolume {
        get {
            return sfxVolume;
        }

        set {
            sfxVolume = value;
        }
    }
    /// <summary>
    /// returns and sets master volume
    /// </summary>
    public float MasterVolume {
        get {
            return masterVolume;
        }

        set {
            masterVolume = value;
        }
    }
    /// <summary>
    /// is toogle on
    /// </summary>
    public bool IsToogleOn {
        get {
            return isToogleOn;
        }

        set {
            isToogleOn = value;
        }
    }

    /// <summary>
    /// standard consturktor
    /// </summary>
    /// <param name="isMute"> setzt mute button</param>
    /// <param name="backgroundVolume"> setzt background Volume 0-1</param>
    /// <param name="sfxVolume"> setzt sfx Volume 0-1</param>
    public SaveSettings(bool isMute, float backgroundVolume, float sfxVolume, float masterVolume) {

        this.isMute = isMute;
        this.backgroundVolume = backgroundVolume;
        this.sfxVolume = sfxVolume;
        this.masterVolume = masterVolume;
        isToogleOn = true;
    }

    /// <summary>
    /// saves the settings
    /// </summary>
    public void savingSetting() {
        Debug.Log("Saving data");

#if UNITY_SWITCH
        //switch

        string json = JsonUtility.ToJson(this);


        nn.Result result;

        UnityEngine.Switch.Notification.EnterExitRequestHandlingSection();

        string filePath = string.Format("{0}:/{1}", "OrbitSaveFiles", "saveSettings.sav");


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
#else


        string json = JsonUtility.ToJson(this);
        using (FileStream file = File.Create(Application.persistentDataPath + "/saveSettings.json")) {
            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(json);

            }
        }
#endif
    }

    /// <summary>
    /// loads the setting out of a saved data if it exists
    /// </summary>
    /// <returns> returns the saved settings</returns>
    public static SaveSettings loadSettings() {

        Debug.Log("loading data");
#if UNITY_SWITCH
        SaveSettings s = new SaveSettings(false, 1, 1, 1);
        nn.Result result;
        //switch
        string filePath = string.Format("{0}:/{1}", "OrbitSaveFiles", "saveSettings.sav");


        nn.fs.FileHandle handle = new nn.fs.FileHandle();

        // Attempt to open the file in read-only mode.
        result = nn.fs.File.Open(ref handle, filePath, nn.fs.OpenFileMode.Read);
        if (!result.IsSuccess()) {
            if (nn.fs.FileSystem.ResultPathNotFound.Includes(result)) {
                Debug.LogFormat("File not found: {0}", filePath);

                return s;
            }
            else {
                Debug.LogErrorFormat("Unable to open {0}: {1}", filePath, result.ToString());

                return s;
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

        s = JsonUtility.FromJson<SaveSettings>(saveData);

        return s;

#else

        SaveSettings s = new SaveSettings(false, 1, 1, 1);


        if (System.IO.File.Exists(Application.persistentDataPath + "/saveSettings.json")) {
            string json = File.ReadAllText(Application.persistentDataPath + "/saveSettings.json");

            if (json == null || json == "") {
                return null;
            }

            s = JsonUtility.FromJson<SaveSettings>(json);
            return s;

        }
        return null;
#endif
    }
}
