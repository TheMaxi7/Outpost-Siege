using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    public Color highlightColor;
    private Renderer rend;
    public static Vector3 offset = new Vector3(0f, 0.13f,0f);
    public GameObject turret;
    BuildManager buildManager;
    PreviewSystem previewSystem;
    public bool isOccupied = false;
    public Material hoverMat;
    private int selectedTurretCost;

    public static bool baseSelected = false;

    void Start()
    {
        previewSystem = PreviewSystem.instance;
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();

        startColor = rend.material.color;
    }

    void Update()
    {
        if (baseSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ResetView();
            }
        }     
    }

    private void OnMouseDown()
    {
        if (!isOccupied)
        {
            if (baseSelected)
            {
                UpdateBasePreview();
            }
            //move camera
            baseSelected = true;
            BuildManager.baseSelected = this;
        }

    }
    private void OnMouseEnter()
    {
       
        if (isOccupied == false)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            turret.GetComponent<Turret>().range.SetActive(true);
        }

    }

    private void OnMouseExit()
    {
        if (isOccupied)
        {
            turret.GetComponent<Turret>().range.SetActive(false);
        }
        rend.material.color = startColor;
    }

    private void ResetView()
    {
        //move camera to default
        baseSelected = false;
        previewSystem.StopShowingPreview();

    }

    private void UpdateBasePreview()
    {
        previewSystem.StopShowingPreview();
        BuildManager.baseSelected = this;
        previewSystem.StartShowingPlacementPreview(BuildManager.turretToBuild, BuildManager.baseSelected.transform.position + offset, BuildManager.baseSelected.transform.rotation);
    }




}
