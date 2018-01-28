using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVEffect : MonoBehaviour {

    public float minLimit;
    public float maxLimit;
    public float speed;
    public bool isMoveWhenPause = true;
    private float currentDirection = -1;
    private float lastTime;
    
	// Use this for initialization
	void Start () {
        lastTime = Time.realtimeSinceStartup;

    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y > maxLimit) {
            currentDirection = -1;
        }
        else if(transform.position.y < minLimit) {
            currentDirection = 1;
        }

        print((Time.realtimeSinceStartup - lastTime));
        if (isMoveWhenPause) {
            transform.position += (Vector3.up * speed * currentDirection * (Time.realtimeSinceStartup - lastTime));
        }
        else {
            transform.Translate(Vector3.up * speed * currentDirection * Time.deltaTime * Time.timeScale);
        }

        lastTime = Time.realtimeSinceStartup;

    }
}
