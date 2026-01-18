using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CombatControls : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    BasicCharacterControls _basicControls;
    Animator _anim;

    public event Action OnPaletteMapEnable;
    public event Action OnPaletteMapDisable;

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

    [SerializeField] InputActionReference _ultimate;
    bool _usingUltimate = false;
    bool _canUseUltimate = true;

    [SerializeField] InputActionReference _palleteWheel;
    bool _paletteWheelOpen = false;

    bool _attackButtonReady = true;

    [SerializeField] InputActionReference _sp1Button;
    bool _canDoSP1 = true;

    [SerializeField] InputActionReference _sp2Button;
    bool _canDoSP2 = true;

    [SerializeField] InputActionReference _sp3Button;
    bool _canDoSP3 = true;

    [SerializeField] InputActionReference _sp4Button;
    bool _canDoSP4 = true;

    bool _isAttacking = false;
    Coroutine _isAttackingChange;

    float _sp1CD = 10f, _sp2CD = 20f, _sp3CD = 30f, _sp4CD = 45f, _ultimateCD = 60f;

    [SerializeField] GameObject _image;


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
        _ultimate.action.performed += UltimateAttack;
        _palleteWheel.action.started += OnPaletteButton;
        _sp1Button.action.performed += SP1;
        _sp2Button.action.performed += SP2;
        _sp3Button.action.performed += SP3;
        _sp4Button.action.performed += SP4;
    }

    private void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .85f && Time.time > _lastAttackEnd && _anim.GetBool("INCOMBO") == true) 
        {
            ComboEnd();
        }

        if(_ultimate.action.phase == InputActionPhase.Performed)
        {
            TurnOffAttack();
        }
        if(_ultimate.action.phase == InputActionPhase.Canceled)
        {
            TurnOnAttack();
        }

        if (_usingUltimate == true)
        {
            if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                _usingUltimate = false;
            }
        }
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        if(_usingUltimate == false && _paletteWheelOpen == false)
        {
            if(_anim.GetBool("ISRUNNING") == false)
            {
                if (_anim.GetBool("INCOMBO") == false && _canStartCombo == true)
                {
                    _basicControls.StopMovement();
                    _basicControls.DeactivateJump();
                    _isAttacking = true;
                    _anim.SetTrigger("ATTACK1");
                    _anim.SetBool("INCOMBO", true);
                    _lastAttackEnd = Time.time + _comboDelay;
                    _canStartCombo = false;
                    _currentComboInput++;
                }
                else if (_nextAttackReady == true && _anim.GetBool("INCOMBO") == true)
                {
                    if (_currentComboInput == 1)
                    {
                        _nextAttackReady = false;
                        _basicControls.StopMovement();
                        _basicControls.DeactivateJump();
                        _anim.SetTrigger("ATTACK2");
                        _lastAttackEnd = Time.time + _comboDelay;
                        _currentComboInput++;
                    }
                    else if (_currentComboInput == 2)
                    {
                        _nextAttackReady = false;
                        _basicControls.StopMovement();
                        _basicControls.DeactivateJump();
                        _anim.SetTrigger("ATTACK3");
                        _currentComboInput++;
                    }
                }
            }
            else
            {
                _isAttacking = true;
                _anim.SetTrigger("RUNNINGATTACK");
                _basicControls.DeactivateJump();
                Invoke("RunningAttackReset", 1f);
            }
        }
    }

    void RunningAttackReset()
    {
        _isAttacking = false;
        _anim.ResetTrigger("RUNNINGATTACK");
        _basicControls.ActivateJump();
    }

        void DeactivateCombo()
    {
        _canStartCombo = true;
        _nextAttackReady = false;
        _isAttacking = false;
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

    public void TurnOnAttack()
    {
        _attackButton.action.performed += OnAttack;
    }

    void UltimateAttack(InputAction.CallbackContext ctx)
    {
        if (_canUseUltimate && _isAttacking == false)
        {
            Debug.Log("Ultimate performed");
            _usingUltimate = true;
            _canUseUltimate = false;
            _anim.SetTrigger("ULTIMATE");
            _image.gameObject.SetActive(true);
            Invoke("UltimateCD", _ultimateCD);
        }
    }

    void UltimateCD()
    {
        _anim.ResetTrigger("ULTIMATE");
        _canUseUltimate = true;
    }


    void OnPaletteButton(InputAction.CallbackContext ctx)
    {
        if(_attackButtonReady == true)
        {
            Debug.Log("Palette wheel opened");
            OnPaletteMapEnable?.Invoke();
            TurnOffAttack();
            _basicControls.DeactivateJump();
            _paletteWheelOpen = true;
            _image.gameObject.SetActive(true);
            _attackButtonReady = false;
        }
        else
        {
            Debug.Log("Palette wheel closed");
            OnPaletteMapDisable?.Invoke();
            TurnOnAttack();
            _basicControls.ActivateJump();
            _paletteWheelOpen = false;
            _image.gameObject.SetActive(false);
            _attackButtonReady = true;
        }
    }

    void SP1(InputAction.CallbackContext ctx)
    {
        if(_paletteWheelOpen == true && _canDoSP1 == true && _isAttacking == false)
        {
            Debug.Log("Special 1 performed");
            _anim.SetTrigger("SP1");
            _canDoSP1 = false;
            _paletteWheelOpen = false;
            _image.gameObject.SetActive(false);
            _basicControls.ActivateJump();
            _attackButtonReady = true;
            TurnOnAttack();
            Invoke("SP1CD", _sp1CD);
        }
    }

    void SP1CD()
    {
        _isAttacking = false;
        _anim.ResetTrigger("SP1");
        _canDoSP1 = true;
    }

    void SP2(InputAction.CallbackContext ctx)
    {
        if (_paletteWheelOpen == true && _canDoSP2 == true && _isAttacking == false)
        {
            Debug.Log("Special 2 performed");
            _anim.SetTrigger("SP2");
            _canDoSP2 = false;
            _paletteWheelOpen = false;
            _image.gameObject.SetActive(false);
            _basicControls.ActivateJump();
            _attackButtonReady = true;
            TurnOnAttack();
            Invoke("SP2CD", _sp2CD);
        }
    }

    void SP2CD()
    {
        _isAttacking = false;
        _anim.ResetTrigger("SP2");
        _canDoSP2 = true;
    }

    void SP3(InputAction.CallbackContext ctx)
    {
        if (_paletteWheelOpen == true && _canDoSP3 == true && _isAttacking == false)
        {
            Debug.Log("Special 3 performed");
            _anim.SetTrigger("SP3");
            _canDoSP3 = false;
            _paletteWheelOpen = false;
            _image.gameObject.SetActive(false);
            _basicControls.ActivateJump();
            _attackButtonReady = true;
            TurnOnAttack();
            Invoke("SP3CD", _sp3CD);
        }
    }

    void SP3CD()
    {
        _isAttacking = false;
        _anim.ResetTrigger("SP3");
        _canDoSP3 = true;
    }

    void SP4(InputAction.CallbackContext ctx)
    {
        if (_paletteWheelOpen == true && _canDoSP4 == true && _isAttacking == false)
        {
            Debug.Log("Special 4 performed");
            _anim.SetTrigger("SP4");
            _canDoSP4 = false;
            _paletteWheelOpen = false;
            _image.gameObject.SetActive(false);
            _basicControls.ActivateJump();
            _attackButtonReady = true;
            TurnOnAttack();
            Invoke("SP4CD", _sp4CD);
        }
    }

    void SP4CD()
    {
        _isAttacking = false;
        _anim.ResetTrigger("SP4");
        _canDoSP4 = true;
    }

    public void IsAttackingToggle()
    {
        _isAttacking = !_isAttacking;
    }

    private void OnDestroy()
    {
        _attackButton.action.performed -= OnAttack;
        _ultimate.action.performed -= UltimateAttack;
        _palleteWheel.action.started -= OnPaletteButton;
    }
}