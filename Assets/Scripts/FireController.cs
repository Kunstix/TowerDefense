using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public List<GameObject> enemiesInRange;

    private float lastFireTime;
    private UpgradeController upgradeData;


    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastFireTime = Time.time;
        upgradeData = gameObject.GetComponentInChildren<UpgradeController>();
    }

    void Update()
    {
        GameObject target = GetTargetEnemy();
        FireAt(target);
        RotateShooter(target);
    }

    private GameObject GetTargetEnemy()
    {
        float minEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToTreasure = enemy.GetComponent<EnemyController>().GetDistanceToTreasure();
            if (distanceToTreasure < minEnemyDistance)
            {
                minEnemyDistance = distanceToTreasure;
                return enemy;
            }
        }
        return null;
    }

    private void FireAt(GameObject target)
    {
        if (target != null)
        {
            if (Time.time - lastFireTime > upgradeData.currentUpgrade.fireRate)
            {
                Fire(target.GetComponent<Collider2D>());
                lastFireTime = Time.time;
            }
        }
    }

    private void RotateShooter(GameObject target)
    {
        Vector3 direction = gameObject.transform.position - target.transform.position;
        float angleInDegrees = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;
        gameObject.transform.rotation = Quaternion.AngleAxis(angleInDegrees, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    void Fire(Collider2D target)
    {
        Debug.Log("Fire!");
        GameObject bulletPrefab = upgradeData.currentUpgrade.bullet;
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = startPosition;
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.target = target.gameObject;
        bulletController.startPosition = startPosition;
        bulletController.targetPosition = targetPosition;

        AudioManager.AM.shoot.Play();
    }
}
