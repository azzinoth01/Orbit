using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// container classe um skills zu beschreiben
/// </summary>
[Serializable]
public class Skillsequenze
{
    [SerializeField] private float delay;
    [SerializeField] private GameObject skill;

    /// <summary>
    /// construktor classe
    /// </summary>
    /// <param name="delay"> delay zwischen den skills</param>
    /// <param name="skill"> den skill der benutzt wird</param>
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
