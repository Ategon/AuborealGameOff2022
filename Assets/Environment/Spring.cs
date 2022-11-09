using Assets.Player.Thirst;
using UnityEngine;

public class Spring : Interactable
{
    [Header("Spring")]
    [SerializeField] private ThirstController thirstController;
    [SerializeField] private int thirstValue;
    protected override void Interact()
    {
        thirstController.ChangeThirst(thirstValue);
    }
}
