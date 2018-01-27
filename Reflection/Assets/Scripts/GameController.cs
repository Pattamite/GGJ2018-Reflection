using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int enemyCount = 0;

    UIController uiController;

    // Use this for initialization
    void Start () {
        uiController = GameObject.FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update () {
        if (!uiController.isMenu) {
            TogglePauseByKey();
            CheckEnemyCount();
        }
    }

    private void CheckEnemyCount () {
        if (enemyCount <= 0) {
            print("Stage Clear");
            //
        }
    }

    private void TogglePauseByKey() {
        if (Input.GetButtonDown("Cancel")) {
            if(Time.timeScale == 1) {
                GamePause();
            }
            else {
                GameResume();
            }
        }
    }

    public void GamePause () {
        uiController.ActivateUI(StaticVar.UI_MENU_PAUSE);
        Time.timeScale = 0;
    }

    public void GameResume () {
        uiController.DeactivateUI(StaticVar.UI_MENU_PAUSE);
        Time.timeScale = 1;
    }

    public void GameOver () {
        uiController.ActivateUI(StaticVar.UI_MENU_GAMEOVER);
        Time.timeScale = 0;
    }

    public void LoadLevel (int sceneNumber) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }
}
