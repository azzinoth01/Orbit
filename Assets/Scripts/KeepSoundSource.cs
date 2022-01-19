using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSoundSource : MonoBehaviour
{
    // Start is called before the first frame update


    public AudioSource audios;
    public List<int> playOnSceneIndex;
    public string dontDestroyID;


    void Start() {



        if (Globals.dontDestoryOnLoadObjectID.Contains(dontDestroyID)) {

            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            Globals.dontDestoryOnLoadObjectID.Add(dontDestroyID);

            audios.Play();

        }

    }

    // Update is called once per frame
    //void Update() {

    //}


    private void OnLevelWasLoaded(int level) {


        if (playOnSceneIndex.Contains(level)) {
            if (audios.isPlaying == false) {
                audios.Play();
            }
        }
        else {
            audios.Stop();
        }
    }
}
