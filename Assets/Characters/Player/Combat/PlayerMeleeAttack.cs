using Assets.Audio.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeAttack : MeleeAttack
{
    [Header("Player Attack")]
    private Inputs inputs;
    private InputAction mousePosition;
    private InputAction attack;
    private new Camera camera;
    [SerializeField] private float cooldown;
    private float timeUntilNextAttack;
    [SerializeField] private PlayerMeleeAttackEvent playerMeleeAttackEvent;
    private void Awake()
    {
        inputs = new Inputs();
        camera = Camera.main;
    }
    private void OnEnable()
    {
        mousePosition = inputs.Player.MousePosition;
        mousePosition.Enable();
        attack = inputs.Player.Attack;
        attack.Enable();
        attack.performed += OnAttack;
    }
    private void OnDisable()
    {
        mousePosition.Disable();
        attack.Disable();
    }
    void Update()
    {
        if (timeUntilNextAttack > 0)
        {
            timeUntilNextAttack -= Time.deltaTime;
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (timeUntilNextAttack <= 0)
        {
            playerMeleeAttackEvent.Raise(this, null);
            timeUntilNextAttack = cooldown;
            Vector3 mousePos = camera.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
            Attack(mousePos);
        }
    }
}
