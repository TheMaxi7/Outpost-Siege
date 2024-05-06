using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    BuildManager buildManager;
    public static bool isTurretSelected = false;
    PreviewSystem previewSystem;

    private void Start()
    {
        previewSystem = PreviewSystem.instance;
        buildManager = BuildManager.instance;
    }

    public void SelectBasicTurret()
    {
        buildManager.SetTurretToBuild(buildManager.basicTurretPrefab);
        isTurretSelected = true;
        if (BuildManager.turretToBuild != null )
            previewSystem.StopShowingPreview();
        
        previewSystem.StartShowingPlacementPreview(BuildManager.turretToBuild, BuildManager.baseSelected.transform.position + Base.offset, BuildManager.baseSelected.transform.rotation);

    }

    public void SelectMissileTurret()
    {
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
        isTurretSelected = true;
        if (BuildManager.turretToBuild != null)
            previewSystem.StopShowingPreview();
        previewSystem.StartShowingPlacementPreview(BuildManager.turretToBuild, BuildManager.baseSelected.transform.position + Base.offset, BuildManager.baseSelected.transform.rotation);

    }

    public void SelectLaserTurret()
    {
        buildManager.SetTurretToBuild(buildManager.laserTurretPrefab);
        isTurretSelected = true;
        if (BuildManager.turretToBuild != null)
            previewSystem.StopShowingPreview();
        previewSystem.StartShowingPlacementPreview(BuildManager.turretToBuild, BuildManager.baseSelected.transform.position + Base.offset, BuildManager.baseSelected.transform.rotation);
    }

    public void SelectSniperTurret()
    {
        buildManager.SetTurretToBuild(buildManager.sniperTurretPrefab);
        isTurretSelected = true;
        if (BuildManager.turretToBuild != null)
            previewSystem.StopShowingPreview();
        previewSystem.StartShowingPlacementPreview(BuildManager.turretToBuild, BuildManager.baseSelected.transform.position + Base.offset, BuildManager.baseSelected.transform.rotation);
    }
}
