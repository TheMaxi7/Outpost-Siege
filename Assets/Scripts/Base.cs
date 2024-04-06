using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    public Vector3 offset;
    private GameObject turret;
    BuildManager buildManager;
    private GameObject previewObject;
    bool isOccupied = false;
    private int selectedTurretCost;
    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void Update()
    {
       
    }

    private void OnMouseDown()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        
        if(turret != null)
        {
            return;
        }

        GameObject turretToBuild = buildManager.GetTurretToBuild();
        selectedTurretCost = turretToBuild.GetComponent<Turret>().turretCost; 
        if (selectedTurretCost <= UiManager.coinsCount) 
        {
            turret = Instantiate(turretToBuild, transform.position + offset, transform.rotation);
            turret.GetComponent<Turret>().canShoot = true;
            UiManager.coinsCount -= selectedTurretCost;
            isOccupied = true;
        }

    }
    private void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        if (isOccupied == false)
        {
            rend.material.color = hoverColor;
            StartShowingPlacementPreview(buildManager.GetTurretToBuild());
        }
        else
        {
           turret.GetComponent<Turret>().rangeIndicator.enabled = true;
        }
        
    }

    private void OnMouseExit()
    {
        if (isOccupied)
        {
            turret.GetComponent<Turret>().rangeIndicator.enabled = false;
        }
        rend.material.color = startColor;
        StopShowingPreview();
    }

    public void StopShowingPreview()
    {
        if (previewObject != null)
            Destroy(previewObject);
    }


    public void StartShowingPlacementPreview(GameObject prefab)
    {
        
        previewObject = Instantiate(prefab, transform.position + offset, transform.rotation);
        PreparePreview(previewObject);
    }

    private void PreparePreview(GameObject previewObject)
    {
        
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer renderer in renderers)
        {
            Debug.Log(renderer);
            if (renderer.GetComponent<CanvasRenderer>() != null)
            {
                continue;
            }

            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = rend.material;
            }
            renderer.materials = materials;
        }
    }

}
