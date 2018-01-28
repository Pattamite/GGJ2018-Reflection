using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Boss : MonoBehaviour {

    public int maxHP = 10;
    public int attackDamage = 1;
    private int currentHP;
    private GameController gameController;

    public float RotateSpeed = 5f;
    public float Radius = 0.1f;
    private Vector3 _centre;
    private float _angle;

    public GameObject[] enemy;
    public float spawnY;
    public float spawnXMin;
    public float spawnXMax;
    public float spawnTime;
    public Transform spawnParent;

    private float lastSpawnTime;
    public Slider slider;
    public GameObject bossAvatar;

    void Start () {
        _centre = transform.position;
        currentHP = maxHP;
        gameController = GameObject.FindObjectOfType<GameController>();

        gameController.enemyCount++;
        lastSpawnTime = 0f;
    }

    // Update is called once per frame
    void Update () {
        _angle += RotateSpeed * Time.deltaTime * Time.timeScale;
        Vector3 offset = new Vector3(Mathf.Sin(_angle) * Radius, Mathf.Cos(_angle) * Radius, 0);
        transform.position = _centre + offset;
        CheckSpawn();
        slider.value = GetHPRatio();
    }

    private void CheckSpawn () {
        if(Time.time - lastSpawnTime > spawnTime) {
            int randomEnemy = Random.Range(0, enemy.Length);
            Instantiate(enemy[randomEnemy], new Vector3(Random.Range(spawnXMin, spawnXMax), spawnY, -4), Quaternion.identity, spawnParent);

            lastSpawnTime = Time.time;
        }
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
        gameController.enemyCount--;
        Destroy(bossAvatar);
        Destroy(slider);
        Destroy(gameObject);
        
    }

    public float GetHPRatio () {
        return (float)currentHP / (float)maxHP;
    }
}
