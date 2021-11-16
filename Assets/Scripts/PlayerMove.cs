using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float SpeedY;
    public float SpeedX;
    public Rigidbody2D body;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            body.velocity = new Vector2(body.velocity.x, SpeedY);
        }
        else if (Input.GetKeyUp(KeyCode.W)) {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            body.velocity = new Vector2(-SpeedX, body.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.S)) {
            body.velocity = new Vector2(body.velocity.x, -SpeedY);
        }
        else if (Input.GetKeyUp(KeyCode.S)) {
            body.velocity = new Vector2(body.velocity.x, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            body.velocity = new Vector2(SpeedX, body.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.D)) {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }
}
