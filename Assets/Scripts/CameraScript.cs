using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private void Awake() {
        Globals.currentCamera = gameObject.GetComponent<Camera>();
    }

}
