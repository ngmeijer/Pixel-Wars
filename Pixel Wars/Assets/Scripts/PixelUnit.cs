using UnityEngine;

public enum ArmyColourEnum
{
    GREEN,
    BLUE,
    RED,
    PURPLE,
    ORANGE,
};

[CreateAssetMenu(fileName = "Pixel Army")]
public class PixelUnit : ScriptableObject
{
    public ArmyColourEnum colourEnum;
    public float RespawnRate;
    public BATTLE_MODE BattleMode;
    public float AttackSpeed;
}