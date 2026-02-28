using UnityEngine;
using UnityEngine.AI;

public class AIAlly : AIBase
{
    [SerializeField] GameObject _player, _enemy;
    [SerializeField] float _repathDistance = 0.3f;
    [SerializeField] float _walkThreshold = 0.05f;
    private Vector3 _lastPlayerPos;

    [SerializeField] float _attackRange = 2f;
    [SerializeField] float _attackCooldown = 1.2f;

    private float _attackTimer;
    private float _patrolTimer;
    protected override void Start()
    {
        base.Start();
        if (_player != null)
        {
            _lastPlayerPos = _player.transform.position;
            _agent.SetDestination(_lastPlayerPos); // initial follow
        }
        ChangeState(EnemyState.Search);
    }
    protected override void OnEnterState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Search:

                _animator.SetBool("isChase", false);
                break;
            // case EnemyState.Chase:


            //     break;
            case EnemyState.Attack:
                _agent.isStopped = true;
                _animator.SetBool("isWalking", false);
                //_animator.SetTrigger("Attack");
                break;

        }
    }

    protected override void OnUpdateState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Search:

                bool flowControl = followPlayer();
                if (!flowControl)
                {
                    return;
                }
                if (_enemy != null)
                {
                    float dist = Vector3.Distance(transform.position, _enemy.transform.position);
                    if (dist <= _attackRange)
                    {
                        ChangeState(EnemyState.Attack);
                    }
                }

                break;

            // case EnemyState.Chase:

            //     break;
            case EnemyState.Attack:
                HandleAttack();
                break;
        }

        bool followPlayer()
        {
            if (_player == null || _agent == null || !_agent.isOnNavMesh) return false;

            Vector3 playerPos = _player.transform.position;

            if (Vector3.Distance(playerPos, _lastPlayerPos) > _repathDistance)
            {
                _lastPlayerPos = playerPos;
                _agent.SetDestination(playerPos);
            }

            bool hasReached =
                !_agent.pathPending &&
                _agent.remainingDistance <= _agent.stoppingDistance + 0.05f;

            bool isWalking = !hasReached && _agent.velocity.magnitude > 0.05f;

            _animator.SetBool("isWalking", isWalking);
            return true;
        }
    }
    private void HandleAttack()
    {
        if (_enemy == null)
        {
            ChangeState(EnemyState.Search);
            return;
        }

        // Face enemy
        Vector3 dir = (_enemy.transform.position - transform.position).normalized;
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);

        float dist = Vector3.Distance(transform.position, _enemy.transform.position);

        // If enemy moved away, go back to Search
        if (dist > _attackRange + 0.5f)
        {
            ChangeState(EnemyState.Search);
            return;
        }

        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0f)
        {
            _attackTimer = _attackCooldown;
            PerformAttack();
        }
    }
    private void PerformAttack()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                SP1();
                break;

            case 1:
                SP2();
                break;

            case 2:
                UltimateAbility();
                break;
        }
    }
    protected override void SP1()
    {
        Debug.Log("Ally used SP1");
        _animator.SetTrigger("isAttack1");
    }

    protected override void SP2()
    {
        Debug.Log("Ally used SP2");
        _animator.SetTrigger("isAttack2");
    }

    protected override void UltimateAbility()
    {
        Debug.Log("Ally used Ultimate");
        _animator.SetTrigger("isAttack3");
    }

    protected override void OnExitState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Search:
                _animator.SetBool("isWalking", false);

                break;
            case EnemyState.Chase:
                _animator.SetBool("isChase", false);

                break;
            case EnemyState.Attack:
                _agent.isStopped = false;
                break;
        }
    }

}
