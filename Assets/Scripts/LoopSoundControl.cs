using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSoundControl : MonoBehaviour
{

    public AudioSource startAudio;
    public AudioSource transitionAudio;
    public AudioSource loopAudio;

    private bool transitionPlayed;
    private bool isPlaying;

    public bool IsPlaying {
        get {
            return isPlaying;
        }


    }


    // Start is called before the first frame update
    void Start() {

        startPlaying();



    }

    // Update is called once per frame
    void Update() {

        if (transitionAudio != null) {
            if (transitionPlayed == false && startAudio.isPlaying == false && transitionAudio.isPlaying == false && loopAudio.isPlaying == false) {
                transitionAudio.Play();
                transitionPlayed = true;
                Debug.Log("transition started");
            }
            else if (loopAudio != null) {
                if (transitionPlayed == true && startAudio.isPlaying == false && transitionAudio.isPlaying == false && loopAudio.isPlaying == false) {
                    loopAudio.Play();
                    Debug.Log("loop started");
                }
            }
        }





    }

    public void startPlaying() {
        startAudio.Play();
        transitionPlayed = false;
        isPlaying = true;
    }

    public void stopPlaying() {
        isPlaying = false;
        startAudio.Stop();
        transitionAudio.Stop();
        loopAudio.Stop();
    }
}
