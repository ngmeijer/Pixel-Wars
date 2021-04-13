using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropertiesCanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI armyColourText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI baseHealthText;
    [SerializeField] private TextMeshProUGUI respawnRateText;

    private LegionSpawner spawner;
    
    private void OnEnable()
    {
        if (TryGetComponent(out spawner))
        {
            ArmyUnit army = spawner.armyProperties;
            armyColourText.text = army.ColourEnum.ToString();
            attackSpeedText.text = army.AttackSpeed.ToString();
            baseHealthText.text = army.BaseHealth.ToString();
            respawnRateText.text = army.RespawnRate.ToString();
        }
        

    }
}
