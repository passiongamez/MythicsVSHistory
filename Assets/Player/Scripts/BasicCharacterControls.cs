using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCharacterControls : MonoBehaviour
{
    [SerializeField] CharacterBaseStats _stats;
    Animator _anim;

    [SerializeField] InputActionReference _moveReference;
    Vector2 _moveAction;
    Vector3 _movement;

    CharacterController _characterController;

    float _speed;

    private void Awake()
    {
        _speed = _stats.speed;
        _characterController = GetComponent<CharacterController>();

        if( _characterController == null)
        {
            Debug.Log("Character controller is null");
        }
    }

    private void Start()
    {
        InputManager.Instance.CallToEnableAController(0);
    }

    private void Update()
    {
        if(_moveReference != null && _moveReference.action.enabled == true)
        {
            OnMove();
        }
    }

    private void OnMove()
    {
        _moveAction = _moveReference.action.ReadValue<Vector2>();
        _movement = new Vector3(_moveAction.x, 0, _moveAction.y) * _speed;
        Vector3.ClampMagnitude(_movement, 1f);


        _characterController.Move(_movement * Time.deltaTime);
    }
}
