using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    public GameObject basicTurretPrefab;
    public GameObject missileTurretPrefab;
    public GameObject laserTurretPrefab;
    public GameObject sniperTurretPrefab;

    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject turretInfoUI;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private GameObject cancelButton;

    public static Base baseSelected;
    public static GameObject turretToBuild;

    PreviewSystem previewSystem;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        shop.SetActive(false);
    }

    private void Start()
    {
        previewSystem = PreviewSystem.instance;
    }

    private void Update()
    {
        if (Base.baseSelected == false)
        {
            shop.SetActive(false);
            confirmButton.SetActive(false);
            cancelButton.SetActive(false);
            turretInfoUI.SetActive(false);
        }


        if ((Base.baseSelected == true))
        {
            if (!baseSelected.isOccupied)
            {
                shop.SetActive(true);
                confirmButton.SetActive(true);
                cancelButton.SetActive(true);
                turretInfoUI.SetActive(false);
            }
            else
            {
                turretInfoUI.SetActive(true);
                shop.SetActive(false);
                confirmButton.SetActive(false);
                cancelButton.SetActive(false);
            }
        }
    }


    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    public void ConfirmBuild()
    {
        if (turretToBuild.GetComponent<Turret>().turretCost <= UiManager.coinsCount)
        {
            GameObject turretIstantiated = Instantiate(turretToBuild, baseSelected.transform.position + Base.offset, baseSelected.transform.rotation);
            turretIstantiated.GetComponent<Turret>().canShoot = true;
            UiManager.coinsCount -= turretToBuild.GetComponent<Turret>().turretCost;
            UiManager.totalMoneyConvertedInDps += turretToBuild.GetComponent<Turret>().turretCost;
            
            Shop.isTurretSelected = false;
            baseSelected.isOccupied = true;
            baseSelected.turret = turretIstantiated;
            previewSystem.StopShowingPreview();
            baseSelected.turret.GetComponent<Turret>().range.SetActive(false);
        }
    }

    public void UpgradeTurret()
    {
        if ((baseSelected.turret.GetComponent<Turret>().upgradeCost <= UiManager.coinsCount) && (baseSelected.turret.GetComponent<Turret>().turretLevel < 3))
        {
            UiManager.coinsCount -= baseSelected.turret.GetComponent<Turret>().upgradeCost;
            UiManager.totalMoneyConvertedInDps += turretToBuild.GetComponent<Turret>().upgradeCost;
            baseSelected.turret.GetComponent<Turret>().turretLevel += 1;
            baseSelected.turret.GetComponent<Turret>().attackRange *= 1.3f;
            baseSelected.turret.GetComponent<Turret>().turretDamage *= 1.3f;
            baseSelected.turret.GetComponent<Turret>().upgradeCost = (int)(baseSelected.turret.GetComponent<Turret>().upgradeCost * 1.4f);
            baseSelected.turret.GetComponent<Turret>().sellValue = (int)(baseSelected.turret.GetComponent<Turret>().sellValue * 1.4f);
        }

    }
    public void SellTurret()
    {
        UiManager.coinsCount += baseSelected.turret.GetComponent<Turret>().sellValue;
        Destroy(baseSelected.turret);
        baseSelected.isOccupied = false;
    }
}
