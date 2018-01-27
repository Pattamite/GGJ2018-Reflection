using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAdapter : MonoBehaviour {
    MultiLaserBlock multiLaserBlock;
    private bool laserActive;
    private bool attackActive;
    public bool isPreview;

	// Use this for initialization
	void Start () {
        multiLaserBlock = GetComponent<MultiLaserBlock>();
        laserActive = false;
        attackActive = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(isPreview) ActivateLaser();
        ActivateAttack();
    }

    public void ActivateLaser () {
        if (laserActive) {
            if (multiLaserBlock) multiLaserBlock.HitByLaser();
        }
    }

    public void HitByLaser () {
        laserActive = true;
    }

    public void NotHitbyLaser () {
        laserActive = false;
    }

    public void ActivateAttack () {
        if (attackActive) {
            if (multiLaserBlock) multiLaserBlock.HitByAttack();
            attackActive = false;
        }
    }

    public void HitByAttack () {
        attackActive = true;
    }

    public void NotHitByAttack () {
        attackActive = false;
    }
}
