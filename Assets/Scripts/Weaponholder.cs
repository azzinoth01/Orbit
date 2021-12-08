using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// classe um die Waffen nach Mouseposition ausrichtet
/// </summary>
public class Weaponholder : MonoBehaviour
{
    private Camera cam;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start() {
        cam = Globals.currentCamera;

    }

    /// <summary>
    /// richtet die Waffen nach Mouseposition aus
    /// </summary>
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = 0;
            Vector2 dir = pos - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, dir);



            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);



        }
    }
}
