using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSoundControl : MonoBehaviour
{

    public AudioSource startAudio;
    public AudioSource transitionAudio;
    public AudioSource loopAudio;

    private bool transitionPlayed;

    // Start is called before the first frame update
    void Start() {
        startAudio.Play();
        transitionPlayed = false;
    }

    // Update is called once per frame
    void Update() {

        if (transitionPlayed == false && startAudio.isPlaying == false && transitionAudio.isPlaying == false && loopAudio.isPlaying == false) {
            transitionAudio.Play();
            transitionPlayed = true;
        }
        else if (transitionPlayed == true && startAudio.isPlaying == false && transitionAudio.isPlaying == false && loopAudio.isPlaying == false) {
            loopAudio.Play();
        }


    }
}
