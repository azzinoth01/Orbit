using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 pos = Globals.virtualMouse.canvas.worldCamera.ScreenToWorldPoint(Globals.virtualMouse.VirtualMouseProperty.position.ReadValue());

        //Debug.Log(pos);
        transform.position = pos;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
    }
}
