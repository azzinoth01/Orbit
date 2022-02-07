using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// class to handle the inventory button events
/// </summary>
public class InventoryButton : MonoBehaviour, IPointerDownHandler
{
    private Inventory_fill inv;
    private Item item;
    private bool locked;
    private GameObject obj;

    public Inventory_fill Inv {
        get {
            return inv;
        }

        set {
            inv = value;
        }
    }

    public Item Item {
        get {
            return item;
        }

        set {
            item = value;
        }
    }

    public bool Locked {
        get {
            return locked;
        }

        set {
            locked = value;
        }
    }

    public GameObject Obj {
        get {
            return obj;
        }

        set {
            obj = value;
        }
    }

    /// <summary>
    /// checks if the button was hold down to allow drag and drop
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData) {

        inv.inventoryButton(item, locked, obj);


    }


}
