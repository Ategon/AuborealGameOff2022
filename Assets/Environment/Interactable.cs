using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private InteractionPopup interactionPopup;
    [SerializeField] private string verb;
    private bool isPlayerInRange;
    private Inputs inputs;
    private InputAction interact;
    private static string key = "Z";

    private void Awake()
    {
        inputs = new Inputs();
    }

    private void OnEnable()
    {
        interact = inputs.Player.Interact;
        interact.Enable();
        interact.performed += OnPressInteract;
    }
    private void OnDisable()
    {
        interact.Disable();
    }
    public void PlayerEnterRange()
    {
        isPlayerInRange = true;
        if (interactionPopup != null)
        {
            interactionPopup.Show(verb, key);
        }
    }
    public void PlayerExitRange()
    {
        isPlayerInRange = false;
        if (interactionPopup != null)
        {
            interactionPopup.Hide();
        }
    }
    private void OnPressInteract(InputAction.CallbackContext context)
    {
        if (isPlayerInRange)
        {
            Interact();
        }
    }
    protected abstract void Interact();
}
