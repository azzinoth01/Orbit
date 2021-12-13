using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
    public AudioMixerGroup masterGroup;
    public AudioMixerGroup background;
    public AudioMixerGroup sfx;
    public AudioMixerSnapshot normal;
    public AudioMixerSnapshot mute;

    private bool isMute;
    private float backgroundVolume;
    private float sfxVolume;

    private SaveSettings saveSetting;



    private void Start() {
        saveSetting = saveSetting.loadSettings();
        if (saveSetting == null) {

            saveSetting = new SaveSettings(false, 1, 1);
        }
        else {

            isMute = saveSetting.IsMute;
            backgroundVolume = saveSetting.BackgroundVolume;
            sfxVolume = saveSetting.SfxVolume;


        }
    }



    private void Update() {
        //float volume;

        //if (slider == 0) {
        //    volume = -80;
        //}
        //else {
        //    volume = Mathf.Log10(slider) * 20;
        //}



        // Debug.Log(volume);

        //master.audioMixer.SetFloat("masterVolume", volume);

    }

    public void setStartSettings() {
        if (isMute == true) {
            mute.TransitionTo(0);

        }
        else {
            normal.TransitionTo(0);

        }

        float volume;

        if (backgroundVolume == 0) {
            volume = -80;
        }
        else {
            volume = Mathf.Log10(backgroundVolume) * 20;
        }
        background.audioMixer.SetFloat("backgroundVolume", volume);

        if (sfxVolume == 0) {
            volume = -80;
        }
        else {
            volume = Mathf.Log10(sfxVolume) * 20;
        }
        sfx.audioMixer.SetFloat("sfxVolume", volume);


    }

    public void toggleMute() {

        if (isMute == false) {
            mute.TransitionTo(0);
            isMute = true;
        }
        else {
            normal.TransitionTo(0);
            isMute = false;
        }
        saveSettingChanges();
    }

    public void sfxChanged() {
        float volume;

        if (sfxVolume == 0) {
            volume = -80;
        }
        else {
            volume = Mathf.Log10(sfxVolume) * 20;
        }
        sfx.audioMixer.SetFloat("sfxVolume", volume);
        saveSettingChanges();
    }

    public void backgroundSoundChange() {

        float volume;

        if (backgroundVolume == 0) {
            volume = -80;
        }
        else {
            volume = Mathf.Log10(backgroundVolume) * 20;
        }
        background.audioMixer.SetFloat("backgroundVolume", volume);

        saveSettingChanges();
    }

    public void saveSettingChanges() {
        saveSetting.IsMute = isMute;
        saveSetting.BackgroundVolume = backgroundVolume;
        saveSetting.SfxVolume = sfxVolume;

        saveSetting.savingSetting();
    }
}
