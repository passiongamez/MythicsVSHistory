using UnityEngine;

public class BasicCharacterControls : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    Animator _anim;

    float _speed;

    private void Awake()
    {
        _speed = _stats.speed;
    }

    private void Start()
    {
        InputManager.Instance.CallToEnableAController(0);
    }

    private void OnMove()
    {
        
    }
}
