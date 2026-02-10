using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    public enum EnemyState
    {
        Idle,
        Search,
        Chase,
        Attack,
        Death
    }

    EnemyState _currentState;
    Animator _animator;
    NavMeshAgent _agent;

    //id 0 = aggressive, id 1 = strategic, id 2 = support, id 3 = defensive
    [SerializeField] int _personalityID;

    [Header("Stats")]
    float _maxHP;
    float _power;
    float _defense;
    float _speed;
    float _currentHP;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _maxHP = _stats.health;
        _power = _stats.power;
        _defense = _stats.defense;
        _speed = _stats.speed;

        _currentHP = _maxHP;
        _agent.speed = _speed;
    }

    private void Start()
    {
        _currentState = EnemyState.Idle;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Idle:
                _animator.SetBool("ISMOVING", false);
                break;
            case EnemyState.Search:
                _animator.SetBool("ISMOVING", true);
                break;
            case EnemyState.Chase:
                _animator.SetBool("ISMOVING", true);
                break;
            case EnemyState.Attack:
                _animator.SetBool("ISATTACKING", true);
                break;
            case EnemyState.Death:
                _animator.SetTrigger("ISDEAD");
                break;
        }
    }
}
