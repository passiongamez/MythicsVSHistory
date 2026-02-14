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

    protected CapsuleCollider _collider;
    protected float _height;

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

    [Header("Sight Settings")]
    protected float _coneAngle = 90f;
    protected float _sightRange = 15f;
    [SerializeField] protected LayerMask _playerLayer;
    protected string _playerTag = "Player";
    protected float[] _rayHeights;
    protected float _updateInterval = 0.2f;
    protected float _updateTimer;

    protected Transform _transfrom;

    public GameObject _detectedTarget { get; protected set; }
    public bool _canSeeTarget { get; protected set; }

    protected int _rayCount = 5;
    protected Vector3 _rayOrigin;
    protected Vector3 _rayDirection;
    protected float _angleOffset;

    protected virtual void Awake()
    {
        _transfrom = transform;

        _collider = GetComponent<CapsuleCollider>();
        _height = _collider.height;

        _rayHeights = new float[3];
        _rayHeights[0] = _height - _height;
        _rayHeights[1] = _height / 2;
        _rayHeights[2] = _height;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        _updateTimer += Time.deltaTime;









        if (_updateTimer < _updateInterval) return;
        _updateTimer = 0;

        RayFOV();
    }

    protected virtual void RayFOV()
    {
        _canSeeTarget = false;
        _detectedTarget = null; 

        foreach(float height in _rayHeights)
        {
            _rayOrigin = _transfrom.position + _transfrom.up * height;

            for(int i = 0; i < _rayCount; i++)
            {
                _angleOffset = ((float)i / (_rayCount - 1) - .5f) * _coneAngle;

                _rayDirection = Quaternion.Euler(0, _angleOffset, 0) * _transfrom.forward;

                if(Physics.Raycast(_rayOrigin, _rayDirection, out RaycastHit hit, _sightRange, _playerLayer))
                {
                    if (hit.collider.CompareTag(_playerTag))
                    {
                        _detectedTarget = hit.collider.gameObject;
                        _canSeeTarget = true;
                        return;
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _canSeeTarget ? Color.green : Color.red;

        foreach (float height in _rayHeights)
        {
            Vector3 origin = _transfrom.position + _transfrom.up * height;

            // Draw cone outline
            Vector3 left = Quaternion.Euler(0, -_coneAngle / 2f, 0) * _transfrom.forward * _sightRange;
            Vector3 right = Quaternion.Euler(0, _coneAngle / 2f, 0) * _transfrom.forward * _sightRange;
            Gizmos.DrawRay(origin, left);
            Gizmos.DrawRay(origin, right);
            Gizmos.DrawRay(origin, _transfrom.forward * _sightRange);
        }
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
