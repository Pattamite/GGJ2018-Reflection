using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int maxHP = 1;
    public int attackDamage = 1;
    private int currentHP;
    private GameController gameController;
    public float additionSpeedVertical = 0f;
    public float additionSpeedHorizontal = 0f;
    public float minHorizontal;
    public float maxHorizontal;
    public float currentHorizontalDirection = 1f;


    void Start () {
        currentHP = maxHP;
        gameController = GameObject.FindObjectOfType<GameController>();

        gameController.enemyCount++;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement () {
        transform.Translate(Vector3.down * additionSpeedVertical * Time.deltaTime * Time.timeScale);

        if(transform.localPosition.x < minHorizontal) {
            currentHorizontalDirection = 1f;
        }
        if (transform.localPosition.x > maxHorizontal) {
            currentHorizontalDirection = -1f;
        }

        transform.Translate(Vector3.right * currentHorizontalDirection *  additionSpeedHorizontal * Time.deltaTime * Time.timeScale);
    }

    public void GetHit (int damage) {
        currentHP -= damage;

        if(currentHP <= 0) {
            Kill();
        }
    }

    public bool IsAlive () {
        return currentHP > 0;
    }

    public void Kill () {
        Destroy(gameObject);
        gameController.enemyCount--;
    }
}
