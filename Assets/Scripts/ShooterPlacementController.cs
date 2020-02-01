using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlacementController : MonoBehaviour
{
    public GameObject shooterPrefab;
    private GameObject shooter;

    private void OnMouseUp()
    {
        if (canPlaceShooter())
        {
            PlaceShooterForCoins();
        }
        else if (canUpgradeShooter())
        {
            UpgradeShooterForCoins();
        }
    }

    private bool canPlaceShooter()
    {
        return shooter == null;
    }

    private void PlaceShooterForCoins()
    {
        int cost = shooterPrefab.GetComponent<UpgradeController>().upgrades[0].cost;
        if (GameManager.GM.GetCoins() >= cost)
        {
            Debug.Log("Place shooter.");
            shooter = Instantiate(shooterPrefab, transform.position, Quaternion.identity);
            ReduceCoins();
        }
    }

    private void ReduceCoins()
    {
        Debug.Log("Reduce coins: " + shooterPrefab.GetComponent<UpgradeController>().currentUpgrade.cost);
        GameManager.GM.SetCoins(GameManager.GM.GetCoins() - shooter.GetComponent<UpgradeController>().currentUpgrade.cost);

    }

    private void UpgradeShooterForCoins()
    {
        int cost = shooter.GetComponent<UpgradeController>().GetNextUpgrade().cost;
        if (GameManager.GM.GetCoins() >= cost)
        {
            Debug.Log("Upgrade shooter.");
            shooter.GetComponent<UpgradeController>().Upgrade();
            ReduceCoins();
        }
    }

    private bool canUpgradeShooter()
    {
        if (shooter != null)
        {
            UpgradeController upgrader = shooter.GetComponent<UpgradeController>();
            ShooterUpgrade nextUpgrade = upgrader.GetNextUpgrade();
            if (nextUpgrade != null)
            {
                return true;
            }
        }
        return false;
    }
}
