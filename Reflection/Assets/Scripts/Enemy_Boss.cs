using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : MonoBehaviour {

    public int maxHP = 10;
    public int attackDamage = 1;
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

        if (currentHP <= 0) {
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
