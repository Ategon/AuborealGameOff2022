using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [field: Range(1, 100)]
    [field: SerializeField]
    public float PlayerSpeed { get; private set; }

    private Inputs _inputs;
    private Vector2 _playerDirection;
    private Rigidbody2D _playerRidigbody;

    private void Awake()
        => Init();

    private void OnEnable()
        => InitCallbacks(true);

    private void OnDisable()
        => InitCallbacks(false);

    private void Init()
    {
        _inputs = new Inputs();
        TryGetComponent(out _playerRidigbody);
    }

    private void InitCallbacks(bool enable)
    {
        if (enable)
        {
            _inputs.Enable();
            _inputs.Player.Move.performed += OnMovement;
        }
        else
        {
            _inputs.Disable();
            _inputs.Player.Move.performed -= OnMovement;
        }
    }

    private void OnMovement(InputAction.CallbackContext movementCxt)
    {
        _playerDirection = movementCxt.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var targetPosition = _playerRidigbody.position + _playerDirection * (PlayerSpeed * Time.fixedDeltaTime);
        _playerRidigbody.MovePosition(targetPosition);
    }
}