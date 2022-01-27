using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void OnPointerDown(PointerEventData eventData) {

        inv.inventoryButton(item, locked, obj);


    }


}
