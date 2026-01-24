using UnityEngine;

public class DamageDealer : MonoBehaviour
{
   [SerializeField] CharacterBaseStats _stats;

    float _power;
    public float power;

    public void SetPower(float newPower)
    {
        _power = newPower;
        power = _power;
        Debug.Log(power + " is the damage output");
    }
}

