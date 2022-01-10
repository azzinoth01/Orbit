using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static GameObject bossUI;
    public static Image bossHpBar;
    public static VirtualMouse virtualMouse;


}
