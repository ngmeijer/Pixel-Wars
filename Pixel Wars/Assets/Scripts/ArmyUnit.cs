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
    [Range(0.1f, 5f)] public float RespawnRate;
    [Range(0.1f, 5f)] public float AttackSpeed;
    [Range(10, 1000)] public int BaseHealth;
    [Range(0, 10)] public int MaxAmountOfUnits;
    public GameObject LegionPrefab;
}