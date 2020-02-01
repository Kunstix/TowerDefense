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

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if(target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthbarController healthbar = healthBarTransform.gameObject.GetComponent<HealthbarController>();

                Debug.Log("Reduce health!");
                healthbar.currentHealth -= Mathf.Max(damage, 0);
              
                if(healthbar.currentHealth <= 0)
                {
                    Destroy(target);
                    // Audio abspielen
                    Debug.Log("Increase coins by 50");
                    GameManager.GM.SetCoins(GameManager.GM.GetCoins() + 50);
                }
            }

            Destroy(gameObject);
        }
    }
}
