using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.SaveLoad;

namespace Characters.Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : SavableObject
    {
        [field: Range(1, 1000)]
        [field: SerializeField]
        public float PlayerSpeed { get; private set; }

        public Vector2 PlayerDirection { get { return _playerDirection; } }
        public Vector2 Aim { get { return _aim; } }

        private Inputs _inputs;
        private Vector2 _playerDirection;
        private PlayerDash _playerDash;
        private Rigidbody2D _playerRidigbody;
        private Vector2 _aim;
        [Header("References")]
        [SerializeField] private PlayerMeleeAttack _playerMeleeAttack;
        [SerializeField] private PlayerShootProjectile _playerShootProjectile;
        [SerializeField] private PlayerFootstepSound _playerFootstepSound;

        private void Awake()
            => Init();

        private void OnEnable()
            => InitCallbacks(true);

        private void OnDisable()
            => InitCallbacks(false);

        private void Init()
        {
            _inputs = new Inputs();
            TryGetComponent(out _playerDash);
            TryGetComponent(out _playerRidigbody);
        }

        private void InitCallbacks(bool enable)
        {
            if (enable)
            {
                _inputs.Enable();
                _inputs.Player.Move.performed += OnMovement;
                _inputs.Player.MousePosition.performed += OnAim;
            }
            else
            {
                _inputs.Disable();
                _inputs.Player.Move.performed -= OnMovement;
                _inputs.Player.MousePosition.performed -= OnAim;
            }
        }

        private void OnMovement(InputAction.CallbackContext movementCxt)
        {
            _playerDirection = movementCxt.ReadValue<Vector2>();
        }

        private void OnAim(InputAction.CallbackContext movementCxt)
        {
            _aim = movementCxt.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            if (_playerMeleeAttack.isAttacking)
            {
                Move(_playerMeleeAttack.attackDirection, _playerMeleeAttack.attackVelocity);
            }
            else if (_playerShootProjectile.isShooting)
            {

            }
            else
            {
                if (_playerDash.CanDash)
                {
                    _playerDash.Dash();
                }
                else
                {
                    Move(_playerDirection, PlayerSpeed);
                    if (_playerDirection != Vector2.zero)
                    {
                        _playerFootstepSound.isWalking = true;
                    }
                    else
                    {
                        _playerFootstepSound.isWalking = false;
                    }
                }
            }
        }

        private void Move(Vector2 direction, float speed)
        {
            var targetPosition = _playerRidigbody.position + direction * (speed * Time.fixedDeltaTime);
            _playerRidigbody.MovePosition(targetPosition);
        }

        public bool IsBusy()
        {
            return _playerMeleeAttack.isAttacking | _playerShootProjectile.isShooting;
        }

    }
}