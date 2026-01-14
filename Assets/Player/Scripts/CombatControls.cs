using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    float _comboDelay = 1f;
    int _maxComboInput = 3;
    int _currentComboInput = 0;
    bool _nextAttackReady = false;

    [SerializeField] Image _image;


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
        _attackButton.action.performed += OnAttack;
    }

    private void Update()
    {
        //if(_anim.GetBool("INCOMBO") == true && Time.time < _lastAttackEnd && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
        //{
        //    if(_currentComboInput < _maxComboInput)
        //    _nextAttackReady = true;
        //}
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .9f && Time.time > _lastAttackEnd && _anim.GetBool("INCOMBO") == true) 
        {
            ComboEnd();
        }

        if(_nextAttackReady == true)
        {
            _image.gameObject.SetActive(true);
        }
        else
        {
            _image.gameObject.SetActive(false);
        }
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
            _lastAttackEnd = Time.time + _comboDelay;
            _canStartCombo = false;
        }
        else if (_nextAttackReady == true)
        {
            if(_currentComboInput == 1) //&& _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            {
                _basicControls.StopMovement();
                _basicControls.DeactivateJump();
                _nextAttackReady = false;
                _anim.SetTrigger("ATTACK2");
                _currentComboInput++;
                _lastAttackEnd = Time.time + _comboDelay;
            }
            else if (_currentComboInput == 2) //&& _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .6f)
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
        TurnOffAttack();
        DeactivateCombo();
        Invoke("TurnOnAttack", _comboDelay);
    }

    public void NextAttackReady()
    {
        _nextAttackReady = true;
    }

    public void NextAttackEnd()
    {
        _nextAttackReady = false;
    }

    void TurnOffAttack()
    {
        _attackButton.action.performed -= OnAttack;
    }

    void TurnOnAttack()
    {
        _attackButton.action.performed += OnAttack;
    }

    private void OnDestroy()
    {
        _attackButton.action.performed -= OnAttack;
    }
}