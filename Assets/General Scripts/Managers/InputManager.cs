using UnityEngine.InputSystem;
using UnityEngine;
using GameControlMaster;
using Unity.Entities;

public class InputManager : SingletonMaster<InputManager>
{
    CharacterControls _controls;

    public override void Initialize()
    {
        _controls = new CharacterControls();
        //EnableUIControls();
    }

    public void CallToEnableAController(int index)
    {
        switch (index)
        {
            case 0:
                DisableUIControls();
                DisableTeamModeControls();
                EnableGameplayControls();
                break;
            case 1:
                DisableGameplayControls();
                DisableTeamModeControls();
                EnableUIControls();
                break;
            case 2:
                DisableGameplayControls();
                DisableUIControls();
                EnableTeamModeControls();
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

    private void OnDestroy()
    {
        if(_controls != null)
        {
            DisableGameplayControls();
            DisableUIControls();
            DisableTeamModeControls();
            _controls.Dispose();
        }
    }
}
