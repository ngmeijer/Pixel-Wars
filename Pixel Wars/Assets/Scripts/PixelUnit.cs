using UnityEngine;

[CreateAssetMenu(fileName = "Pixel Army")]
public class PixelUnit : ScriptableObject
{
    public Color ArmyColour;
    public float RespawnRate;
    public BATTLE_MODE BattleMode;
}