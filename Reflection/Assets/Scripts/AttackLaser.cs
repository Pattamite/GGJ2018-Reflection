using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLaser : MonoBehaviour {
    public float lineLength = 100;
    public int maxReflectCount = 100;
    public Color normalColor;
    public Color fireColor;
    public float normalSize = 0.05f;
    public float fireSize = 0.10f;
    public float fireTime = 0.5f;

    private float lastFireTime = 0.0f;
    private LineRenderer lineRenderer;
    private Vector2 firePosition;
    private Vector2 fireDirection;
    private bool isFiring;
    

    // Use this for initialization
    void Awake () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = normalColor;
        lineRenderer.endColor = normalColor;
        lineRenderer.startWidth = normalSize;
        lineRenderer.endWidth = normalSize;
        Material whiteDiffuseMat = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.material = whiteDiffuseMat;
        isFiring = false;
    }

    // Update is called once per frame
    void Update () {
        updateLine();
    }

    public void updateLine () {
        if (isFiring && Time.time - lastFireTime < fireTime) {
            float timeRatio = (Time.time - lastFireTime) / fireTime;
            Color currentColor = Color.Lerp(fireColor, normalColor, timeRatio);
            lineRenderer.startColor = currentColor;
            lineRenderer.endColor = currentColor;

            float currentSize = Mathf.Lerp(fireSize, normalSize, timeRatio);
            lineRenderer.startWidth = currentSize;
            lineRenderer.endWidth = currentSize;
        }
        else if (isFiring) {
            Destroy(gameObject);
        }
    }

    public void Attack (Vector2 firePosition, Vector2 fireDirection) {
        this.firePosition = firePosition;
        this.fireDirection = fireDirection;
        lastFireTime = Time.time;
        DrawLaser();
        isFiring = true;
    }

    private void DrawLaser () {
        print(lineRenderer);
        ResetLineRenderer();
        AddPositionToLineRenderer(firePosition);
        bool isLineEnd = false;
        GameObject lastHitObject = null;

        Vector2 currentPosition = new Vector2(firePosition.x, firePosition.y);
        Vector2 currentDirection = new Vector2(fireDirection.x, fireDirection.y);

        RaycastHit2D objectHitData = Physics2D.Raycast(currentPosition, currentDirection, lineLength, 1 << StaticVar.LAYER_BLOCK);

        while (objectHitData && lineRenderer.positionCount <= maxReflectCount) {
            DetectEnemy(currentPosition, currentDirection, objectHitData.distance, 1);
            lastHitObject = objectHitData.collider.gameObject;

            BlockAdapter blockAdapter = lastHitObject.GetComponent<BlockAdapter>();
            if (blockAdapter) blockAdapter.HitByLaser();

            AddPositionToLineRenderer(new Vector3(objectHitData.point.x, objectHitData.point.y));

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
            DetectEnemy(currentPosition, currentDirection, lineLength, 1);
            AddPositionToLineRenderer(new Vector3(currentPosition.x + (currentDirection.x * lineLength),
                                       currentPosition.y + (currentDirection.y * lineLength)));
        }
    }

    private void AddPositionToLineRenderer (Vector2 position) {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(position.x, position.y, -2));
    }

    private void ResetLineRenderer () {
        lineRenderer.positionCount = 0;
    }

    private void DetectEnemy (Vector2 position, Vector2 direction, float range, int damage) {
        GameObject lastEnemyHit = null;
        RaycastHit2D enemyHitData = Physics2D.Raycast(position, direction, range, 1 << StaticVar.LAYER_ENEMY);

        while (enemyHitData) {
            lastEnemyHit = enemyHitData.collider.gameObject;
            print(lastEnemyHit.ToString());
            range -= enemyHitData.distance;

            lastEnemyHit.GetComponent<Collider2D>().enabled = false;
            enemyHitData = Physics2D.Raycast(enemyHitData.point, direction, range, 1 << StaticVar.LAYER_ENEMY);
            lastEnemyHit.GetComponent<Collider2D>().enabled = true;

            Enemy enemy = lastEnemyHit.GetComponent<Enemy>();
            if (enemy) {
                enemy.GetHit(damage);
            }
        }
    }
}
