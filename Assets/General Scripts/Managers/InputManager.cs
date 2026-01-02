using UnityEngine.InputSystem;
using UnityEngine;
using GameControlMaster;
using Unity.Entities;

public class InputManager : SingletonMaster<InputManager>
{
    [SerializeField] CharacterControls _controls;

    void Awake()
    {
        _controls = new CharacterControls();
        EnableUIControls();
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
        _controls.Gameplay.Enable();
        Debug.Log("Gameplay controls enabled");
    }

    void DisableGameplayControls()
    {
        _controls.Gameplay.Disable();
        Debug.Log("Gameplay controls disabled");
    }

    void EnableUIControls()
    {
        _controls.UIControls.Enable(); 
        Debug.Log("UI controls enabled");
    }

    void DisableUIControls()
    {
        _controls.UIControls.Disable();
        Debug.Log("UI controls disabled");
    }

    void EnableTeamModeControls()
    {
        _controls.TeamMode.Enable();
        Debug.Log("Team controls enabled");
    }

    void DisableTeamModeControls()
    {
        _controls.TeamMode.Disable();
        Debug.Log("Team controls disabled");
    }

    private void OnDisable()
    {
        DisableGameplayControls();
        DisableTeamModeControls();
    }
}
