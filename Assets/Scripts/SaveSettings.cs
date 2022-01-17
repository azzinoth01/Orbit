using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// container classe zum speichern von Settings
/// </summary>
[Serializable]
public class SaveSettings
{
    [SerializeField] private bool isMute;
    [SerializeField] private float backgroundVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private float masterVolume;

    public bool IsMute {
        get {
            return isMute;
        }

        set {
            isMute = value;
        }
    }

    public float BackgroundVolume {
        get {
            return backgroundVolume;
        }

        set {
            backgroundVolume = value;
        }
    }

    public float SfxVolume {
        get {
            return sfxVolume;
        }

        set {
            sfxVolume = value;
        }
    }

    public float MasterVolume {
        get {
            return masterVolume;
        }

        set {
            masterVolume = value;
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
    }

    /// <summary>
    /// speichert die Settings
    /// </summary>
    public void savingSetting() {

        string json = JsonUtility.ToJson(this);
        using (FileStream file = File.Create(Application.persistentDataPath + "/saveSettings.json")) {
            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(json);

            }
        }

    }

    /// <summary>
    /// ladet die Settings aus einer gespeicherten Datei, wenn vorhanden
    /// </summary>
    /// <returns> gibt die gespeicherten Settings zurück</returns>
    public static SaveSettings loadSettings() {

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

    }
}
