using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectBasicTurret()
    {
        buildManager.SetTurretToBuild(buildManager.basicTurretPrefab);
    }

    public void SelectMissileTurret()
    {
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }

    public void SelectLaserTurret()
    {
        buildManager.SetTurretToBuild(buildManager.laserTurretPrefab);
    }

    public void SelectSniperTurret()
    {
        buildManager.SetTurretToBuild(buildManager.sniperTurretPrefab);
    }
}
