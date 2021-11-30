using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Rigidbody2D body;
    private float playerMaxSpeed;


    private void Awake() {
        Globals.currentCamera = gameObject.GetComponent<Camera>();
    }

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        playerMaxSpeed = Globals.player.GetComponent<Player>().maxSpeed * 2;

    }


    private void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            //Debug.Log((Globals.player.transform.position - transform.position));
            Vector3 direction = (Globals.player.transform.position - transform.position) * playerMaxSpeed;
            body.velocity = direction;

        }
    }

}
