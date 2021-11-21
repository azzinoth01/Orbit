using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skillsequenze
{
    [SerializeField] private float delay;
    [SerializeField] private GameObject skill;

    public Skillsequenze(float delay, GameObject skill) {
        this.delay = delay;
        this.skill = skill;
    }

    public GameObject Skill {
        get {
            return skill;
        }


    }

    public float Delay {
        get {
            return delay;
        }


    }
}
