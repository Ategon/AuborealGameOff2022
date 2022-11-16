using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Assets.Player.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootProjectile : ShootProjectile
{
    [Header("References")]
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private InventoryController inventoryController;
    private Inputs inputs;
    private InputAction shootProjectile;
    private InputAction mousePosition;
    private new Camera camera;
    [SerializeField] private float cooldown;
    private float timeUntilNextFire;
    private void Awake()
    {
        inputs = new Inputs();
        camera = Camera.main;
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

    }
    private void OnDisable()
    {
        shootProjectile.Disable();
        mousePosition.Disable();
    }
    private void OnPressShoot(InputAction.CallbackContext context)
    {
        if (timeUntilNextFire <= 0 & inventoryController.ChangeAmmo(-1))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
            Fire(projectileSpawnPoint.position, mousePos);
            timeUntilNextFire = cooldown;
        }
    }
}
