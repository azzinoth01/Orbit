using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSoundSource : MonoBehaviour
{
    // Start is called before the first frame update


    //public AudioSource audios;
    public List<int> playOnSceneIndex;
    public string dontDestroyID;

    public LoopSoundControl audios;

    void Start() {



        if (Globals.dontDestoryOnLoadObjectID.Contains(dontDestroyID)) {

            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            Globals.dontDestoryOnLoadObjectID.Add(dontDestroyID);

            audios.startPlaying();

        }

    }

    // Update is called once per frame
    //void Update() {

    //}


    private void OnLevelWasLoaded(int level) {


        if (playOnSceneIndex.Contains(level)) {
            if (audios.IsPlaying == false) {
                audios.startPlaying();
            }
        }
        else {
            audios.stopPlaying();
        }
    }
}
