using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// limitiert die Framerate auf 0 fps
/// </summary>
public class LimitFrameRate : MonoBehaviour
{
    /// <summary>
    /// limitiert die Framerate auf 60 fps
    /// </summary>
    private void Awake() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

}
