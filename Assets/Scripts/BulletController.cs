using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10.0F;
    public GameObject target;
    public int damage;

    public Vector3 startPosition;
    public Vector3 targetPosition;
    private float distance;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    void Update()
    {
        float flyingTime = Time.time - startTime;

        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, flyingTime * speed / distance);
        RotateBullet();

        if (gameObject.transform.position.Equals(targetPosition))
        {
            hitEnemy();
            Destroy(gameObject);
        }
    }

    private void RotateBullet()
    {
        if (target != null)
        {
            Vector3 offset = target.transform.position - transform.position;

            transform.rotation = Quaternion.LookRotation(
                                   Vector3.forward,
                                   offset          
                                 );
        }
    }
    

    private void hitEnemy()
    {
        if (target != null)
        {
            Transform healthBarTransform = target.transform.Find("HealthBar");
            HealthbarController healthbar = healthBarTransform.gameObject.GetComponent<HealthbarController>();

            Debug.Log("Reduce health!");
            healthbar.currentHealth -= Mathf.Max(damage, 0);

            if (healthbar.currentHealth <= 0)
            {
                EnemyDies();
            }
        }
    }

    private void EnemyDies()
    {
        Destroy(target);
        AudioManager.AM.enemyDies.Play();
        Debug.Log("Increase coins by 50");
        GameManager.GM.SetCoins(GameManager.GM.GetCoins() + 50);
        GameManager.GM.SetScore(GameManager.GM.GetScore() + 10);
    }
}
