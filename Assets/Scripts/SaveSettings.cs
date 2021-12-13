using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveSettings
{
    [SerializeField] private bool isMute;
    [SerializeField] private float backgroundVolume;
    [SerializeField] private float sfxVolume;

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

    public SaveSettings(bool isMute, float backgroundVolume, float sfxVolume) {

        this.isMute = isMute;
        this.backgroundVolume = backgroundVolume;
        this.sfxVolume = sfxVolume;
    }


    public void savingSetting() {

        string json = JsonUtility.ToJson(this);
        using (FileStream file = File.Create(Application.persistentDataPath + "/saveSettings.json")) {
            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(json);

            }
        }

    }

    public SaveSettings loadSettings() {

        SaveSettings s;

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
