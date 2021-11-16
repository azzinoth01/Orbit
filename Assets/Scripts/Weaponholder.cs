using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponholder : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        // Debug.Log(pos);

        //transform.position = pos;

        Vector2 dir = pos - transform.position;

        //Debug.Log(dir);

        float angle = Vector2.SignedAngle(Vector2.right, dir);

        // Debug.Log(angle);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
