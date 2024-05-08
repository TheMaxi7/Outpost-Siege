using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color highlightColor;
    private Color startColor;
    private Renderer rend;
    public static Vector3 offset = new Vector3(0f, 0.13f,0f);

    [HideInInspector] public GameObject turret;
    PreviewSystem previewSystem;
    [HideInInspector] public bool isOccupied = false;
    [SerializeField] private Material hoverMat;
    public static bool baseSelected = false;

    void Start()
    {
        previewSystem = PreviewSystem.instance;

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
                if (CameraController.isBuilding)
                    CameraController.UpdateZoomBase(this);
                    
            }
            baseSelected = true;
            BuildManager.baseSelected = this;
        }

        if (!CameraController.isBuilding)
            CameraController.ZoomBase(this);

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
        CameraController.UnZoomBase();
        baseSelected = false;
        previewSystem.StopShowingPreview();

    }

    private void UpdateBasePreview()
    {
        if (previewSystem.isShowingPreview)
        {
            previewSystem.StopShowingPreview();
            BuildManager.baseSelected = this;
            previewSystem.StartShowingPlacementPreview(BuildManager.turretToBuild, BuildManager.baseSelected.transform.position + offset, BuildManager.baseSelected.transform.rotation);
        }

    }




}
