using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controliert die Particel system effecte
/// </summary>
public class ParticleControl : MonoBehaviour
{
    public ParticleSystem particle;
    public bool destroyAfterPlay;

    /// <summary>
    /// startet das particle system 
    /// </summary>
    private void OnEnable() {
        particle.Play();
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
            if (particle.isPlaying == false) {
                if (destroyAfterPlay == true) {
                    Destroy(gameObject);
                }
                else {
                    gameObject.SetActive(false);
                }

            }

        }

    }
}
