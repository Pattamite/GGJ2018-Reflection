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

        drawLaser(firePosition, fireDirection);
    }

    private void drawLaser(Vector2 firePosition, Vector2 fireDirection) {
        int laserVertexCount = 1;
        Vector3[] positions = new Vector3[maxReflectCount];
        positions[0] = firePosition;
        bool isLineEnd = false;
        lastHitObject = null;

        Vector2 currentPosition = new Vector2(firePosition.x, firePosition.y);
        Vector2 currentDirection = new Vector2(fireDirection.x, fireDirection.y);

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentDirection);

        while (hit && laserVertexCount < maxReflectCount) {
            lastHitObject = hit.collider.gameObject;
            

            positions[laserVertexCount] = new Vector3(hit.point.x, hit.point.y,
                                   firePositionObject.transform.position.z);
            laserVertexCount++;

            if (hit.collider.gameObject.tag == StaticVar.BLOCK_TAG_REFLECTIVE) {
                Vector2 reflectDirection = Vector2.Reflect(currentDirection, hit.normal);
                print(hit.collider.gameObject.ToString() + " " + reflectDirection.ToString());

                currentPosition = new Vector2(hit.point.x, hit.point.y);
                currentDirection = reflectDirection;
            }
            else {
                isLineEnd = true;
                break;
            }
            lastHitObject.GetComponent<Collider2D>().enabled = false;
            hit = Physics2D.Raycast(currentPosition, currentDirection);
            lastHitObject.GetComponent<Collider2D>().enabled = true;

        }

        if (!isLineEnd) {
            positions[laserVertexCount] = new Vector3(currentPosition.x + (currentDirection.x * lineLength),
                                       currentPosition.y + (currentDirection.y * lineLength),
                                       firePositionObject.transform.position.z);
            laserVertexCount++;
        }

        lineRenderer.positionCount = laserVertexCount;
        lineRenderer.SetPositions(positions);


        /*int laserVertexCount = 1;
        Vector3[] positions = new Vector3[maxReflecCount];
        positions[0] = firePosition;

        if (Physics2D.Raycast(firePosition, fireDirection)) {
            RaycastHit2D hit = Physics2D.Raycast(firePosition, fireDirection);
            if(hit.collider.gameObject.tag == StaticVar.BLOCK_TAG_REFLECTIVE) {
                positions[laserVertexCount] = new Vector3(hit.point.x, hit.point.y,
                                   firePositionObject.transform.position.z);
                laserVertexCount++;

                Vector2 reflectDirection = Vector2.Reflect(firePosition, hit.normal);
                positions[laserVertexCount] = new Vector3(hit.point.x + (reflectDirection.x * lineLength),
                                       hit.point.y + (reflectDirection.y * lineLength),
                                       firePositionObject.transform.position.z);
                laserVertexCount++;
            }
            else {
                positions[laserVertexCount] = new Vector3(hit.point.x, hit.point.y,
                                       firePositionObject.transform.position.z);
                laserVertexCount++;
            }
        }
        else {
            positions[laserVertexCount] = new Vector3(firePosition.x + (fireDirection.x * lineLength),
                                   firePosition.y + (fireDirection.y * lineLength),
                                   firePositionObject.transform.position.z);
            laserVertexCount++;
        }

        lineRenderer.positionCount = laserVertexCount;
        lineRenderer.SetPositions(positions);*/
    }
}
