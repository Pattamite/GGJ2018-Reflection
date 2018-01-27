using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLaserBlock : MonoBehaviour {
    public GameObject attackLaser;
    public int laserCount;
    public GameObject[] firePositionObject;

    private float lastAttack;



    private void OnValidate () {
        if (laserCount < 0) {
            laserCount = 0;
        }
        Array.Resize<GameObject>(ref firePositionObject, laserCount);

        if (firePositionObject.Length != laserCount) {
            Array.Resize<GameObject>(ref firePositionObject, laserCount);
        }
    }

    public void HitByAttack () {
        if(Time.time - lastAttack >= StaticVar.ATTACK_DELAY) {
            foreach (GameObject gun in firePositionObject) {
                GameObject newAttack = Instantiate(attackLaser);
                Vector2 firePosition = gun.transform.position;
                float fireAngle = gun.transform.eulerAngles.z;
                float xFireVector = Mathf.Cos(fireAngle / 180f * Mathf.PI);
                float yFireVector = Mathf.Sin(fireAngle / 180f * Mathf.PI);
                Vector2 fireDirection = new Vector2(xFireVector, yFireVector);

                newAttack.GetComponent<AttackLaser>().Attack(firePosition, fireDirection);
            }

            lastAttack = Time.time;
        }
    }

    public void HitByLaser () {
        foreach (GameObject gun in firePositionObject) {
            Vector2 firePosition = gun.transform.position;
            float fireAngle = gun.transform.eulerAngles.z;
            float xFireVector = Mathf.Cos(fireAngle / 180f * Mathf.PI);
            float yFireVector = Mathf.Sin(fireAngle / 180f * Mathf.PI);
            Vector2 fireDirection = new Vector2(xFireVector, yFireVector);

            gun.GetComponent<LaserPointer>().DrawLaser(firePosition, fireDirection);
        }
    }

    public void Restart () {
        
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
