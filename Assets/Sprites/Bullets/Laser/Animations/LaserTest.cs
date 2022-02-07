using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// laser animaton control
/// </summary>
public class LaserTest : MonoBehaviour
{
    public List<Animator> Laser;
    public bool Trigger;
    public AudioSource startAudio;
    public AudioSource loopAudio;
    private bool started;

    /// <summary>
    /// sets base values
    /// </summary>
    void Start() {
        started = false;
    }

    /// <summary>
    /// activates the laser animation
    /// </summary>
    void Update() {



        if (Trigger == true) {
            foreach (Animator a in Laser) {
                a.SetBool("LaserON", true);
                if (startAudio != null && started == false) {
                    startAudio.Play();
                    started = true;
                }
            }
            //Debug.Log("test");

            if (startAudio != null && startAudio.isPlaying == false) {
                //  Debug.Log("test2");
                if (loopAudio != null && loopAudio.isPlaying == false) {
                    //Debug.Log("testing");
                    loopAudio.Play();
                }
            }
        }
        else {
            foreach (Animator a in Laser) {
                a.SetBool("LaserON", false);

                if (loopAudio != null) {
                    loopAudio.Stop();
                    started = false;
                    // Debug.Log("sopped");
                }
            }
        }
    }
}
