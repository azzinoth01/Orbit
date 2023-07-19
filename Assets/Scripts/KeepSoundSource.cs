using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// class to keep the sound playing while a sceene change happens
/// </summary>
public class KeepSoundSource : MonoBehaviour {
    // Start is called before the first frame update


    //public AudioSource audios;
    /// <summary>
    /// where the audio should be played
    /// </summary>
    public List<int> playOnSceneIndex;
    /// <summary>
    /// don't destroy ID
    /// </summary>
    public string dontDestroyID;
    /// <summary>
    /// audio to be played
    /// </summary>
    public LoopSoundControl audios;

    /// <summary>
    /// destroys the object if it already exists in the dontDestroyOnLoad list
    /// else adds it to the list
    /// </summary>
    void Start() {



        if (Globals.dontDestoryOnLoadObjectID.Contains(dontDestroyID)) {

            Destroy(gameObject);
        }
        else {
            SceneManager.sceneLoaded += LevelLoaded;
            DontDestroyOnLoad(gameObject);
            Globals.dontDestoryOnLoadObjectID.Add(dontDestroyID);

            audios.startPlaying();

        }

    }

    //// Update is called once per frame
    ////void Update() {

    ////}

    ///// <summary>
    ///// sceene change event
    ///// checks if the music is allowed to be played in the new sceene or not
    ///// if not it stops the playing
    ///// else it keeps playing
    ///// </summary>
    ///// <param name="level"></param>
    //private void OnLevelWasLoaded(int level) {


    //    if (playOnSceneIndex.Contains(level)) {
    //        if (audios.IsPlaying == false) {
    //            audios.startPlaying();
    //        }
    //    }
    //    else {
    //        audios.stopPlaying();
    //    }
    //}

    public void LevelLoaded(Scene scene, LoadSceneMode mode) {

        if (playOnSceneIndex.Contains(scene.buildIndex)) {
            if (audios.IsPlaying == false) {
                audios.startPlaying();
            }
        }
        else {
            audios.stopPlaying();
        }
    }
}
