using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool designMode;
    public List<Vector2> waypoints;
    public float speed;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {

        }
    }


    [ContextMenu("Set Waypoints on Screen")]
    public void paintWayoints() {


    }
}
