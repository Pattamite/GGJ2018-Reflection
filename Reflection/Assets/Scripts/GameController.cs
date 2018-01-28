using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int enemyCount = 0;

    private float delay = 5f;
    private int currentState;
    public AudioSource audioSource;
    public AudioClip MainGameSong;
    public AudioClip GameOverSong;
    public int nextSceneWhenWin;

    UIController uiController;

    // Use this for initialization
    void Start () {
        uiController = GameObject.FindObjectOfType<UIController>();
        audioSource.clip = MainGameSong;
        audioSource.Play();
        currentState = StaticVar.STATE_GAME_PLAYING;
    }

    // Update is called once per frame
    void Update () {
        if (!uiController.isMenu) {
            TogglePauseByKey();
            if(delay > 0) {
                delay -= Time.deltaTime;
            }
            else {
                CheckEnemyCount();
            }
        }
    }

    private void CheckEnemyCount () {
        if (enemyCount <= 0) {
            LoadLevel(nextSceneWhenWin);
        }
    }

    private void TogglePauseByKey() {
        if (Input.GetButtonDown("Cancel") && currentState != StaticVar.STATE_GAME_OVER) {
            if(currentState == StaticVar.STATE_GAME_PLAYING) {
                GamePause();
            }
            else if(currentState == StaticVar.STATE_GAME_PAUSE) {
                GameResume();
            }
        }
    }

    public void GamePause () {
        audioSource.Pause();
        currentState = StaticVar.STATE_GAME_PAUSE;
        uiController.ActivateUI(StaticVar.UI_MENU_PAUSE);
        Time.timeScale = 0;
    }

    public void GameResume () {
        audioSource.Play();
        currentState = StaticVar.STATE_GAME_PLAYING;
        uiController.DeactivateUI(StaticVar.UI_MENU_PAUSE);
        Time.timeScale = 1;
    }

    public void GameOver () {
        audioSource.Stop();
        audioSource.clip = GameOverSong;
        audioSource.Play();
        currentState = StaticVar.STATE_GAME_OVER;
        uiController.DeactivateUI(StaticVar.UI_MENU_PAUSE);
        uiController.ActivateUI(StaticVar.UI_MENU_GAMEOVER);
        Time.timeScale = 0;
    }

    public void LoadLevel (int sceneNumber) {
        if(audioSource) audioSource.Stop();
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame () {
        Application.Quit();
    }
}
