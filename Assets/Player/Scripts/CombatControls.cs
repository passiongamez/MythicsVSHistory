using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatControls : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    BasicCharacterControls _basicControls;
    Animator _anim;

    int _power;
    float _cooldown = 1.25f;
    bool _canAttack = true;
    WaitForSeconds _attackWait;

    [SerializeField] InputActionReference _attackButton;

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

    void OnAttack(InputAction.CallbackContext ctx)
    {
        if (_canAttack)
        {
            _anim.SetTrigger("Attack1");
            _canAttack = false;
            _basicControls.StopMovement();
            _basicControls.DeactivateJump();
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return _attackWait;
        _canAttack = true;
        _anim.ResetTrigger("Attack1");
        _basicControls.ResumeMovement();
        _basicControls.ActivateJump();
    }
}
