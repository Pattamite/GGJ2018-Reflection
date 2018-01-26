using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserPointer : MonoBehaviour {

    public GameObject firePositionObject;
    public GameObject lastHitObject;
    public float lineLength;
    public int maxReflectCount = 100;

    private LineRenderer lineRenderer;
    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 firePosition = firePositionObject.transform.position;
        float fireAngle = transform.eulerAngles.z;
        float xFireVector = Mathf.Cos(fireAngle / 180f * Mathf.PI);
        float yFireVector = Mathf.Sin(fireAngle / 180f * Mathf.PI);
        Vector2 fireDirection = new Vector2(xFireVector, yFireVector);

        DrawLaser(firePosition, fireDirection);
    }

    private void DrawLaser (Vector2 firePosition, Vector2 fireDirection) {
        ResetLineTenderer();
        AddPositionToLineRenderer(firePosition);
        bool isLineEnd = false;
        lastHitObject = null;

        Vector2 currentPosition = new Vector2(firePosition.x, firePosition.y);
        Vector2 currentDirection = new Vector2(fireDirection.x, fireDirection.y);

        RaycastHit2D objectHitData = Physics2D.Raycast(currentPosition, currentDirection, lineLength, 1 << StaticVar.LAYER_BLOCK);

        while (objectHitData && lineRenderer.positionCount <= maxReflectCount) {
            DetectEnemy(currentPosition, currentDirection, objectHitData.distance);
            lastHitObject = objectHitData.collider.gameObject;

            AddPositionToLineRenderer(new Vector3(objectHitData.point.x, objectHitData.point.y,
                                   firePositionObject.transform.position.z));

            if (objectHitData.collider.gameObject.tag == StaticVar.BLOCK_TAG_REFLECTIVE) {
                Vector2 reflectDirection = Vector2.Reflect(currentDirection, objectHitData.normal);
                //print(objectHit.collider.gameObject.ToString() + " " + reflectDirection.ToString());

                currentPosition = new Vector2(objectHitData.point.x, objectHitData.point.y);
                currentDirection = reflectDirection;
            }
            else {
                isLineEnd = true;
                break;
            }

            lastHitObject.GetComponent<Collider2D>().enabled = false;
            objectHitData = Physics2D.Raycast(currentPosition, currentDirection, lineLength, 1 << StaticVar.LAYER_BLOCK);
            lastHitObject.GetComponent<Collider2D>().enabled = true;
        }

        if (!isLineEnd) {
            DetectEnemy(currentPosition, currentDirection, lineLength);
            AddPositionToLineRenderer(new Vector3(currentPosition.x + (currentDirection.x * lineLength),
                                       currentPosition.y + (currentDirection.y * lineLength),
                                       firePositionObject.transform.position.z));
        }
    }

    private void AddPositionToLineRenderer(Vector3 position) {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    private void ResetLineTenderer () {
        lineRenderer.positionCount = 0;
    }

    private void DetectEnemy (Vector2 position, Vector2 direction, float range) {
        GameObject lastEnemyHit = null;
        RaycastHit2D enemyHitData = Physics2D.Raycast(position, direction, range, 1 << StaticVar.LAYER_ENEMY);

        while (enemyHitData) {
            lastEnemyHit = enemyHitData.collider.gameObject;
            print(lastEnemyHit.ToString());
            range -= enemyHitData.distance;

            lastEnemyHit.GetComponent<Collider2D>().enabled = false;
            enemyHitData = Physics2D.Raycast(enemyHitData.point, direction, range, 1 << StaticVar.LAYER_ENEMY);
            lastEnemyHit.GetComponent<Collider2D>().enabled = true;
        }
    }
}
