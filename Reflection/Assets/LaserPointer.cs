using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserPointer : MonoBehaviour {

    public GameObject firePositionObject;
    public float lineLength;
    public int maxReflecCount = 100;

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
        int laserVertexCount = 2;
        Vector3[] positions = new Vector3[maxReflecCount];
        positions[0] = firePosition;

        if (Physics2D.Raycast(firePosition, fireDirection)) {
            RaycastHit2D hit = Physics2D.Raycast(firePosition, fireDirection);
            positions[1] = new Vector3(hit.point.x, hit.point.y,
                                   firePositionObject.transform.position.z);
        }
        else {
            positions[1] = new Vector3(firePosition.x + (fireDirection.x * lineLength),
                                   firePosition.y + (fireDirection.y * lineLength),
                                   firePositionObject.transform.position.z);
        }

        lineRenderer.positionCount = positions.Length; //change later
        lineRenderer.SetPositions(positions);

    }
}
