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
    [SerializeField]private Canvas ability1Canvas;
    [SerializeField] private Image ability1Indicator;
    [SerializeField] private GameObject bomb1Prefab;
    [SerializeField] private float countdownAbility1;
    public static int costAbility1;
    [SerializeField] private bool isAbility1Ready = true;
    [SerializeField] private Image cdOverlay1;


    [Header("Special Ability 2")]
    [SerializeField] private Canvas ability2Canvas;
    [SerializeField] private Image ability2Indicator;
    [SerializeField] private GameObject bomb2Prefab;
    [SerializeField] private float countdownAbility2;
    public static int costAbility2;
    [SerializeField] private bool isAbility2Ready = true;
    [SerializeField] private Image cdOverlay2;

    [Header("Special Ability 3")]
    [SerializeField] private Canvas ability3Canvas;
    [SerializeField] private Image ability3Indicator;
    [SerializeField] private GameObject bomb3Prefab;
    [SerializeField] private float countdownAbility3;
    public static int costAbility3;
    [SerializeField] private bool isAbility3Ready = true;
    [SerializeField] private Image cdOverlay3;

    [Header("General setup")]
    [SerializeField] private Transform deployPoint;
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
