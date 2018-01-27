using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float rotateSpeed = 60f;
    public float rotateAddSpeed = 60f;
    public float minRotate = 10f;
    public float maxRotate = 170f;
    private LaserPointer laserPointer;
	
	void Start () {
        laserPointer = GetComponent<LaserPointer>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        Attack();
    }

    private void Movement () {
        if (Input.GetButton("Fire2")) {
            transform.Rotate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * (rotateSpeed + rotateAddSpeed) * Time.deltaTime * -1));
        }
        else {
            transform.Rotate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime * -1));
        }
        


        transform.eulerAngles = new Vector3(0, 0 , Mathf.Clamp(transform.eulerAngles.z, minRotate, maxRotate));

    }

    private void Attack () {
        if(Input.GetButtonDown("Fire1")) {
            print("Bang !!!");
            laserPointer.Attack();
        }
    }
}
