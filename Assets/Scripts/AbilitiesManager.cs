using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AbilitiesManager : MonoBehaviour
{
    public Canvas ability1Canvas;
    public Canvas ability2Canvas;
    public Canvas ability3Canvas;

    public Image ability1Indicator;
    public Image ability2Indicator;
    public Image ability3Indicator;

    public GameObject bomb1Prefab;
    public GameObject bomb2Prefab;
    public GameObject bomb3Prefab;

    public Transform deployPoint;
    private Vector3 targetPoint;
    private Vector3 position;
    private RaycastHit hit;
    private Ray ray;

    void Start()
    {
        ability1Canvas.enabled = false;
        ability2Canvas.enabled = false;
        ability3Canvas.enabled = false;

        ability1Indicator.enabled = false;
        ability2Indicator.enabled = false;
        ability3Indicator.enabled = false;
    }

    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Ability1Canvas();
        Ability2Canvas();
        Ability3Canvas();
    }

    private void Ability1Canvas()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            ability1Canvas.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0) && ability1Indicator.enabled == true && ability1Canvas.enabled == true)
            {
                targetPoint = hit.point;
                Shoot(bomb1Prefab, deployPoint);
            }
            else if (Input.GetMouseButtonDown(1) && ability1Indicator.enabled == true && ability1Canvas.enabled == true)
            {
                ToggleAbility(ability1Canvas, ability1Indicator);
            }

        }
    }

    private void Ability2Canvas()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            ability2Canvas.transform.position = hit.point;
        }
    }

    private void Ability3Canvas()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            ability3Canvas.transform.position = hit.point;
        }
    }

    public void ToggleAbility(Canvas abilityCanvas, Image abilityIndicator)
    {
        abilityCanvas.enabled = !abilityCanvas.enabled;
        abilityIndicator.enabled = abilityCanvas.enabled;
    }
    public void ActivateAbility1()
    {
        ToggleAbility(ability1Canvas, ability1Indicator);
    }

    public void ActivateAbility2()
    {
        ToggleAbility(ability2Canvas, ability2Indicator);
    }

    public void ActivateAbility3()
    {
        ToggleAbility(ability3Canvas, ability3Indicator);
    }

    private void Shoot(GameObject bombPrefab, Transform deployPoint)
    {
        GameObject bombDeployed = Instantiate(bombPrefab, deployPoint.position, deployPoint.rotation);
        Bomb bomb = bombDeployed.GetComponent<Bomb>();
        bomb.GetTargetPoint(targetPoint);
    }
}
