using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAdapter : MonoBehaviour {
    MultiLaserBlock multiLaserBlock;

	// Use this for initialization
	void Start () {
        multiLaserBlock = GetComponent<MultiLaserBlock>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitByLaser () {
        if (multiLaserBlock) multiLaserBlock.HitByLaser();
    }
}
