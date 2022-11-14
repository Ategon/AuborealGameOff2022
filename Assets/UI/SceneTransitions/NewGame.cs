using Assets.Navigation;
using Assets.Player.Health;
using Assets.Player.Inventory;
using Assets.Player.Thirst;
using UnityEditor;
using UnityEngine;

public class NewGame : SceneChanger
{
    [SerializeField] private ThirstController thirstController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerLocationController playerLocationController;

    public override void ChangeScene()
    {
        thirstController.NewGame();
        healthController.NewGame();
        inventoryController.NewGame();
        playerLocationController.NewGame();
        base.ChangeScene();
    }
}
