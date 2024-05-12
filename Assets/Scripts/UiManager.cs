using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static int livesCount = 20;
    public static int coinsCount = 300;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI turretRangeValue;
    [SerializeField] private TextMeshProUGUI turretDamageValue;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private TextMeshProUGUI sellValue;
    [SerializeField] private TextMeshProUGUI upgradeCost;

    void Start()
    {
        livesText.text = "" + livesCount;
        coinsText.text = "" + coinsCount;
    }

    void Update()
    {
        livesText.text = livesCount.ToString();
        coinsText.text = coinsCount.ToString();

        if (BuildManager.baseSelected != null)
        {
            if (BuildManager.baseSelected.isOccupied)
            {
                GetTurretLevel();
                turretRangeValue.text = BuildManager.baseSelected.turret.GetComponent<Turret>().attackRange.ToString();
                turretDamageValue.text = BuildManager.baseSelected.turret.GetComponent<Turret>().turretDamage.ToString();
                sellValue.text = BuildManager.baseSelected.turret.GetComponent<Turret>().sellValue.ToString();
                upgradeCost.text = BuildManager.baseSelected.turret.GetComponent<Turret>().upgradeCost.ToString();
            }

        }


    }

    void GetTurretLevel()
    {
        if (BuildManager.baseSelected.turret.GetComponent<Turret>().turretLevel == 1)
        {
            star2.SetActive(false);
            star3.SetActive(false);
        }
        if (BuildManager.baseSelected.turret.GetComponent<Turret>().turretLevel == 2)
            star2.SetActive(true);
        if (BuildManager.baseSelected.turret.GetComponent<Turret>().turretLevel == 3)
        {
            star2.SetActive(true);
            star3.SetActive(true);
        }
    }
}
