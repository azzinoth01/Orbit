using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weaponholder : MonoBehaviour
{
    private Camera cam;
    public float rotationSpeed;
    public int direction;
    public float currentAngle;

    // Start is called before the first frame update
    void Start() {
        cam = Globals.currentCamera;
    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            Vector3 pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            pos.z = 0;
            Vector2 dir = pos - transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, dir);

            if (angle < currentAngle) {
                direction = -1;
            }


            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
