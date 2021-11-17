using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Controlls.IBullet_hellActions
{

    public Rigidbody2D body;

    public float force;
    public float maxSpeed;
    public Pause_handler pause_handler;
    private Vector2 impulse;

    private Controlls controll;


    public void OnMove_down(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.down * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.down * force);
        }

    }

    public void OnMove_left(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.left * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.left * force);
        }
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.right * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.right * force);
        }
    }

    public void OnMove_up(InputAction.CallbackContext context) {

        if (context.started) {
            impulse = impulse + (Vector2.up * force);
        }
        else if (context.canceled) {
            impulse = impulse - (Vector2.up * force);
        }
    }

    void Controlls.IBullet_hellActions.OnPause(InputAction.CallbackContext context) {
        if (Globals.pause == true) {
            pause_handler.setResume();
        }
        else {
            pause_handler.setPause();
        }

    }

    // Start is called before the first frame update
    void Start() {
        if (controll == null) {
            controll = new Controlls();
            controll.Enable();
            controll.bullet_hell.SetCallbacks(this);
        }
        impulse = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update() {
        if (Globals.pause == true) {
            return;
        }
        else {
            //Debug.Log(impulse);

            //Debug.Log(impulse.normalized * force);
            body.AddForce(impulse.normalized * force, ForceMode2D.Impulse);
            Vector2 normalizedSpeed = body.velocity.normalized * maxSpeed;
            normalizedSpeed.x = Mathf.Abs(normalizedSpeed.x);
            normalizedSpeed.y = Mathf.Abs(normalizedSpeed.y);

            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -normalizedSpeed.x, normalizedSpeed.x), Mathf.Clamp(body.velocity.y, -normalizedSpeed.y, normalizedSpeed.y));
        }

    }
}
