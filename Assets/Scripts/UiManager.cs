using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static int livesCount = 20;
    public static int coinsCount = 800;
    public static int totalMoneyConvertedInDps;
    [Header("General Game UI")]
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Sprite basicEnemySprite;
    [SerializeField] private Sprite fastEnemySprite;
    [SerializeField] private Sprite strongEnemySprite;
    [SerializeField] private Image waveEnemyTypeIcon;
    [SerializeField] private TextMeshProUGUI enemiesInNextWaveValue;
    [SerializeField] private Sprite nextEnemySprite;
    [SerializeField] private TextMeshProUGUI waveNumber;

    [Header("Turret UI")]
    [SerializeField] private TextMeshProUGUI turretRangeValue;
    [SerializeField] private TextMeshProUGUI turretDamageValue;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private TextMeshProUGUI sellValue;
    [SerializeField] private TextMeshProUGUI upgradeCost;

    [Header("Shop UI")]
    [SerializeField] private TextMeshProUGUI baseTurretPriceText;
    [SerializeField] private TextMeshProUGUI missileTurretPriceText;
    [SerializeField] private TextMeshProUGUI catapultTurretPriceText;
    [SerializeField] private TextMeshProUGUI laserTurretPriceText;
    [SerializeField] private GameObject basicTurretPrefab;
    [SerializeField] private GameObject missileTurretPrefab;
    [SerializeField] private GameObject laserTurretPrefab;
    [SerializeField] private GameObject catapultTurretPrefab;

    [Header("Abilities UI")]
    [SerializeField] private TextMeshProUGUI ability1Cost;
    [SerializeField] private TextMeshProUGUI ability2Cost;
    [SerializeField] private TextMeshProUGUI ability3Cost;


    private Spawner[] spawners;

    void Start()
    {   
        spawners = Object.FindObjectsOfType<Spawner>();
        SetNextWaveIndicator();
        totalMoneyConvertedInDps = 0;
        livesText.text = "" + livesCount;
        coinsText.text = "" + coinsCount;
        enemiesInNextWaveValue.text = "x " + WaveManager.nextWaveEnemies* spawners.Length;
        waveNumber.text = WaveManager.waveNumber + "/30";
        waveEnemyTypeIcon.sprite = nextEnemySprite;

        baseTurretPriceText.text = basicTurretPrefab.GetComponent<Turret>().turretCost.ToString() +" $";
        missileTurretPriceText.text = missileTurretPrefab.GetComponent<Turret>().turretCost.ToString() + " $";
        catapultTurretPriceText.text = catapultTurretPrefab.GetComponent<Turret>().turretCost.ToString() + " $";
        laserTurretPriceText.text = laserTurretPrefab.GetComponent<Turret>().turretCost.ToString() + " $";

        ability1Cost.text = AbilitiesManager.costAbility1.ToString() + " $";
        ability2Cost.text = AbilitiesManager.costAbility2.ToString() + " $";
        ability3Cost.text = AbilitiesManager.costAbility3.ToString() + " $";
    }

    void Update()
    {
        livesText.text = livesCount.ToString();
        coinsText.text = coinsCount.ToString();
        waveNumber.text = WaveManager.waveNumber + "/30";
        enemiesInNextWaveValue.text = "x " + WaveManager.nextWaveEnemies* spawners.Length;
        waveEnemyTypeIcon.sprite = nextEnemySprite;
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

        SetNextWaveIndicator();
        
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

    void SetNextWaveIndicator()
    {
        switch (WaveManager.nextEnemyType)
        {
            case 1:
                nextEnemySprite = basicEnemySprite;
                break;
            case 2:
                nextEnemySprite = fastEnemySprite;
                break;
            case 3:
                nextEnemySprite = strongEnemySprite;
                break;
        }
    }
}
