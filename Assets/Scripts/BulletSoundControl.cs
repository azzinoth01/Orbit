using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSoundControl : MonoBehaviour
{

    public AudioSource audios;

    private void OnEnable() {
        audios.Play();
    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            if (audios.isPlaying == false) {
                gameObject.SetActive(false);

                try {
                    Skill s = GetComponentInParent<Skill>();
                    s.checkDisabled();
                }
                catch {

                }
            }
        }

    }
}
