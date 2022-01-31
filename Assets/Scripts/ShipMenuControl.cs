using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMenuControl : MonoBehaviour, Controlls.IBullet_hellActions
{
    private Controlls controll;

    public void OnDoge(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    public void OnMove_down(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    public void OnMove_left(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    public void OnMove_rigth(InputAction.CallbackContext context) {
        //throw new System.NotImplementedException();
    }

    public void OnMove_up(InputAction.CallbackContext context) {
        //  throw new System.NotImplementedException();
    }

    public void OnPause_menu(InputAction.CallbackContext context) {

        if (context.started) {
            Globals.menuHandler.onClickMainMenu(0);
        }

    }

    public void OnShoot(InputAction.CallbackContext context) {
        // throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start() {
        if (controll == null) {
            controll = new Controlls();

            Rebinding_menu rebind = new Rebinding_menu();
            controll = rebind.loadRebinding(controll);

            controll.bullet_hell.Enable();
            controll.bullet_hell.SetCallbacks(this);



        }
    }


    private void OnDestroy() {
        controll.Dispose();
        controll = null;
    }
}
