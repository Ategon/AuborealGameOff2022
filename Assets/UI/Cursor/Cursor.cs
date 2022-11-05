using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    Image image;
    private float rotationSpeed = 100;
    private float size = 0;
    public Camera mainCamera;
    [SerializeField] private GameObject child;

    public Vector2 Aim = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        image = child.GetComponent<Image>();
    }

    void Update()
    {
        child.SetActive(true);
        // UnityEngine.Cursor.visible = false;

        float screenWidthDiff = Screen.width / Screen.width;
        float screenHeightDiff = Screen.height / Screen.height;
        if (size > 0) size -= Time.deltaTime;
        else if (size < 0) size = 0;
        // //Vector3 pos = data.sharedData.MainCamera.ScreenToWorldPoint(data.PlayerState.CursorPos);
        // //transform.position = new Vector3(pos.x, 1, pos.z);
        // image.rectTransform.localPosition = new Vector3(data.Aim.x / screenWidthDiff + (player.position.x * 100), data.Aim.y / screenHeightDiff + (player.position.y * 100), 1);
        // child.transform.Rotate(0f, 0.0f, rotationSpeed * Time.deltaTime, Space.Self);

        Vector2 screenPos = mainCamera.WorldToScreenPoint(Aim);
        image.rectTransform.position = new Vector3(screenPos.x / screenWidthDiff, screenPos.y / screenHeightDiff, 1);
        child.transform.Rotate(0f, 0.0f, rotationSpeed * Time.deltaTime, Space.Self);

        if (Aim != Vector2.zero)
        {
            size = 0.5f;
        }

        child.transform.localScale = new Vector3(1 + size, 1 + size, 1);
    }
}