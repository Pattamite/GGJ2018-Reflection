using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLaserBlock : MonoBehaviour {
    public int laserCount;
    public GameObject[] firePositionObject;
    

    private void OnValidate () {
        if (laserCount < 0) {
            laserCount = 0;
        }
        Array.Resize<GameObject>(ref firePositionObject, laserCount);

        if (firePositionObject.Length != laserCount) {
            Array.Resize<GameObject>(ref firePositionObject, laserCount);
        }
    }

    public void HitByLaser () {
        foreach(GameObject laserPointer in firePositionObject) {
            laserPointer.GetComponent<LaserPointer>().Activate();
        }
    }

    public void Restart () {
        foreach (GameObject laserPointer in firePositionObject) {
            laserPointer.GetComponent<LaserPointer>().Deactivate();
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
