using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static int livesCount = 20;
    public static int coinsCount = 50;
    public static int totalCoinsInGame;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI turretRangeValue;
    [SerializeField] private TextMeshProUGUI turretDamageValue;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private TextMeshProUGUI sellValue;
    [SerializeField] private TextMeshProUGUI upgradeCost;
    [SerializeField] private Sprite basicEnemySprite;
    [SerializeField] private Sprite fastEnemySprite;
    [SerializeField] private Sprite strongEnemySprite;
    [SerializeField] private Image waveEnemyTypeIcon;
    [SerializeField] private TextMeshProUGUI enemiesInNextWaveValue;
    [SerializeField] private Sprite nextEnemySprite;
    private Spawner[] spawners;

    void Start()
    {   
        spawners = Object.FindObjectsOfType<Spawner>();
        SetNextWaveIndicator();
        totalCoinsInGame = coinsCount;
        livesText.text = "" + livesCount;
        coinsText.text = "" + coinsCount;
        enemiesInNextWaveValue.text = "x " + WaveManager.nextWaveEnemies* spawners.Length;
        waveEnemyTypeIcon.sprite = nextEnemySprite;
        
    }

    void Update()
    {
        livesText.text = livesCount.ToString();
        coinsText.text = coinsCount.ToString();
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
