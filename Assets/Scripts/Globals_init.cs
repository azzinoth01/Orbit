using nn.account;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// class to initialise the globale variables
/// </summary>
public class Globals_init : MonoBehaviour {
    /// <summary>
    /// money icon
    /// </summary>
    public Sprite moneyIcon;
    /// <summary>
    /// money drop prefab
    /// </summary>
    public GameObject moneyDropPrefrab;
    /// <summary>
    /// enemy hit sound
    /// </summary>
    public AudioSource tempEnemyHit;

    public ItemCatalogSave itemCatalogText;

    /// <summary>
    /// initialise the globale variables with standard values
    /// </summary>
    private void Awake() {

#if UNITY_SWITCH
        nn.account.Account.Initialize();
        nn.Result result;
        if (nn.account.Account.TryOpenPreselectedUser(ref Globals.userHandle)) {
            // Get the user ID of the preselected user account.
            result = nn.account.Account.GetUserId(ref Globals.userId, Globals.userHandle);
        }
        else {
            // This should not be possible in retail
            nn.Nn.Abort("TryOpenPreselectedUser failed");
        }

        Debug.Log("Mounting save data archive");
        result = nn.fs.SaveData.Mount("OrbitSaveFiles", Globals.userId);
#endif

        Globals.itemCatalogSave = itemCatalogText;
        Globals.pause = false;

        if (Globals.bulletPool == null) {
            Globals.bulletPool = new List<Skill>();
        }
        if (Globals.spawnerListe == null) {
            Globals.spawnerListe = new List<Enemy_Spawner>();
        }


        Globals.moneyIcon = moneyIcon;
        Globals.moneyDrop = moneyDropPrefrab;

        PlayerSave save = PlayerSave.loadSettings();
        if (save == null) {
            save = new PlayerSave();
            Debug.Log("new palyer save");
        }
        Debug.Log("debug check");
        Debug.LogErrorFormat("debugCheck {0}", "check");

        Globals.money = save.Money;

        //Globals.catalog = ItemCatalog.loadSettingsText(itemCatalogText.ItemList);

        ItemCatalog cat = new ItemCatalog();

        foreach (WeaponInfo info in itemCatalogText.WeaponInfo) {
            cat.ItemList.Add(info);
        }
        foreach (Parts info in itemCatalogText.ParstInfos) {
            cat.ItemList.Add(info);
        }
        Globals.catalog = cat;

        //  Debug.LogError("settings loaded");

        Globals.tempEnemyHit = tempEnemyHit;

        if (Globals.dontDestoryOnLoadObjectID == null) {
            Globals.dontDestoryOnLoadObjectID = new List<string>();
        }

        if (Globals.infityWaveSpawner == null) {
            Globals.infityWaveSpawner = new List<Enemy_Spawner>();
        }
        Debug.Log("init finished");
    }

}
