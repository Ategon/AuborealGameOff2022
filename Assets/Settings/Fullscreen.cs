using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Fullscreen : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Toggle toggle;

    private void Update()
    {
        toggle.isOn = (Screen.fullScreenMode != FullScreenMode.Windowed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
