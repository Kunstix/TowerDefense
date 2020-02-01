using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPlacementController : MonoBehaviour
{
    public GameObject shooterPrefab;
    private GameObject shooter;

    private void OnMouseUp()
    {
        if (placeShooter())
        {
            int cost = shooterPrefab.GetComponent<UpgradeController>().upgrades[0].cost;
            if (GameManager.GM.GetCoins() >= cost)
            {
                shooter = Instantiate(shooterPrefab, transform.position, Quaternion.identity);
                GameManager.GM.SetCoins(GameManager.GM.GetCoins() - shooterPrefab.GetComponent<UpgradeController>().currentUpgrade.cost);
            }
        }
        else if (upgradeShooter())
        {
            shooter.GetComponent<UpgradeController>().Upgrade();
        }
    }

    private bool placeShooter()
    {
        return shooter == null;
    }

    private bool upgradeShooter()
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
