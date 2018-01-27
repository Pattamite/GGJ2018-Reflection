using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int maxHP = 1;
    private int currentHP;
    private GameController gameController;

	void Start () {
        currentHP = maxHP;
        gameController = GameObject.FindObjectOfType<GameController>();

        gameController.enemyCount++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHit (int damage) {
        currentHP -= damage;

        if(currentHP <= 0) {
            gameController.enemyCount--;
            Kill();
        }
    }

    public void Kill () {
        Destroy(gameObject);
    }
}
