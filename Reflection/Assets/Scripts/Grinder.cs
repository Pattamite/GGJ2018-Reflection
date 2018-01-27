using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour {

    void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.GetComponent<Enemy>()) {
            print("Enemy Enter");
        }
        if(other.gameObject.tag != StaticVar.WALL_TAG) Destroy(other.gameObject);
    }
}
