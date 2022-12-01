using TMPro;
using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private Color backgroundColor;
    public void Show(string verb, string key)
    {
        spriteRenderer.color = backgroundColor;
        textMeshPro.text = "Press " + key + " to " + verb;
    }
    public void Hide()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0);
        textMeshPro.text = "";
    }

    public void ShowEndBoat()
    {
        spriteRenderer.color = backgroundColor;
        textMeshPro.text = "Recover your sextant from the mage to sail!";
    }
}
