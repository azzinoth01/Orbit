using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class EditorTest : MonoBehaviour, Controlls.IBullet_hellActions
{
    public bool start;
    public GameObject body;

    private Controlls controll;
    // Start is called before the first frame update
    void Start() {
        if (controll == null) {
            controll = new Controlls();
            controll.Enable();
            controll.bullet_hell.SetCallbacks(this);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Mouse.current.position.ReadValue() != Vector2.zero) {
            Debug.Log("maus");
        }
        if (Mouse.current.leftButton.ReadValue() != 0) {
            Debug.Log("mouse klick: " + Mouse.current.leftButton.ReadValue().ToString());
        }
        //if (start == true) {
        //    body.transform.position = body.transform.position + (Vector3.right * 10);
        //}
    }

    [ContextMenu("start")]
    public void setStart() {

        start = true;
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
        Debug.Log("test");
    }

    public void OnMove_left(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnMove_up(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnMove_down(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnPause(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }
}
