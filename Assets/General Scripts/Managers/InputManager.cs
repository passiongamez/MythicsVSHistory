using UnityEngine.InputSystem;
using UnityEngine;
using GameControlMaster;
using Unity.Entities;
using System;

public class InputManager : SingletonMaster<InputManager>
{
    CharacterControls _controls;
    CombatControls _combatControls;

    public event Action<int> OnSpecialPerformed;

    public override void Initialize()
    {
        _combatControls = FindFirstObjectByType<CombatControls>();

        if( _combatControls != null)
        {
            _combatControls.OnPaletteMapEnable += EnableAttackPaletteWheel;
            _combatControls.OnPaletteMapDisable += DisableAttackPaletteWheel;
        }

        _controls = new CharacterControls();
        //EnableUIControls();
    }

    public void ActivateSpecial(int special)
    {
        if(_controls.SpecialAttacks.enabled == true)
        {
            
        }
    }


    public void CallToEnableAController(int index)
    {
        switch (index)
        {
            case 0:
                DisableUIControls();
                DisableTeamModeControls();
                DisableAttackPaletteWheel();
                EnableGameplayControls();
                break;
            case 1:
                DisableGameplayControls();
                DisableTeamModeControls();
                DisableAttackPaletteWheel();
                EnableUIControls();
                break;
            case 2:
                DisableGameplayControls();
                DisableUIControls();
                DisableAttackPaletteWheel();
                EnableTeamModeControls();
                break;
            case 3:
                //DisableTeamModeControls();
                DisableUIControls();
                EnableAttackPaletteWheel();
                break;
        }
    }

    void EnableGameplayControls()
    {
        if (_controls.Gameplay.enabled == false && _controls != null)
        {
            _controls.Gameplay.Enable();
            Debug.Log("Gameplay controls enabled");
        }
    }

    void DisableGameplayControls()
    {
        if (_controls.Gameplay.enabled == true && _controls != null)
        {
            _controls.Gameplay.Disable();
            Debug.Log("Gameplay controls disabled");
        }
    }

    void EnableUIControls()
    {
        if (_controls.UIControls.enabled == false && _controls != null)
        {
            _controls.UIControls.Enable();
            Debug.Log("UI controls enabled");
        }
    }

    void DisableUIControls()
    {
        if (_controls.UIControls.enabled == true && _controls != null)
        {
            _controls.UIControls.Disable();
            Debug.Log("UI controls disabled");
        }
    }

    void EnableTeamModeControls()
    {
        if (_controls.TeamMode.enabled == false && _controls != null)
        {
            _controls.TeamMode.Enable();
            Debug.Log("Team controls enabled");
        }
    }

    void DisableTeamModeControls()
    {
        if (_controls.TeamMode.enabled == true && _controls != null)
        {
            _controls.TeamMode.Disable();
            Debug.Log("Team controls disabled");
        }
    }

    void EnableAttackPaletteWheel()
    {
        _controls.SpecialAttacks.Enable();
        Debug.Log("Attack palette enabled");
    }

    void DisableAttackPaletteWheel()
    {
        _controls.SpecialAttacks.Disable();
        Debug.Log("Attack palette wheel disabled");
    }

    private void OnDestroy()
    {
        if(_controls != null)
        {
            DisableGameplayControls();
            DisableUIControls();
            DisableTeamModeControls();
            _controls.Dispose();
        }

        _combatControls.OnPaletteMapEnable -= EnableAttackPaletteWheel;
        _combatControls.OnPaletteMapDisable -= DisableAttackPaletteWheel;
    }
}
