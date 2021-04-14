using UnityEngine;

public enum ArmyColourEnum
{
    GREEN,
    BLUE,
    RED,
    PURPLE,
    ORANGE,
};

[CreateAssetMenu(fileName = "Army Unit")]
public class ArmyUnit : ScriptableObject
{
    public ArmyColourEnum ColourEnum;
    public BATTLE_MODE BattleMode;
    [Range(0.1f, 5f)] public float RespawnRate = 0.8f;
    [Range(0.1f, 5f)] public float AttackSpeed = 1f;
    [Range(10, 1000)] public int BaseHealth = 300;
    [Range(0, 8)] public int MaxAmountOfUnits = 5;
    public GameObject LegionPrefab;
}