using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AbilitiesManager : MonoBehaviour
{

    [Header("Special Ability 1")]
    public Canvas ability1Canvas;
    public Image ability1Indicator;
    public GameObject bomb1Prefab;
    public float countdownAbility1;
    public int costAbility1;
    private bool isAbility1Ready = true;
    public Image cdOverlay1;


    [Header("Special Ability 2")]
    public Canvas ability2Canvas;
    public Image ability2Indicator;
    public GameObject bomb2Prefab;
    public float countdownAbility2;
    public int costAbility2;
    private bool isAbility2Ready = true;
    public Image cdOverlay2;

    [Header("Special Ability 3")]
    public Canvas ability3Canvas;
    public Image ability3Indicator;
    public GameObject bomb3Prefab;
    public float countdownAbility3;
    public int costAbility3;
    private bool isAbility3Ready = true;
    public Image cdOverlay3;

    [Header("General setup")]
    public Transform deployPoint;
    private Vector3 targetPoint;
    private Ray ray;

    void Start()
    {
        ability1Canvas.enabled = false;
        ability2Canvas.enabled = false;
        ability3Canvas.enabled = false;

        ability1Indicator.enabled = false;
        ability2Indicator.enabled = false;
        ability3Indicator.enabled = false;

        cdOverlay1.fillAmount = 0;
        cdOverlay2.fillAmount = 0;
        cdOverlay3.fillAmount = 0;
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
            if (Input.GetMouseButtonDown(0) && ability1Indicator.enabled == true && ability1Canvas.enabled == true && isAbility1Ready && costAbility1 <= UiManager.coinsCount)
            {
                UiManager.coinsCount -= costAbility1;
                targetPoint = hit.point;
                Shoot(bomb1Prefab, deployPoint);
                ToggleAbility(ability1Canvas, ability1Indicator);
                isAbility1Ready = false;
                StartCoroutine(StartAbilityCoooldown(cdOverlay1, countdownAbility1, 1));
            }
            if (Input.GetMouseButtonDown(1) && ability1Indicator.enabled == true && ability1Canvas.enabled == true)
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
            if (Input.GetMouseButtonDown(0) && ability2Indicator.enabled == true && ability2Canvas.enabled == true && isAbility2Ready && costAbility2 <= UiManager.coinsCount)
            {
                UiManager.coinsCount -= costAbility2;
                targetPoint = hit.point;
                Shoot(bomb2Prefab, deployPoint);
                ToggleAbility(ability2Canvas, ability2Indicator);
                isAbility2Ready = false;
                StartCoroutine(StartAbilityCoooldown(cdOverlay2, countdownAbility2, 2));
            }
            if (Input.GetMouseButtonDown(1) && ability2Indicator.enabled == true && ability2Canvas.enabled == true)
            {
                ToggleAbility(ability2Canvas, ability2Indicator);
            }
        }
    }

    private void Ability3Canvas()
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            ability3Canvas.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0) && ability3Indicator.enabled == true && ability3Canvas.enabled == true && isAbility3Ready && costAbility3 <= UiManager.coinsCount)
            {
                UiManager.coinsCount -= costAbility3;
                targetPoint = hit.point;
                Shoot(bomb3Prefab, deployPoint);
                ToggleAbility(ability3Canvas, ability3Indicator);
                isAbility3Ready = false;
                StartCoroutine(StartAbilityCoooldown(cdOverlay3, countdownAbility3, 3));
            }
            if (Input.GetMouseButtonDown(1) && ability3Indicator.enabled == true && ability3Canvas.enabled == true)
            {
                ToggleAbility(ability3Canvas, ability3Indicator);
            }
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

    IEnumerator StartAbilityCoooldown(Image abilityCdOverlay, float abilityCooldown, int abilityIndex)
    {
        float cooldownTimer = abilityCooldown;
        while (cooldownTimer >= 0)
        {
            abilityCdOverlay.fillAmount = cooldownTimer / abilityCooldown;
            cooldownTimer -= Time.deltaTime;
            yield return null;
        }
        switch (abilityIndex)
        {
            case 1:
                isAbility1Ready = true;
                break;
            case 2:
                isAbility2Ready = true;
                break;
            case 3:
                isAbility3Ready = true;
                break;
            default:
                break;
        }
    }
}
