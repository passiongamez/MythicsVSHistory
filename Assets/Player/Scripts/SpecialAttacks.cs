using GameControlMaster;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAttacks : MonoBehaviour
{
    CombatControls _combatControls;

    private void Awake()
    {
        _combatControls = GetComponent<CombatControls>();

        if(_combatControls == null)
        {
            Debug.Log("Combat controls are null");
        }
    }

    void SP1(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 1 performed");
        _combatControls.TurnOnAttack();
        InputManager.Instance.CallToEnableAController(0);
    }

    void SP2(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 2 performed");
        _combatControls.TurnOnAttack();
        InputManager.Instance.CallToEnableAController(0);
    }

    void SP3(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 3 performed");
        _combatControls.TurnOnAttack();
        InputManager.Instance.CallToEnableAController(0);
    }

    void SP4(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 4 performed");
        _combatControls.TurnOnAttack();
        InputManager.Instance.CallToEnableAController(0);
    }

    void Cooldown()
    {

    }
}
