using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class IslandClickArea : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Island island;
    [SerializeField] private SpriteRenderer spriteHighlight;
    [Header("Click and Hover")]
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private Color highlightHoverColor;
    [SerializeField] private Color highlightDefaultColor;
    private bool isHovering;
    private new Camera camera;
    [HideInInspector]
    public NavigationInputActions navigationInput;
    private InputAction click;
    private InputAction point;


    private void Awake()
    {
        camera = Camera.main;
        navigationInput = new NavigationInputActions();
    }

    private void OnEnable()
    {
        click = navigationInput.Navigation.Click;
        click.Enable();
        click.performed += OnMouseClick;
        point = navigationInput.Navigation.Point;
        point.Enable();
    }
    private void OnDisable()
    {
        click.Disable();
        click.Disable();
    }
    private void Update()
    {
        bool isMouseOverIsland = IsMouseOverIsland();
        if (isMouseOverIsland & !isHovering)
        {
            isHovering = true;
            spriteHighlight.color = highlightHoverColor;
            island.RaiseIslandMouseEnter();
        }
        if (!isMouseOverIsland & isHovering)
        {
            isHovering = false;
            spriteHighlight.color = highlightDefaultColor;
            island.RaiseIslandMouseExit();
        }
    }
    private bool IsMouseOverIsland()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(point.ReadValue<Vector2>());
        float maxDist = circleCollider.radius;
        return (maxDist >= Vector2.Distance(mousePos, new Vector2(transform.position.x, transform.position.y)));
    }
    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (isHovering)
        {
            island.RaiseIslandClick();
        }
    }
}
