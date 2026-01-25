using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCharacterControls : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    Animator _anim;
    CharacterController _characterController;

    [Header("InputActionReference")]
    [SerializeField] InputActionReference _moveReference;
    Vector2 _moveAction;
    Vector3 _movement;
    Quaternion _rotation;

    [SerializeField] InputActionReference _jumpButton;

    float _gravity = -20f;
    float _verticalVelocity = 0f;
    float _groundCheckDistance = .1f;
    LayerMask _groundLayerMask = ~0;
    float _rayOrigin;
    Vector3 _verticalMovement;

    [Header("Stats")]
    float _speed;
    [SerializeField] float _rotationSpeed = 10f;


    float _health;
    float _currentHealth;

    [Header("Jump")]
    bool _isGrounded = true;
    [SerializeField] float _jumpForce = 10f;
    Vector3 _jumpHeight;
    float _jumpPos;
    bool _canJump = true;
    WaitForSeconds _jumpCooldown;
    float _jumpCooldownTime = 1f;
    Coroutine _jumpCoroutine;
    Coroutine _landingCoroutine;

    Coroutine _deathRoutine;
    WaitForSeconds _deathWait;
    bool _isDead = false;


    private void Awake()
    {
        _speed = _stats.speed;
        _health = _stats.health;
        _currentHealth = _health;
        _characterController = GetComponent<CharacterController>();
        _characterController.stepOffset = 0.15f;
        _jumpCooldown = new WaitForSeconds(_jumpCooldownTime);
        _jumpButton.action.started +=  OnJump;
        _deathWait = new WaitForSeconds(2.5f);

        if( _characterController == null)
        {
            Debug.Log("Character controller is null");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.Log("Animator is null");
        }
    }

    private void Start()
    {
        InputManager.Instance.CallToEnableAController(0);
    }

    private void Update()
    {
        CheckGrounded();


        if(_isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -1f;
        }

        if (!_isGrounded)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        if(_moveReference != null && _moveReference.action.enabled == true)
        {
            OnMove();
        }


        _verticalMovement = Vector3.up * _verticalVelocity;
        _characterController.Move(_verticalMovement * Time.deltaTime);
    }

    private void OnMove()
    {
        if (_isGrounded)
        {
            _moveAction = _moveReference.action.ReadValue<Vector2>();
            _movement = new Vector3(_moveAction.x, 0, _moveAction.y).normalized * _speed;

            _characterController.Move(_movement * Time.deltaTime);
        }

        if (_movement.sqrMagnitude > 0.01f)
        {
         _anim.SetBool("ISRUNNING", true);
         _rotation = Quaternion.LookRotation(_movement, Vector3.up);
         transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _anim.SetBool("ISRUNNING", false);
        }
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (_isGrounded == true && _canJump == true)
        {
            _anim.SetTrigger("JUMP");
            _verticalVelocity = _jumpForce;
            _isGrounded = false;
            _canJump = false;

            if (_jumpCoroutine == null)
            {
                _jumpCoroutine = StartCoroutine(JumpCooldown());
            }

            if(_landingCoroutine == null)
            {
                _landingCoroutine = StartCoroutine(Landing());
            }
            _anim.ResetTrigger("ISGROUNDED");
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return _jumpCooldown;
        _canJump = true;
        _anim.ResetTrigger("JUMP");
        _jumpCoroutine = null;
    }

    IEnumerator Landing()
    {
        while (!_isGrounded)
        {
            yield return null;
        }
        _anim.SetTrigger("ISGROUNDED");
        _landingCoroutine = null;
    }

    void CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, .1f, _groundLayerMask))
        {
            float maxAllowedDistance = (_characterController.height / 2f)
                                     - _characterController.skinWidth
                                     - _groundCheckDistance;

            _isGrounded = hit.distance <= maxAllowedDistance;

            if (Vector3.Dot(hit.normal, Vector3.up) < 0.7f)
            {
                _isGrounded = false;
            }
            else
            {
                _isGrounded = false;
            }

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f, _groundLayerMask))
            {
                // Only accept if almost vertical (helps avoid detecting walls as ground)
                if (Vector3.Dot(hit.normal, Vector3.up) > 0.7f) // normal is mostly upward
                {
                    _isGrounded = true;
                    return;
                }
            }

#if UNITY_EDITOR
            Color rayColor = _isGrounded ? Color.green : Color.red;
            Debug.DrawRay(transform.position, Vector3.down * .1f, rayColor);

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitDebug, .1f, _groundLayerMask))
            {
                Debug.DrawRay(transform.position, Vector3.down * hitDebug.distance, Color.yellow);
            }
#endif
        }
    }

    public void StopMovement()
    {
        _moveReference.action.Disable();
    }

    public void ResumeMovement()
    {
        _moveReference.action.Enable();
    }

    public void DeactivateJump()
    {
        _jumpButton.action.Disable();
    }

    public void ActivateJump()
    {
        _jumpButton.action.Enable();
    }

    IEnumerator OnDeath()
    {
        Debug.Log("Character is dead");
        StopMovement();
        DeactivateJump();
        _anim.SetTrigger("DEATH");
        _deathRoutine = null;
        yield return _deathWait;
        Destroy(this.gameObject);
    }

    public void CallForDeath()
    {
        if (_deathRoutine == null && _isDead == false)
        {
            _isDead = true;
            _deathRoutine = StartCoroutine(OnDeath());
        }
        else
            return;
    }

    public void SendDamage(float damage)
    {
        SubtractHealth(damage);
    }

    void SubtractHealth(float damage)
    {
        if(_isDead == true)
        {
            return;
        }
        else
            _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            if(_deathRoutine == null)
            {
                _isDead = true;
                _deathRoutine = StartCoroutine(OnDeath());
            }
        }
    }

    private void OnDestroy()
    {
        _jumpButton.action.started -= OnJump;
    }
}
