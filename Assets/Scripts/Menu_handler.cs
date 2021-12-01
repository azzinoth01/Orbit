using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_handler : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public GameObject levelSelectUI;
    public GameObject shipMenuUI;

    public void onClickShipEditor() {
        Debug.Log("Durch klicken dieses Buttons haben Sie sich verpflichtet Markus Dullnig mit Süßigkeiten zu füttern");
    }

    public void onClickWorldSelect() {
        shipMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }

    public void onClickBack() {
        shipMenuUI.SetActive(true);
        levelSelectUI.SetActive(false);
    }

    public void onClickMainMenu(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);

    }

    public void onClickLevelSelect(int levelSceneIndex) {
        SceneManager.LoadScene(levelSceneIndex);
    }

}
