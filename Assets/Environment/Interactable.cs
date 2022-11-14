using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Interactable : MonoBehaviour
{
    [Header("Info Display")]
    [SerializeField] private InteractionPopup interactionPopup;
    [SerializeField] private string verb;
    [Header("Depletion")]
    [SerializeField] private bool depletesWhenUsed;
    [SerializeField] private GameObject depletedPrefab;
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
    public virtual void PlayerEnterRange()
    {
        isPlayerInRange = true;
        if (interactionPopup != null)
        {
            var bindingIndex = interact.GetBindingIndex();
            var displayString = interact.GetBindingDisplayString(bindingIndex, out string deviceLayoutName, out string controlPath);
            interactionPopup.Show(verb, displayString);
        }
    }
    public virtual void PlayerExitRange()
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
            if (Interact() & depletesWhenUsed) { Deplete(); }
        }
    }
    protected abstract bool Interact();

    private void Deplete()
    {
        if (depletedPrefab) { Instantiate(depletedPrefab, transform.position, Quaternion.identity); }
        Destroy(gameObject);
    }
}
