using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static int livesCount = 20;
    public static int coinsCount = 150;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coinsText;

    void Start()
    {
        livesText.text = "" + livesCount;
        coinsText.text = "" + coinsCount;
    }

    void Update()
    {
        livesText.text = livesCount.ToString();
        coinsText.text = coinsCount.ToString();
    }
}
