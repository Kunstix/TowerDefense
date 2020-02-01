using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0F;
    public GameObject[] pathPoints;
    private int currentPathPoint;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        Vector3 startPosition = pathPoints[currentPathPoint].transform.position;
        Vector3 endPosition = pathPoints[currentPathPoint + 1].transform.position;

        float totalDistance = Vector3.Distance(startPosition, endPosition);
        float currentDistance = (Time.time - startTime) * speed;

        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentDistance / totalDistance);

        if (gameObject.transform.position.Equals(endPosition))
        {
            if(currentPathPoint < pathPoints.Length - 2)
            {
                currentPathPoint++;
                RotateEnemy();
                startTime = Time.time;
            } else
            {
                CameraShakeController.instance.ShakeCamera();
                Destroy(gameObject);
                GameManager.GM.SetLives(GameManager.GM.GetLives() - 1);
            }
            
        }
    }

    public float GetDistanceToTreasure()
    {
        float distance = 0;
        distance += Vector2.Distance(gameObject.transform.position, pathPoints[currentPathPoint + 1].transform.position);
        for(int i = currentPathPoint + 1; i < pathPoints.Length - 1; i++)
        {
            Vector3 startPosition = pathPoints[i].transform.position;
            Vector3 endPosition = pathPoints[i].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

    private void RotateEnemy()
    {
        Vector3 startPosition = pathPoints[currentPathPoint].transform.position;
        Vector3 endPosition = pathPoints[currentPathPoint + 1].transform.position;
        Vector3 direction = endPosition - startPosition;

        float x = direction.x;
        float y = direction.y;
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;

        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }
}
