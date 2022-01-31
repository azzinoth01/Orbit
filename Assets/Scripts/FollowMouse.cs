using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    private bool mouseIsPressed;

    public List<RemoveSlotItem> slotMouseoverCheck;
    public Inventory_fill inv;

    public bool MouseIsPressed {
        get {
            return mouseIsPressed;
        }

        set {
            mouseIsPressed = value;
        }
    }

    // Start is called before the first frame update
    void Start() {
        mouseIsPressed = false;
    }

    // Update is called once per frame
    void Update() {

        Vector3 pos = Globals.virtualMouse.canvas.worldCamera.ScreenToWorldPoint(Globals.virtualMouse.VirtualMouseProperty.position.ReadValue());

        //Debug.Log(pos);
        transform.position = pos;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);


        if (Globals.virtualMouse.VirtualMouseProperty.leftButton.wasReleasedThisFrame && mouseIsPressed == true) {
            mouseIsPressed = false;
            foreach (RemoveSlotItem slot in slotMouseoverCheck) {
                if (slot.IsMouseOver == true) {
                    if (slot.isMainWeapon == true) {
                        inv.mainWeaponSlotClicked(slot.Image);
                    }
                    if (slot.isSecondaryWeapon == true) {
                        inv.secondaryWeaponSlotClicked(slot.Image);
                    }
                    if (slot.isSecondaryWeapon1 == true) {
                        inv.secondaryWeaponSlotTwoClicked(slot.Image);
                    }
                    if (slot.isShipPart == true) {
                        inv.shieldSlotClicked(slot.Image);
                    }

                }
            }
        }
    }
}
