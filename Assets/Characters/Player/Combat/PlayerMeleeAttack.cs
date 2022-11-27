using Assets.Audio.Events;
using Characters.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeAttack : MeleeAttack
{
    [Header("Animator")]
    [SerializeField] private Animator animator;
    private Inputs inputs;
    private InputAction mousePosition;
    private InputAction attack;
    private new Camera camera;
    [Header("Player Attack")]
    [SerializeField] private float cooldown;
    private float timeUntilNextAttack;
    [Header("References")]
    [SerializeField] private PlayerMeleeSwingEvent playerMeleeSwingEvent;
    [SerializeField] private PlayerMeleeHitEvent playerMeleeHitEvent;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Cursor cursor;
    [SerializeField] private ParticleSystem swordParticles;
    [Header("Attack Momentum")]
    public float attackVelocity;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public Vector2 attackDirection;


    private int attackType = 0;
    
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
        attack.performed += OnAttackButtonPressed;
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

    private void OnAttackButtonPressed(InputAction.CallbackContext context)
    {
        if (timeUntilNextAttack <= 0 & !playerMovement.IsBusy())
        {
            animator.SetInteger("AttackType", attackType);

            attackType = 0;
            // for animation swapping change to this. currently second anim sucks
            //attackType = attackType == 0 ? 1 : 0;

            swordParticles.Play();
            animator.SetTrigger("Attack");
            timeUntilNextAttack = cooldown;
            isAttacking = true;
            Vector3 mousePos = camera.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
            attackDirection = (new Vector2(mousePos.x - hitboxCenter.position.x, mousePos.y - hitboxCenter.position.y)).normalized;

            if(cursor) cursor.BonusSize = -0.35f;
        }
    }

    public void DealAttackDamage()
    {
        if (Attack(hitboxCenter.position + new Vector3(attackDirection.x, attackDirection.y, 0)))
        {
            playerMeleeHitEvent.Raise(this, null);
        }
        else
        {
            playerMeleeSwingEvent.Raise(this, null);
        }
        
    }

    public void FinishAttackAnimation()
    {
        isAttacking = false;
    }

}
