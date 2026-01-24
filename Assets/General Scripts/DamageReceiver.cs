using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    BasicCharacterControls _characterControls;
    [SerializeField] CharacterBaseStats _stats;

    [SerializeField] GameObject _player;
    [SerializeField] float _bodyPartHealth;

    //for reference head is 0, body 1, arms 2, legs 3
    public int _bodyPartID;

    float _incomingDamage;
    float _defense;

    private void Awake()
    {
        _characterControls = _player.GetComponent<BasicCharacterControls>();

        if(_characterControls == null)
        {
            Debug.Log("Character Controls is null");
        }

        _defense = _stats.defense;
    }

    private void OnTriggerEnter(Collider other)
    {
        _incomingDamage = 0;

            if (other.TryGetComponent<DamageDealer>(out DamageDealer damageDealer))
            {
                _incomingDamage = damageDealer.power;
                _incomingDamage -= _defense;

                if(_incomingDamage <= 0)
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

