using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Assets.Audio.Events;
using Assets.Player.Upgrades;
using Assets.Player.Inventory;
using Characters.Player.Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using Assets.EventSystem;

public class PlayerShootProjectile : ShootProjectile
{
    [Header("References")]
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerShootEvent playerShootEvent;
    [SerializeField] private RangedUpgradeEvent rangedUpgradeEvent;
    [SerializeField] private UpgradeController upgradeController;
    private Inputs inputs;
    private InputAction shootProjectile;
    private InputAction mousePosition;
    private new Camera camera;
    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timeUntilNextFire;
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [HideInInspector]
    public bool isShooting;
    private Vector2 fireDirection;
    private void Awake()
    {
        inputs = new Inputs();
        camera = Camera.main;
        UpdateDamage();
    }
    private void Update()
    {
        if (timeUntilNextFire > 0)
            timeUntilNextFire -= Time.deltaTime;
    }
    private void OnEnable()
    {
        shootProjectile = inputs.Player.ShootProjectile;
        mousePosition = inputs.Player.MousePosition;
        shootProjectile.Enable();
        mousePosition.Enable();
        shootProjectile.performed += OnPressShoot;
        rangedUpgradeEvent.AddListener(OnRangedUpgrade);

    }
    private void OnDisable()
    {
        shootProjectile.Disable();
        mousePosition.Disable();
        rangedUpgradeEvent.RemoveListener(OnRangedUpgrade);
    }
    private void OnPressShoot(InputAction.CallbackContext context)
    {
        if (timeUntilNextFire <= 0 & !playerMovement.IsBusy())
        {
            if (inventoryController.ChangeAmmo(-1))
            {
                isShooting = true;
                animator.SetTrigger("Shoot");
                Vector3 mousePos = camera.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
                fireDirection = (new Vector2(mousePos.x - projectileSpawnPoint.position.x, mousePos.y - projectileSpawnPoint.position.y)).normalized;
                timeUntilNextFire = cooldown;
                playerShootEvent.Raise(this, null);
            }
        }
    }

    public void ShootProjectile()
    {
        Fire(projectileSpawnPoint.position, new Vector2(projectileSpawnPoint.position.x + fireDirection.x, projectileSpawnPoint.position.y + fireDirection.y));
    }


    public void FinishShootAnimation()
    {
        isShooting = false;
    }

    private void OnRangedUpgrade(object sender, EventParameters args)
    {
        UpdateDamage();
    }

    private void UpdateDamage()
    {
        projectileDamage = upgradeController.rangedDamage;
    }
}
