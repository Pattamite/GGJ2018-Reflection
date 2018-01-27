using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float rotateSpeed = 60f;
    public float rotateAddSpeed = 60f;
    public float minRotate = 10f;
    public float maxRotate = 170f;
    public int maxHP = 5;
    public float maxEnergy = 100f;
    public float energyPerUse = 20f;
    public float energyRegenRate = 20f;

    private LaserPointer laserPointer;
    private int currentHP;
    private float currentEnergy;
    private
	
	void Start () {
        laserPointer = GetComponent<LaserPointer>();
        currentHP = maxHP;
        currentEnergy = maxEnergy;
    }
	
	// Update is called once per frame
	void Update () {
        Energy();
        Movement();
        Attack();
    }

    public void GetHit(int damage) {
        currentHP -= damage;
        if(currentHP <= 0) {
            //game over
        }
    }

    private void Energy () {
        currentEnergy = Mathf.Clamp(currentEnergy + (energyRegenRate * Time.deltaTime * Time.timeScale), 0, maxEnergy);
    }

    private void Movement () {
        if (Input.GetButton("Fire2")) {
            transform.Rotate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * (rotateSpeed + rotateAddSpeed) * Time.deltaTime * -1 * Time.timeScale));
        }
        else {
            transform.Rotate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * rotateSpeed * Time.deltaTime * -1 * Time.timeScale));
        }
        transform.eulerAngles = new Vector3(0, 0 , Mathf.Clamp(transform.eulerAngles.z, minRotate, maxRotate));
    }

    private void Attack () {
        if(Input.GetButtonDown("Fire1") && currentEnergy >= energyPerUse && Time.timeScale != 0) {
            if (laserPointer.Attack()) {
                currentEnergy -= energyPerUse;
            }
        }
    }

    public float GetEnergyRatio () {
        return currentEnergy / maxEnergy;
    }

    public bool CanAttack () {
        return currentEnergy >= energyPerUse;
    }

    public int GetHP () {
        return currentHP;
    }
}
