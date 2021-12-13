using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public ParticleSystem particle;
    public bool destroyAfterPlay;

    /// <summary>
    /// starts the particle system playing
    /// </summary>
    private void OnEnable() {
        particle.Play();
        //Debug.Log("start playing");
    }



    /// <summary>
    /// deactivates or destorys the gameobject after playing
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
