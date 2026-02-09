using UnityEngine;

public class DamageDealer : MonoBehaviour
{
   [SerializeField] CharacterBaseStats _stats;

    float _power;
    public float power { get { return _power; } }

    private void Start()
    {
        _power = _stats.power;
    }

    public void SetPower(float newPower)
    {
        _power = newPower;
        Debug.Log(power + " is the damage output");
    }
}

