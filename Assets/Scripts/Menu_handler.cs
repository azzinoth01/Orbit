using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// menu classe um jegliches menu außer main menu zu verwalten
/// </summary>
public class Menu_handler : MonoBehaviour
{

    public GameObject levelSelectUI;
    public GameObject shipMenuUI;
    public GameObject levelFinishedUI;
    public GameObject gameOverUI;
    public GameObject pauseUI;

    public Image bossHpBar;
    public GameObject bossUI;

    public List<Text> playtimeText;
    public List<Text> enemyKilledCounterText;

    public Text score;

    private int currentScore;

    private float playtime;

    public List<Text> moneyEarnedText;
    public List<Text> toalMoneyText;

    public List<Image> selectedButtonImages;

    public List<GameObject> menuList;

    public Text menuName;

    public Toggle tooltipToogle;

    public float Playtime {
        get {
            return playtime;
        }

        set {
            playtime = value;
        }
    }

    public void addScore(int points) {

        if (Globals.waveControler != null) {

        }
        else {
            currentScore = currentScore + points;
            onChangedScore();
        }

    }

    public void onChangedScore() {
        if (Globals.waveControler != null) {
            if (Globals.currentWinCondition != null && score != null) {
                score.text = "Wave: " + Globals.waveControler.CurrentWave + "\r\n" + "Enemies: " + Globals.currentWinCondition.enemysToKill.ToString();
            }

        }
        else {
            score.text = "Score: " + currentScore.ToString();
        }

    }


    /// <summary>
    /// setzt den menuHandler in den Globalen variablen
    /// </summary>
    //private void Awake() {
    //    Globals.menuHandler = this;
    //    Globals.bossHpBar = bossHpBar;
    //    Globals.bossUI = bossUI;
    //    currentScore = 0;


    //    SaveSettings s = SaveSettings.loadSettings();

    //    if (tooltipToogle != null) {
    //        tooltipToogle.isOn = s.IsToogleOn;
    //    }

    //}

    private void OnEnable() {
        Globals.menuHandler = this;
        Globals.bossHpBar = bossHpBar;
        Globals.bossUI = bossUI;
        currentScore = 0;


        SaveSettings s = SaveSettings.loadSettings();

        if (tooltipToogle != null) {
            tooltipToogle.isOn = s.IsToogleOn;
        }
    }

    /// <summary>
    /// ship Editor button gelickt
    /// </summary>
    public void onClickShipEditor(int sceneIndex) {
        //Debug.Log("Durch klicken dieses Buttons haben Sie sich verpflichtet Markus Dullnig mit Süßigkeiten zu füttern");

        SceneManager.LoadScene(sceneIndex);
    }
    public void onClickExitShipEditor(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// ship menu zu level select
    /// </summary>
    public void onClickWorldSelect() {
        shipMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }
    /// <summary>
    /// level select zu ship menu
    /// </summary>
    public void onClickBack() {
        shipMenuUI.SetActive(true);
        levelSelectUI.SetActive(false);
    }

    /// <summary>
    /// zu main menu scene
    /// </summary>
    /// <param name="sceneIndex"> main menu scene index</param>
    public void onClickMainMenu(int sceneIndex) {

        if (Globals.pause == true) {
            setResume();
        }

        SceneManager.LoadScene(sceneIndex);

    }

    /// <summary>
    /// level auswahlt
    /// </summary>
    /// <param name="levelSceneIndex">level scene index</param>
    public void onClickLevelSelect(int levelSceneIndex) {
        SceneManager.LoadScene(levelSceneIndex);
    }

    /// <summary>
    /// next level auswahl
    /// </summary>
    /// <param name="backupSeneIndex"> backup scene index falls das next level nicht gibt</param>
    public void onClickNextLeve(int backupSeneIndex) {

        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings <= nextLevelIndex) {
            levelChanges();
            SceneManager.LoadScene(backupSeneIndex);

        }
        else {
            levelChanges();
            SceneManager.LoadScene(nextLevelIndex);

        }


    }

    /// <summary>
    /// zu ship menu
    /// </summary>
    /// <param name="sceneIndex"> ship menu scene index</param>
    public void onClickShipMenu(int sceneIndex) {
        levelChanges();
        SceneManager.LoadScene(sceneIndex);

    }

    /// <summary>
    /// scene neu laden
    /// </summary>
    public void onClickTryAgain() {

        levelChanges();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    /// <summary>
    /// variablene zurücksetzen, wenn level geändert wird
    /// </summary>
    private void levelChanges() {
        if (Globals.pause == true) {
            Debug.Log("pause stopped");
            setResume();
        }

        Globals.bulletPool.Clear();
        //Debug.Log("clear");
        //Debug.Log(Globals.bulletPool.Count);
        //try {
        try {
            Player p = Globals.player.GetComponent<Player>();
            p.clearControlls();
        }
        catch {

        }

        Globals.infityWaveSpawner.Clear();

    }

    /// <summary>
    /// aktiviert das Pause Menu
    /// </summary>
    public void setPause() {
        Globals.pause = true;
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }
    /// <summary>
    /// deaktiviert das Pause Menu
    /// </summary>
    public void setResume() {
        Globals.pause = false;
        Time.timeScale = 1;
        if (pauseUI != null) {
            pauseUI.SetActive(false);
        }

    }

    /// <summary>
    /// aktiviert das Game Over Menu
    /// </summary>
    public void setGameOver() {
        Globals.pause = false;
        Time.timeScale = 1;
        gameOverUI.SetActive(true);

        PlayerSave s = PlayerSave.loadSettings();

        if (s == null) {
            s = new PlayerSave();
        }

        int moneyEarned = 0;

        moneyEarned = Globals.money - s.Money;

        s.Money = Globals.money;

        foreach (Text t in moneyEarnedText) {
            t.text = "Scraps earned " + moneyEarned.ToString();
        }
        foreach (Text t in toalMoneyText) {
            t.text = "Total Scraps " + Globals.money;
        }



        //Debug.Log(s.Money);
        s.savingSetting();
        //Debug.Log(Globals.money);

        foreach (Text t in playtimeText) {
            t.text = "Playtime: " + playtime.ToString() + " Seconds";
        }
        foreach (Text t in enemyKilledCounterText) {
            t.text = "Enemies killed: " + (currentScore / 100).ToString();
        }
        levelChanges();
    }

    /// <summary>
    /// aktiviert das Level Finished Menu
    /// </summary>
    public void setLevelFinish() {


        levelChanges();
        levelFinishedUI.SetActive(true);

        PlayerSave s = PlayerSave.loadSettings();

        if (s == null) {
            s = new PlayerSave();
        }

        // 3 ist die Tutorial scene
        if (SceneManager.GetActiveScene().buildIndex == 4) {
            s.TutorialPlayed = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 5) {
            s.Level1Played = true;
        }
        int moneyEarned = 0;

        moneyEarned = Globals.money - s.Money;

        s.Money = Globals.money;

        foreach (Text t in moneyEarnedText) {
            t.text = "Scraps earned " + moneyEarned.ToString();
        }
        foreach (Text t in toalMoneyText) {
            t.text = "Total Scraps " + Globals.money;
        }

        //Debug.Log(s.Money);
        s.savingSetting();

        foreach (Text t in playtimeText) {
            t.text = "Playtime: " + playtime.ToString() + " Seconds";
        }
        foreach (Text t in enemyKilledCounterText) {
            int i = currentScore - 1000;

            t.text = "Enemies killed: " + (i / 100).ToString() + " und Bosse killed: 1";
        }

    }

    public void onClickOptions(int index) {
        Globals.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(index);
    }

    public void onClickGoBackToPreviousScene() {
        int index = Globals.lastSceneIndex;
        Globals.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }


    public void onSelectButton(Image button) {
        foreach (Image img in selectedButtonImages) {
            img.enabled = false;

        }

        button.enabled = true;
    }

    public void onClickMenuItem(GameObject game) {
        foreach (GameObject gobj in menuList) {
            gobj.SetActive(false);
        }
        game.SetActive(true);
    }

    public void onClickActivateGameobnect(GameObject game) {
        game.SetActive(true);
    }

    public void onClickSetMenuName(string name) {
        menuName.text = name;
    }

    public void onclickSetToogle(GameObject game) {
        SaveSettings s = SaveSettings.loadSettings();

        if (s.IsToogleOn == true) {
            Globals.tooltip.tooltipToogled = false;
            s.IsToogleOn = false;
            TooltipSystem.Hide();

        }
        else {
            s.IsToogleOn = true;
            Globals.tooltip.tooltipToogled = true;

            TooltipTrigger triger = game.GetComponent<TooltipTrigger>();


            TooltipSystem.Show(triger.content, triger.header);
        }

        Debug.Log("tooltip is " + s.IsToogleOn.ToString());
        s.savingSetting();
    }
    public void onTooltipToogleChanged(Toggle tog) {
        SaveSettings s = SaveSettings.loadSettings();

        s.IsToogleOn = tog.isOn;


        Globals.tooltip.tooltipToogled = tog.isOn;

        s.savingSetting();

        Debug.Log("tooltip is " + tog.isOn.ToString());
    }

}
