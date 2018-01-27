using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int maxHP = 1;

    private int currentHP;
	
	void Start () {
        currentHP = maxHP;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetHit (int damage) {
        currentHP -= damage;

        if(currentHP <= 0) {
            Kill();
        }
    }

    public void Kill () {
        Destroy(gameObject);
    }
}
