using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;
    public static bool isTurretSelected = false;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectBasicTurret()
    {
        buildManager.SetTurretToBuild(buildManager.basicTurretPrefab);
        isTurretSelected = true;
    }

    public void SelectMissileTurret()
    {
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
        isTurretSelected = true;
    }

    public void SelectLaserTurret()
    {
        buildManager.SetTurretToBuild(buildManager.laserTurretPrefab);
        isTurretSelected = true;
    }

    public void SelectSniperTurret()
    {
        buildManager.SetTurretToBuild(buildManager.sniperTurretPrefab);
        isTurretSelected = true;
    }
}
