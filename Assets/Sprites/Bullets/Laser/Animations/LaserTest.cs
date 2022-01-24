using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTest : MonoBehaviour
{
    public List<Animator> Laser;
    public bool Trigger;
    public AudioSource startAudio;
    public AudioSource loopAudio;
    private bool started;

    // Start is called before the first frame update
    void Start() {
        started = false;
    }

    // Update is called once per frame
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
