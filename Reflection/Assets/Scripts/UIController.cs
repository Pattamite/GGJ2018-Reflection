using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [Header("Player HP")]
    public Text playerHPText;
    [Header("Player Energy")]
    public Slider playerEnergySlider;
    public Color energyReadyColor;
    public Color energyNotReadyColor;
    public Image energySliderFillImage;
    [Header("Others")]
    public Text enemyCount;

    private Player player;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        gameController = GameObject.FindObjectOfType<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerInfo();
        UpdateOthersInfo();
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
}
