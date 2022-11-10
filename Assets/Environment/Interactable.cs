using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private InteractionPopup interactionPopup;
    [SerializeField] private string verb;
    private bool isPlayerInRange;
    private Inputs inputs;
    private InputAction interact;

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
            var bindingIndex = interact.GetBindingIndex();
            var displayString = interact.GetBindingDisplayString(bindingIndex, out string deviceLayoutName, out string controlPath);
            interactionPopup.Show(verb, displayString);
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