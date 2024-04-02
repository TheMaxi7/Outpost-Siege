using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    private GameObject turretToBuild;
    public GameObject basicTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject laserTurretPrefab;
    public GameObject sniperTurretPrefab;


    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild (GameObject turret)
    {
        turretToBuild = turret;
    }
}
