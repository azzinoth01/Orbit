using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTest : MonoBehaviour
{
    public List<Animator> Laser;
    public bool Trigger;
    public AudioSource startAudio;
    public AudioSource loopAudio;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {



        if (Trigger == true) {
            foreach (Animator a in Laser) {
                a.SetBool("LaserON", true);
                if (startAudio != null) {
                    startAudio.Play();
                }
            }

            if (startAudio != null && startAudio.isPlaying == false) {
                if (loopAudio != null) {
                    loopAudio.Play();
                }
            }
        }
        else {
            foreach (Animator a in Laser) {
                a.SetBool("LaserON", false);

                if (loopAudio != null) {
                    loopAudio.Stop();
                }
            }
        }
    }
}
