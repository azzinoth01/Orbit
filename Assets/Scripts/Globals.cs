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
    public static int money;
    public static List<Inventar_Object> inventory;
    public static Sprite moneyIcon;
    public static GameObject moneyDrop;
    public static ItemCatalog catalog;
    public static Item currentItem;
    public static AudioSource tempEnemyHit;
    public static List<string> dontDestoryOnLoadObjectID;
    public static int lastSceneIndex;
    public static List<Enemy_Spawner> infityWaveSpawner;
    public static WaveControler waveControler;
    public static ToolTip tooltip;
    public static bool skipStartCutscene;

}
