using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatControls : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    BasicCharacterControls _basicControls;
    Animator _anim;

    int _power;
    float _cooldown = .25f;
    bool _canStartCombo = true;
    WaitForSeconds _attackWait;

    [Header("ComboSystem")]
    [SerializeField] InputActionReference _attackButton;
    float _lastAttackEnd;
    float _comboTimeout = .85f;
    int _maxComboInput = 3;
    int _currentComboInput = 0;
    bool _nextAttackReady = false;


    private void Awake()
    {
        _anim = GetComponent<Animator>(); 
        
        if(_anim == null)
        {
            Debug.Log("Animator is null");
        }

        if(_stats == null)
        {
            Debug.Log("Stats are null");
        }

        _basicControls = GetComponent<BasicCharacterControls>();

        if(_basicControls == null)
        {
            Debug.Log("BasicCharacterControls is null");
        }

        _power = _stats.power;

        _attackWait = new WaitForSeconds(_cooldown);
        _attackButton.action.started += OnAttack;
    }

    private void Update()
    {
        if(_anim.GetBool("INCOMBO") == true && Time.time < _lastAttackEnd && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f)
        {
            _nextAttackReady = true;
        }
        else if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && Time.time > _lastAttackEnd && _anim.GetBool("INCOMBO") == true) 
        {
            ComboEnd();
        }

        Debug.Log($"Timer check: {Time.time} vs {_lastAttackEnd}");
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        if (_anim.GetBool("INCOMBO") == false && _canStartCombo == true)
        {
            _basicControls.StopMovement();
            _basicControls.DeactivateJump();
            _anim.SetTrigger("ATTACK1");
            _anim.SetBool("INCOMBO", true);
            _currentComboInput++;
            _lastAttackEnd = Time.time + _comboTimeout;
            _canStartCombo = false;
        }
        else if (_nextAttackReady == true)
        {
            if(_currentComboInput == 1)
            {
                _basicControls.StopMovement();
                _basicControls.DeactivateJump();
                _nextAttackReady = false;
                _anim.SetTrigger("ATTACK2");
                _currentComboInput++;
                _lastAttackEnd = Time.time + _comboTimeout;
            }
            else if (_currentComboInput == 2)
            {
                _basicControls.StopMovement();
                _basicControls.DeactivateJump();
                _nextAttackReady = false;
                _anim.SetTrigger("ATTACK3");
                _currentComboInput++;
            }
        }
    }

        void DeactivateCombo()
    {
        _canStartCombo = true;
        _nextAttackReady = false;
        _anim.ResetTrigger("ATTACK1");
        _anim.ResetTrigger("ATTACK2");
        _anim.ResetTrigger("ATTACK3");
        _currentComboInput = 0;
        _lastAttackEnd = 0;
        _basicControls.ResumeMovement();
        _basicControls.ActivateJump();
    }

    void ComboEnd()
    {
        _anim.SetBool("INCOMBO", false);
        DeactivateCombo();
    }

    private void OnDestroy()
    {
        _attackButton.action.started -= OnAttack;
    }
}