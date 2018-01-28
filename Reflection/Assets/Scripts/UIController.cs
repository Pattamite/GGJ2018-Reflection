using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public bool isMenu = false;
    [Header("Player HP")]
    public Text playerHPText;
    [Header("Player Energy")]
    public Slider playerEnergySlider;
    public Color energyReadyColor;
    public Color energyNotReadyColor;
    public Image energySliderFillImage;
    [Header("Other Values")]
    public Text enemyCount;
    [Header("Menu GameObject")]
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    private Player player;
    private GameController gameController;
    

    // Use this for initialization
    void Start () {
        if (!isMenu) {
            player = GameObject.FindObjectOfType<Player>();
            gameController = GameObject.FindObjectOfType<GameController>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!isMenu) {
            UpdatePlayerInfo();
            UpdateOthersInfo();
        }
	}

    private void UpdatePlayerInfo () {
        playerHPText.text = player.GetHP().ToString();

        playerEnergySlider.value = player.GetEnergyRatio();
        if (player.CanAttack()) {
            energySliderFillImage.color = energyReadyColor;
        }
        else {
            energySliderFillImage.color = energyNotReadyColor;
        }
    }

    private void UpdateOthersInfo () {
        enemyCount.text = gameController.enemyCount.ToString();
    }

    public void ActivateUI (string menuName) {
        if(menuName == StaticVar.UI_MENU_PAUSE) {
            pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1.5f, 0, -10);
        }
        else if (menuName == StaticVar.UI_MENU_GAMEOVER) {
            gameOverMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1.5f, 0, -10);
        }

    }

    public void DeactivateUI (string menuName) {
        if (menuName == StaticVar.UI_MENU_PAUSE) {
            pauseMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1.5f, -20, -10);
        }
        else if (menuName == StaticVar.UI_MENU_GAMEOVER) {
            gameOverMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1.5f, -20, -10);
        }
    }
}
