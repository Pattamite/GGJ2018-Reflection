using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour {
    Player player;

    void Start () {
        player = GameObject.FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D (Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy) {
            if (enemy.IsAlive()) {
                player.GetHit(enemy.attackDamage);
            }
            enemy.Kill();
        }
        if(other.gameObject.tag != StaticVar.WALL_TAG) Destroy(other.gameObject);
    }
}
