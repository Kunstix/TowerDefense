using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<ShooterUpgrade> upgrades;
    public ShooterUpgrade currentUpgrade;

    private void OnEnable()
    {
        Debug.Log("Set current upgrade: " + upgrades[0].cost);
        currentUpgrade = upgrades[0];
        DisplayShooterUpgrade(currentUpgrade);
    }

    public void DisplayShooterUpgrade(ShooterUpgrade upgrade)
    {
        int currentUpgradeIndex = upgrades.IndexOf(currentUpgrade);

        for(int i = 0; i < upgrades.Count; i++)
        {
            if(upgrade.display != null)
            {
                if(i == currentUpgradeIndex)
                {
                    upgrades[i].display.SetActive(true);
                } else
                {
                    upgrades[i].display.SetActive(false);
                    
                }
            }
        }
    }

    public void Upgrade()
    {
        int currentUpgradeIndex = upgrades.IndexOf(currentUpgrade);
        if(currentUpgradeIndex < upgrades.Count - 1)
        {
            currentUpgrade = upgrades[currentUpgradeIndex + 1];
            DisplayShooterUpgrade(currentUpgrade);
        }
    }

    public ShooterUpgrade GetNextUpgrade()
    {
        int currentUpgradeIndex = upgrades.IndexOf(currentUpgrade);
        int maxUpgradeIndex = upgrades.Count - 1;

        if(currentUpgradeIndex < maxUpgradeIndex)
        {
            return upgrades[currentUpgradeIndex + 1];
        } else
        {
            return null;
        }

    }

    public ShooterUpgrade GetShooterUpgrade()
    {
        return currentUpgrade;
    }
}

[System.Serializable]
public class ShooterUpgrade
{
    public int cost;
    public GameObject display;
    public GameObject bullet;
    public float fireRate;
}
