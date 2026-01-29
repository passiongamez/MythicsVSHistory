using UnityEngine;

public class FOVCone : MonoBehaviour
{
    [Header("FOV Settings")]
    [SerializeField] float fovAngle = 90f;
    [SerializeField] float viewDistance = 10f;
    [SerializeField] LayerMask targetMask;
    [SerializeField] string _playerTag = "Player";

    float _tickRate = 0.2f;
    float _updateTimer;

    public GameObject detectedTarget;
    public bool targetInSight;

    Transform _agentTransform;

    Collider[] _1v1Array = new Collider[5];
    int _targetsInRange;
    Collider _target;

    Vector3 _directionToTarget;
    float _angle;

    float _distanceToTarget;

    Vector3 _leftRay;
    Vector3 _rightRay;

    private void Awake()
    {
        _agentTransform = transform;
    }


    // Update is called once per frame
    void Update()
    {
      _updateTimer += Time.deltaTime;

        if (_updateTimer < _tickRate) return;

        _updateTimer = 0;
        DetectTargets();
    }

    void DetectTargets()
    {
        targetInSight = false; 
        detectedTarget = null;

        _targetsInRange = Physics.OverlapSphereNonAlloc(_agentTransform.position, viewDistance, _1v1Array, targetMask);
        if(_targetsInRange == 0) return;

        Debug.Log($"Overlap found {_targetsInRange} hits. TargetMask value: {targetMask.value}");

        for (int i = 0; i < _targetsInRange; i++)
        {
            _target = _1v1Array[i];

            if (!_target.CompareTag(_playerTag)) continue;

            _directionToTarget = (_target.transform.position - _agentTransform.position).normalized;
            _angle = Vector3.Angle(_agentTransform.forward, _directionToTarget);

            if(_angle < fovAngle / 2f)
            {
                _distanceToTarget = Vector3.Distance(_agentTransform.position, _target.transform.position);

                if(_distanceToTarget <= viewDistance)
                {
                    if(Physics.Raycast(_agentTransform.position, _directionToTarget, out RaycastHit hit, viewDistance))
                    {
                        if(hit.collider == _target)
                        {
                            detectedTarget = _target.gameObject;
                            targetInSight = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = targetInSight ? Color.green : Color.red;
        Gizmos.DrawWireSphere(_agentTransform.position, viewDistance);

        // Draw cone
        _leftRay = Quaternion.Euler(0, -fovAngle / 2f, 0) * _agentTransform.forward * viewDistance;
        _rightRay = Quaternion.Euler(0, fovAngle / 2f, 0) * _agentTransform.forward * viewDistance;
        Gizmos.DrawRay(_agentTransform.position, _leftRay);
        Gizmos.DrawRay(_agentTransform.position, _rightRay);
        Gizmos.DrawRay(_agentTransform.position, _agentTransform.forward * viewDistance);
    }
}
