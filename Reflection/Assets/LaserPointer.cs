using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LaserPointer : MonoBehaviour {

    public GameObject firePositionObject;
    public float lineLength;

    private LineRenderer lineRenderer;
    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 firePosition = firePositionObject.transform.position;
        float fireAngle = transform.eulerAngles.z;
        float xFireVector = Mathf.Cos(fireAngle / 180f * Mathf.PI);
        float yFireVector = Mathf.Sin(fireAngle / 180f * Mathf.PI);

        drawLaser(firePosition, xFireVector, yFireVector);
    }

    private void drawLaser(Vector3 firePosition, float xFireVector, float yFireVector) {
        Vector3[] positions = new Vector3[2];
        print(xFireVector.ToString() + " " + yFireVector.ToString());
        positions[0] = firePosition;
        positions[1] = new Vector3(firePosition.x + (xFireVector * lineLength),
                                   firePosition.y + (yFireVector * lineLength),
                                   firePosition.z);
        lineRenderer.positionCount = positions.Length; //change later
        lineRenderer.SetPositions(positions);

    }
}
