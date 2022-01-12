using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private string iD;
    [SerializeField] private string ItemName;
    [SerializeField] private int value;
    [SerializeField] private string icon;
    [SerializeField] private string patternIcon;
    [SerializeField] private string sprite;

    public string ID {
        get {
            return iD;
        }

        set {
            iD = value;
        }
    }

    public string Name {
        get {
            return ItemName;
        }

        set {
            ItemName = value;
        }
    }

    public int Value {
        get {
            return value;
        }

        set {
            this.value = value;
        }
    }

    public string Icon {
        get {
            return icon;
        }

        set {
            icon = value;
        }
    }

    public string Sprite {
        get {
            return sprite;
        }

        set {
            sprite = value;
        }
    }

    public string PatternIcon {
        get {
            return patternIcon;
        }

        set {
            patternIcon = value;
        }
    }
}
