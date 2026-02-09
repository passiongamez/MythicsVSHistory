using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats")]
public class CharacterBaseStats : ScriptableObject
{
    public string characterName;
    public float health;
    public float power;
    public float defense;
    public float speed;
    public int level;
    public int rarity;
}
