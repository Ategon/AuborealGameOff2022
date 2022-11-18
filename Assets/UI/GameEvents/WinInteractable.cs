
using UnityEngine;

public class WinInteractable : Interactable
{
    [SerializeField] private GameManager gameManager;
    protected override bool Interact()
    {
        gameManager.Win();
        return true;
    }
}
