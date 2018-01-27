using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    private void Movement () {
        transform.Translate(Vector3.down * speed * Time.deltaTime * Time.timeScale);
    }
}
