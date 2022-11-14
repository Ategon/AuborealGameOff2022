using TMPro;
using UnityEngine;
using Assets.Player.Inventory;

public class ShopStandPopup : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private Color backgroundColor;
    public void Show(string itemName, string itemDescription, ResourceType resourceType, int cost)
    {
        spriteRenderer.color = backgroundColor;
        textMeshPro.text = itemName + "\n";
        textMeshPro.text += "Cost: " + cost + " " + resourceType.ToString() + "\n";
        textMeshPro.text += "\n";
        textMeshPro.text += itemDescription;
    }
    public void Hide()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
        textMeshPro.text = "";
    }
}
