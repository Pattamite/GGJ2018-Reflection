using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour {

    public GameObject attackLaser;
    public GameObject firePositionObject;
    public float lineLength = 100;
    public int maxReflectCount = 100;
    public bool isMainLaser = false;
    public Color normalColor;
    public float normalSize = 0.05f;
    public float fireTime = 0.5f;

    private float lastFireTime = 0.0f;
    private bool isFiring = false;
    private LineRenderer lineRenderer;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = normalColor;
        lineRenderer.endColor = normalColor;
        Material whiteDiffuseMat = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.material = whiteDiffuseMat;
    }
	
	// Update is called once per frame
	void Update () {
        ResetLineRenderer();

        if (isMainLaser) {
            Vector2 firePosition = firePositionObject.transform.position;
            float fireAngle = firePositionObject.transform.eulerAngles.z;
            float xFireVector = Mathf.Cos(fireAngle / 180f * Mathf.PI);
            float yFireVector = Mathf.Sin(fireAngle / 180f * Mathf.PI);
            Vector2 fireDirection = new Vector2(xFireVector, yFireVector);
            DrawLaser(firePosition, fireDirection);
        }

        updateAttack();
    }

    private void updateAttack () {
        if (isFiring) {
            if(Time.time - lastFireTime >= fireTime) {
                isFiring = false;
            }
        }
    }

    public bool Attack () {
        if (!isFiring) {
            isFiring = true;
            lastFireTime = Time.time;
            Vector2 firePosition = firePositionObject.transform.position;
            float fireAngle = firePositionObject.transform.eulerAngles.z;
            float xFireVector = Mathf.Cos(fireAngle / 180f * Mathf.PI);
            float yFireVector = Mathf.Sin(fireAngle / 180f * Mathf.PI);
            Vector2 fireDirection = new Vector2(xFireVector, yFireVector);

            GameObject newAttack = Instantiate(attackLaser);
            newAttack.GetComponent<AttackLaser>().Attack(firePosition, fireDirection);
            return true;
        }
        else return false;
    }

    public void DrawLaser (Vector2 firePosition, Vector2 fireDirection) {
        ResetLineRenderer();
        AddPositionToLineRenderer(firePosition);
        bool isLineEnd = false;
        GameObject lastHitObject = null;

        Vector2 currentPosition = new Vector2(firePosition.x, firePosition.y);
        Vector2 currentDirection = new Vector2(fireDirection.x, fireDirection.y);

        RaycastHit2D objectHitData = Physics2D.Raycast(currentPosition, currentDirection, lineLength, 1 << StaticVar.LAYER_BLOCK);

        foreach(BlockAdapter gameObject in GameObject.FindObjectsOfType<BlockAdapter>()) {
            gameObject.NotHitbyLaser();
        }

        while (objectHitData && lineRenderer.positionCount <= maxReflectCount && !isLineEnd) {
            lastHitObject = objectHitData.collider.gameObject;

            BlockAdapter blockAdapter = lastHitObject.GetComponent<BlockAdapter>();
            if (blockAdapter) {
                foreach(BlockAdapter ba in GameObject.FindObjectsOfType<BlockAdapter>()) {
                    ba.NotHitbyLaser();
                }
                if(isMainLaser) blockAdapter.HitByLaser();
            }

            AddPositionToLineRenderer(new Vector3(objectHitData.point.x, objectHitData.point.y));

            if (objectHitData.collider.gameObject.tag == StaticVar.BLOCK_TAG_REFLECTIVE && isMainLaser) {
                Vector2 reflectDirection = Vector2.Reflect(currentDirection, objectHitData.normal);
                //print(objectHit.collider.gameObject.ToString() + " " + reflectDirection.ToString());

                currentPosition = new Vector2(objectHitData.point.x, objectHitData.point.y);
                currentDirection = reflectDirection;
            }
            else {
                isLineEnd = true;
                break;
            }

            //lastHitObject.GetComponent<Collider2D>().enabled = false;
            objectHitData = Physics2D.Raycast(currentPosition + objectHitData.normal * 0.01f, currentDirection, lineLength, 1 << StaticVar.LAYER_BLOCK);
            //lastHitObject.GetComponent<Collider2D>().enabled = true;

        }

        if (!isLineEnd) {
            AddPositionToLineRenderer(new Vector3(currentPosition.x + (currentDirection.x * lineLength),
                                       currentPosition.y + (currentDirection.y * lineLength)));
        }
    }

    private void AddPositionToLineRenderer(Vector2 position) {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(position.x, position.y, -5));
    }

    private void ResetLineRenderer () {
        lineRenderer.positionCount = 0;
    }
}
