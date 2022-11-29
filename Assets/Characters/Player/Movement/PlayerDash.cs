using System;
using Assets.Audio.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerDash : MonoBehaviour
    {
        [field: SerializeField] public DashData DashData { get; private set; }
        [SerializeField] private PlayerDashEvent _playerDashEvent;

        private float _dashActiveTime;
        private float _dashCooldownTime;
        private Inputs _inputs;
        private Rigidbody2D _rigidbody;
        private Vector2 _dashDirection;

        [SerializeField] GameObject dashParticles;
        [SerializeField] GameObject dashIndicator;

        public bool CanDash { get; private set; } = false;

        private void Awake()
        {
            _inputs = new Inputs();
            TryGetComponent(out _rigidbody);
            _dashActiveTime = DashData.ActiveTime;
            _dashCooldownTime = DashData.CooldownTime;
        }

        private void OnEnable()
        {
            _inputs.Enable();
            _inputs.Player.Dash.performed += OnDash;
        }

        private void OnDisable()
        {
            _inputs.Disable();
            _inputs.Player.Dash.performed -= OnDash;
        }

        private void OnDash(InputAction.CallbackContext dashCxt)
        {
            if (_dashCooldownTime > 0) return;

            _dashActiveTime = DashData.ActiveTime;
            _dashCooldownTime = DashData.CooldownTime;
            _dashDirection = _inputs.Player.Move.ReadValue<Vector2>();
            _playerDashEvent.Raise(this, null);
            dashParticles.SetActive(true);

            CanDash = true;
        }

        private void Update()
        {
            if (_dashActiveTime > 0)
            {
                _dashActiveTime -= Time.deltaTime;
            }
            else
            {
                CanDash = false;
                dashParticles.SetActive(false);
                if (_dashCooldownTime > 0)
                {
                    _dashCooldownTime -= Time.deltaTime;
                    dashIndicator.transform.localScale = new Vector3(_dashCooldownTime / DashData.CooldownTime * 0.23f, 0.03f, 0);
                }
                else
                {
                    dashIndicator.transform.localScale = new Vector3(0, 0, 0);
                }
            }
        }

        public void Dash()
        {
            _rigidbody.MovePosition(_rigidbody.position + _dashDirection * (DashData.Force * Time.fixedDeltaTime));
        }
    }

    [Serializable]
    public struct DashData
    {
        public float Force;
        public float ActiveTime;
        public float CooldownTime;
    }
}