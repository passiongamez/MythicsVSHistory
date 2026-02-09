using System.Collections;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    BasicCharacterControls _characterControls;
    [SerializeField] CharacterBaseStats _stats;

    [SerializeField] GameObject _player;
    [SerializeField] float _maxBodyPartHealth;
    [SerializeField] float _bodyPartHealth;

    //for reference head is 0, body 1, arms 2, legs 3
    public int _bodyPartID;

    float _incomingDamage;
    float _defense;

    WaitForSeconds _stunTimer;
    float _healthPCT;
    float _baseStunChance = .1f;
    float _stunChance;
    Coroutine _resetHPCoroutine;


    private void Awake()
    {
        _characterControls = _player.GetComponent<BasicCharacterControls>();

        if(_characterControls == null)
        {
            Debug.Log("Character Controls is null");
        }

        _bodyPartHealth = _maxBodyPartHealth;
        _defense = _stats.defense;
        _stunTimer = new WaitForSeconds(3f);
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
                    _characterControls.SendDamage(_incomingDamage);
                    _bodyPartHealth = Mathf.Max(0, _bodyPartHealth - _incomingDamage);

                    CalculateStun();

                    if(Random.value < _stunChance || _bodyPartHealth <= 0)
                    {
                        Debug.Log($"Stun rolled! Chance was {_stunChance:P0}");
                        _characterControls.OnStun(3f);
                        if(_resetHPCoroutine == null)
                        {
                            _resetHPCoroutine = StartCoroutine(ResetHP());
                        }
                    }
                    break;
                case 1:
                    _bodyPartHealth -= _incomingDamage;
                    _characterControls.SendDamage(_incomingDamage);
                    break;
                case 2:
                    _bodyPartHealth -= _incomingDamage;
                    _characterControls.SendDamage(_incomingDamage);
                    break;
                case 3:
                    _bodyPartHealth -= _incomingDamage;
                    _characterControls.SendDamage(_incomingDamage);
                    break;
            }
        }
        if (_bodyPartHealth <= 0)
        {
            _characterControls.CallForDeath();
        }
    }

    void CalculateStun()
    {
        _healthPCT = _bodyPartHealth / _maxBodyPartHealth;
        _baseStunChance = .1f;
        _stunChance = _baseStunChance + (1 - _healthPCT) * .09f;
    }

    IEnumerator ResetHP()
    {
        yield return _stunTimer;
        _bodyPartHealth = _maxBodyPartHealth;
        _resetHPCoroutine = null;
    }
}

