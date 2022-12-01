
using Assets.Audio.Events;
using Assets.Player.Inventory;
using UnityEngine;

public abstract class ShopStand : Interactable
{
    [Header("Shop References")]
    [SerializeField] protected InventoryController inventoryController;
    [SerializeField] private ShopStandPopup shopPopup;
    [SerializeField] private PlayerBuyEvent playerBuyEvent;
    [Header("Item Cost and Description")]
    public ResourceType resourceType;
    public int cost;
    public string itemName;
    public abstract void PurchaseItem();
    protected override bool Interact()
    {
        switch (resourceType)
        {
            case ResourceType.Treasure:
                if (inventoryController.ChangeTreasure(-cost)) 
                { 
                    PurchaseItem();
                    playerBuyEvent.Raise(this, null);
                    return true;
                }
                return false;
            case ResourceType.Wood:
                if (inventoryController.ChangeWood(-cost)) 
                { 
                    PurchaseItem();
                    playerBuyEvent.Raise(this, null);
                    return true;
                }
                return false;
        }
        return false;
    }
    public override void PlayerEnterRange()
    {
        base.PlayerEnterRange();
        shopPopup.Show(itemName, GetDescription(), resourceType, cost);
    }
    public override void PlayerExitRange()
    {
        base.PlayerExitRange();
        shopPopup.Hide();
    }
    protected abstract string GetDescription();
}
