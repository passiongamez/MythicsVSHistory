using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAttacks : MonoBehaviour
{
    InputActionMap _paletteControls;

    private void Awake()
    {
        _paletteControls = new InputActionMap();
        _paletteControls.Disable();
    }

    public void EnablePaletteControls()
    {
        _paletteControls.Enable();
        Debug.Log("Palette controls enabled");
        _paletteControls.FindAction("Special1").performed += SP1;
        _paletteControls.FindAction("Special2").performed += SP2;
        _paletteControls.FindAction("Special3").performed += SP3;
        _paletteControls.FindAction("Special4").performed += SP4;
    }

    public void DisablePaletteControls()
    {
        _paletteControls.Disable();
        Debug.Log("Palette controls disabled");
        _paletteControls.FindAction("Special1").performed -= SP1;
        _paletteControls.FindAction("Special2").performed -= SP2;
        _paletteControls.FindAction("Special3").performed -= SP3;
        _paletteControls.FindAction("Special4").performed -= SP4;
    }

    void SP1(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 1 performed");
        DisablePaletteControls();
    }

    void SP2(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 2 performed");
        DisablePaletteControls();
    }

    void SP3(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 3 performed");
        DisablePaletteControls();
    }

    void SP4(InputAction.CallbackContext ctx)
    {
        Debug.Log("Special 4 performed");
        DisablePaletteControls();
    }

    void Cooldown()
    {

    }
}
