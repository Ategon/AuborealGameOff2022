using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeAttack : MeleeAttack
{
    [Header("Player Attack")]
    [SerializeField] private float attackPointOffset;
    private Inputs inputs;
    private InputAction mousePosition;
    private InputAction attack;
    private new Camera camera;
    [SerializeField] private float cooldown;
    private float timeUntilNextAttack;
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
    }
    void Update()
    {
        UpdateAttackPosition();
        if (timeUntilNextAttack > 0)
        {
            timeUntilNextAttack -= Time.deltaTime;
        }
    }
    private void UpdateAttackPosition()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
        Vector2 hitboxToMouse = new Vector2(mousePos.x - hitboxCenter.position.x, mousePos.y - hitboxCenter.position.y);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, hitboxToMouse) * Mathf.PI / 180;
        Vector3 directionFromCenter = new Vector2(Mathf.Cos(mouseAngle), Mathf.Sin(mouseAngle));
        attackPoint.position = hitboxCenter.position + directionFromCenter * attackPointOffset;
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (timeUntilNextAttack <= 0)
        {
            Attack();
        }
    }
}
