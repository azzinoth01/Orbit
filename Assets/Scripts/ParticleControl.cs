using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controliert die Particle system effecte und Particle Sounds
/// </summary>
public class ParticleControl : MonoBehaviour
{
    public ParticleSystem particle;
    public bool destroyAfterPlay;
    public AudioSource particleAudio;
    public Animator anim;

    /// <summary>
    /// startet das particle system 
    /// </summary>
    private void OnEnable() {
        if (particle != null) {
            particle.Play();
        }

        if (particleAudio != null) {
            particleAudio.Play();
        }
        if (anim != null) {
            anim.enabled = true;
        }


        //Debug.Log("start playing");
    }




    /// <summary>
    /// deactivates or destorys the gameobject nach dem spielen
    /// </summary>
    private void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            if (isPlayingCheck() == false) {
                if (destroyAfterPlay == true) {
                    //Debug.Log("test");
                    Destroy(gameObject);
                }
                else {
                    //Debug.Log("test");
                    gameObject.SetActive(false);
                }

            }

        }

    }

    /// <summary>
    /// checks if audio or particle system is still playing
    /// </summary>
    /// <returns>returns true if it is still playing</returns>
    private bool isPlayingCheck() {

        bool check = true;
        if (particleAudio != null) {
            if (particleAudio.isPlaying == false) {
                check = false;
            }
            else {
                return true;
            }
        }
        if (particle != null) {
            if (particle.isPlaying == false) {
                check = false;
            }
            else {
                return true;
            }
        }
        if (anim != null) {
            if (anim.enabled == false) {
                check = false;
            }
            else {
                return true;
            }
        }


        return check;
    }
}
