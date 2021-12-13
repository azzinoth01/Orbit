using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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





    /// <summary>
    /// setzt den menuHandler in den Globalen variablen
    /// </summary>
    private void Awake() {
        Globals.menuHandler = this;
    }

    /// <summary>
    /// ship Editor button gelickt
    /// </summary>
    public void onClickShipEditor() {
        Debug.Log("Durch klicken dieses Buttons haben Sie sich verpflichtet Markus Dullnig mit Süßigkeiten zu füttern");
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
        Debug.Log("clear");
        Debug.Log(Globals.bulletPool.Count);
        //try {
        try {
            Player p = Globals.player.GetComponent<Player>();
            p.clearControlls();
        }
        catch {

        }

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
        pauseUI.SetActive(false);
    }

    /// <summary>
    /// aktiviert das Game Over Menu
    /// </summary>
    public void setGameOver() {
        Globals.pause = false;
        Time.timeScale = 1;
        gameOverUI.SetActive(true);
        levelChanges();
    }

    /// <summary>
    /// aktiviert das Level Finished Menu
    /// </summary>
    public void setLevelFinish() {
        levelChanges();
        levelFinishedUI.SetActive(true);

    }
}
