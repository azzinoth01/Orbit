using nn.fs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using File = System.IO.File;

/// <summary>
/// class to save the player progress
/// </summary>
[Serializable]
public class PlayerSave {
    [SerializeField] private int money;
    [SerializeField] private WeaponInfo mainWeapon;
    [SerializeField] private WeaponInfo secondaryWeapon;
    [SerializeField] private WeaponInfo secondaryWeapon1;
    [SerializeField] private Parts shieldPart;
    [SerializeField] private List<string> boughtItems;
    [SerializeField] private bool tutorialPlayed;
    [SerializeField] private bool level1Played;


    /// <summary>
    /// player money
    /// </summary>
    public int Money {
        get {
            return money;
        }

        set {
            money = value;
        }
    }

    /// <summary>
    /// main weapon of player
    /// </summary>
    public WeaponInfo MainWeapon {
        get {
            return mainWeapon;
        }

        set {
            mainWeapon = value;
        }
    }
    /// <summary>
    /// secondary weapon of player
    /// </summary>
    public WeaponInfo SecondaryWeapon {
        get {
            return secondaryWeapon;
        }

        set {
            secondaryWeapon = value;
        }
    }
    /// <summary>
    /// second secondary weapon of player
    /// </summary>
    public WeaponInfo SecondaryWeapon1 {
        get {
            return secondaryWeapon1;
        }

        set {
            secondaryWeapon1 = value;
        }
    }
    /// <summary>
    /// ship parts of player
    /// </summary>
    public Parts ShieldPart {
        get {
            return shieldPart;
        }

        set {
            shieldPart = value;
        }
    }
    /// <summary>
    /// list of bought items in the shop
    /// </summary>
    public List<string> BoughtItems {
        get {
            return boughtItems;
        }

        set {
            boughtItems = value;
        }
    }
    /// <summary>
    /// turorial finished save
    /// </summary>
    public bool TutorialPlayed {
        get {
            return tutorialPlayed;
        }

        set {
            tutorialPlayed = value;
        }
    }
    /// <summary>
    /// level 1 finished save
    /// </summary>
    public bool Level1Played {
        get {
            return level1Played;
        }

        set {
            level1Played = value;
        }
    }

    /// <summary>
    /// base values for a new player
    /// </summary>
    public PlayerSave() {
        money = 0;
        mainWeapon = null;
        secondaryWeapon = null;
        secondaryWeapon1 = null;
        shieldPart = null;
        boughtItems = new List<string>();
        boughtItems.Add("1004");

        tutorialPlayed = true;
        level1Played = false;


        ItemCatalog cat = new ItemCatalog();

        foreach (WeaponInfo info in Globals.itemCatalogSave.WeaponInfo) {
            cat.ItemList.Add(info);
        }
        foreach (Parts info in Globals.itemCatalogSave.ParstInfos) {
            cat.ItemList.Add(info);
        }
        Globals.catalog = cat;


        mainWeapon = (WeaponInfo)cat.ItemList.Find(x => x.ID == "1004");
    }


    /// <summary>
    /// saves player information
    /// </summary>
    public void savingSetting() {
        Debug.Log("Saving data");
#if UNITY_SWITCH
        //switch

        string json = JsonUtility.ToJson(this);


        nn.Result result;

        UnityEngine.Switch.Notification.EnterExitRequestHandlingSection();

        string filePath = string.Format("{0}:/{1}", "OrbitSaveFiles", "savePlayer.sav");


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
        Debug.Log("Saving finisehd");
        return;
#else

        //windows

        //  string json = JsonUtility.ToJson(this);
        using (FileStream file = File.Create(Application.persistentDataPath + "/savePlayer.sav")) {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, this);


        }
#endif


    }

    /// <summary>
    /// loads the player information from the saved data, if exists
    /// </summary>
    /// <returns> returns the saved player information </returns>
    public static PlayerSave loadSettings() {


#if UNITY_SWITCH


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

        s = JsonUtility.FromJson<PlayerSave>(saveData);

        return s;

#else
        // windows

        PlayerSave s = new PlayerSave();


        if (System.IO.File.Exists(Application.persistentDataPath + "/savePlayer.sav")) {

            BinaryFormatter bf = new BinaryFormatter();

            using (FileStream file = File.Open(Application.persistentDataPath + "/savePlayer.sav", FileMode.Open)) {
                s = (PlayerSave)bf.Deserialize(file);
            }


            //string json = File.ReadAllText(Application.persistentDataPath + "/savePlayer.sav");

            //if (json == null || json == "") {
            //    return null;
            //}


            //s = JsonUtility.FromJson<PlayerSave>(json);

            if (s == null) {
                return null;
            }
            return s;

        }
        return null;
#endif
    }
}
