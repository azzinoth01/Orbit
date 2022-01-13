using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// controlliert die Soundeffecte
/// </summary>
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

    public Image muteButton;
    public Slider backgroundSlider;
    public Slider sfxSlider;

    public Sprite muteSp;
    public Sprite unMuteSp;


    /// <summary>
    /// ladet die start Soundsettings, wenn welche vorhanden sind, ansonsten werden die Standardsettings verwendet
    /// </summary>
    private void Start() {

        saveSetting = SaveSettings.loadSettings();
        if (saveSetting == null) {

            saveSetting = new SaveSettings(false, 1, 1);
            saveSetting.savingSetting();

        }

        isMute = saveSetting.IsMute;
        backgroundVolume = saveSetting.BackgroundVolume;
        sfxVolume = saveSetting.SfxVolume;


        setStartSettings();
    }




    /// <summary>
    /// setzt die geladen Settings auf die UI und stellt die Soundgroups ein
    /// </summary>
    public void setStartSettings() {
        if (isMute == true) {
            mute.TransitionTo(0);
            muteButton.sprite = muteSp;

        }
        else {
            normal.TransitionTo(0);
            muteButton.sprite = unMuteSp;

        }

        float volume;

        if (backgroundVolume == 0) {
            volume = -80;
        }
        else {
            volume = Mathf.Log10(backgroundVolume) * 20;
        }
        background.audioMixer.SetFloat("backgroundVolume", volume);

        backgroundSlider.value = backgroundVolume;

        if (sfxVolume == 0) {
            volume = -80;
        }
        else {
            volume = Mathf.Log10(sfxVolume) * 20;
        }
        sfx.audioMixer.SetFloat("sfxVolume", volume);

        sfxSlider.value = sfxVolume;

    }


    /// <summary>
    /// toggelt den Mute button
    /// </summary>
    public void toggleMute() {

        if (isMute == false) {
            mute.TransitionTo(0);
            muteButton.sprite = muteSp;
            isMute = true;
        }
        else {
            normal.TransitionTo(0);
            muteButton.sprite = unMuteSp;
            isMute = false;
        }
        saveSettingChanges();
    }

    /// <summary>
    /// setzt den sfx Sound nach ändern des Sliders
    /// </summary>
    public void sfxChanged() {
        sfxVolume = sfxSlider.value;

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

    /// <summary>
    /// setzt den backgroundsound nach ändern des Sliders
    /// </summary>
    public void backgroundSoundChange() {

        backgroundVolume = backgroundSlider.value;

        Debug.Log("changed");
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

    /// <summary>
    /// speichert die Soundsettings
    /// </summary>
    public void saveSettingChanges() {
        saveSetting.IsMute = isMute;
        saveSetting.BackgroundVolume = backgroundVolume;
        saveSetting.SfxVolume = sfxVolume;

        saveSetting.savingSetting();
    }
}
