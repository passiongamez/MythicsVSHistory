using GameControlMaster;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public  CharacterBaseStats stats;
   public GameObject _player;
    [SerializeField] BasicCharacterControls _characterControls;

    public float _bodyPartHealth;
    float _incomingDamage;
    float _defense;
float    _bodyPartID;

    private void Awake()
    {
        _incomingDamage = stats.health;
        _defense = stats.defense;

        _characterControls = _player.GetComponent<BasicCharacterControls>();

        if (_characterControls == null)
        {
            Debug.Log("Character Controls is null");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _incomingDamage = 0;

        if (other.TryGetComponent<DamageDealer>(out DamageDealer damageDealer))
        {
            _incomingDamage = damageDealer.power;
            _incomingDamage -= _defense;

            if (_incomingDamage <= 0)
            {
                _incomingDamage = 1;
            }

            switch (_bodyPartID)
            {
                case 0:
                    _bodyPartHealth -= _incomingDamage;
                    Debug.Log(_incomingDamage + " damage done to head");
                    _characterControls.SendDamage(_incomingDamage);
                    break;
                case 1:
                    _bodyPartHealth -= _incomingDamage;
                    Debug.Log(_incomingDamage + " damage done to body");
                    _characterControls.SendDamage(_incomingDamage);
                    break;
                case 2:
                    _bodyPartHealth -= _incomingDamage;
                    Debug.Log(_incomingDamage + " damage done to arms");
                    _characterControls.SendDamage(_incomingDamage);
                    break;
                case 3:
                    _bodyPartHealth -= _incomingDamage;
                    Debug.Log(_incomingDamage + " damage done to legs");
                    _characterControls.SendDamage(_incomingDamage);
                    break;
            }
        }
        if (_bodyPartHealth <= 0)
        {
            _characterControls.CallForDeath();
        }
    }
}
