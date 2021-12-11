using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// classe um Globale Variablen zu verwalten
/// </summary>
public static class Globals
{
    public static bool pause;
    public static Camera currentCamera;
    public static GameObject player;
    public static List<Skill> bulletPool;
    public static Menu_handler menuHandler;
    public static Win_condition currentWinCondition;
    public static List<Enemy_Spawner> spawnerListe;
    public static List<ParticleSystem> effectPool;

}
