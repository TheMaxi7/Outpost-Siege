using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;

    public GameObject basicTurretPrefab;
    public  GameObject missileTurretPrefab;
    public  GameObject laserTurretPrefab;
    public  GameObject sniperTurretPrefab;

    [SerializeField] private GameObject shop;
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
            if (!shop.activeInHierarchy)
                return;
        }


        if (Base.baseSelected == true)
        {
            shop.SetActive(true);
            confirmButton.SetActive(true);
            cancelButton.SetActive(true);
        }
        else
        {
            shop.SetActive(false);
            confirmButton.SetActive(false);
            cancelButton.SetActive(false);
        }

    }


    public void SetTurretToBuild (GameObject turret)
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
            Shop.isTurretSelected = false;
            baseSelected.isOccupied = true;
            baseSelected.turret = turretIstantiated;
            previewSystem.StopShowingPreview();
            baseSelected.turret.GetComponent<Turret>().range.SetActive(false);
            baseSelected = null;
        }
    }
}
