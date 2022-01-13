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
    private Mouse mouse;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start() {

        mouse = Globals.virtualMouse.VirtualMouseProperty;
        cam = Globals.virtualMouse.canvas.worldCamera;
    }

    /// <summary>
    /// richtet die Waffen nach Mouseposition aus
    /// </summary>
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {


            Vector3 pos = cam.ScreenToWorldPoint(mouse.position.ReadValue());

            pos = cam.WorldToViewportPoint(pos);

            pos = Globals.currentCamera.ViewportToWorldPoint(pos);

            pos.z = 0;
            Vector2 dir = pos - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, dir);



            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);



        }
    }
}
