using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats")]
public class CharacterBaseStats : ScriptableObject
{
    public string characterName;
    public int health;
    public int power;
    public int defense;
    public float speed;
    public int level;
    public int rarity;
}
