using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    [SerializeField] protected CharacterBaseStats _stats;
    protected Animator _animator;
    protected NavMeshAgent _agent;

    [Header("Stats")]
    protected float _maxHP;
    protected float _power;
    protected float _defense;
    protected float _speed;
    protected float _currentHP;
    protected int _level;
    protected float _attackMultiplier;
    protected float _tempPower;
    protected float _defenseMultiplier;
    protected float _tempDefense;
    protected float _tempSpeed;

    protected Collider _collider;

    public enum EnemyState
    {
        Idle,
        Search,
        Chase,
        Attack,
        Death
    }

    protected EnemyState _currentState;
    protected EnemyState _previousState;

    public enum PersonalityType
    {
        Aggressive,
        Strategic,
        Support,
        Defensive
    }

    protected EnemyState _personality;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    protected virtual void PassiveAbility()
    {

    }

    protected virtual void BasicAttack1()
    {

    }

    protected virtual void BasicAttack2()
    {

    }

    protected virtual void ComboEnder1()
    {

    }

    protected virtual void BasicAttack3()
    {

    }

    protected virtual void ComboEnder2()
    {

    }

    protected virtual void SP1()
    {

    }

    protected virtual void SP2()
    {

    }

    protected virtual void SP3()
    {

    }
     protected virtual void SP4()
    {

    }

    protected virtual void UltimateAbility()
    {

    }
}
