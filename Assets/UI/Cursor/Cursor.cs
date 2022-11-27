using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Characters.Player.Movement;

public class Cursor : MonoBehaviour
{
    public float BonusSize { get { return bonusSize; } set { bonusSize = value; } }

    Image image;
    [SerializeField] private float rotationSpeed = 100;
    private float bonusSize = 0;
    private float size = 0;
    public Camera mainCamera;
    [SerializeField] private GameObject child;
    private PlayerMovement pm;

    Vector2 lastScreenPoint = Vector2.zero;

    public Vector2 Aim = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        image = child.GetComponent<Image>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    private void OnDisable()
    {
        UnityEngine.Cursor.visible = true;
    }

    void Update()
    {
        if (!pm.gameObject.activeInHierarchy)
        {
            UnityEngine.Cursor.visible = true;
            return;
        }

        if (pm.Aim != lastScreenPoint)
        {
            lastScreenPoint = pm.Aim;
            size = 0.25f;
        }

        Aim = mainCamera.ScreenToWorldPoint(pm.Aim);

        child.SetActive(true);
        UnityEngine.Cursor.visible = false;

        float screenWidthDiff = Screen.width / Screen.width;
        float screenHeightDiff = Screen.height / Screen.height;
        
        if (bonusSize < 0) bonusSize += Time.deltaTime;
        else if (bonusSize > 0) bonusSize = 0;

        if (size > 0) size -= Time.deltaTime / 10;
        else if (size < 0) size = 0;
        // //Vector3 pos = data.sharedData.MainCamera.ScreenToWorldPoint(data.PlayerState.CursorPos);
        // //transform.position = new Vector3(pos.x, 1, pos.z);
        // image.rectTransform.localPosition = new Vector3(data.Aim.x / screenWidthDiff + (player.position.x * 100), data.Aim.y / screenHeightDiff + (player.position.y * 100), 1);
        // child.transform.Rotate(0f, 0.0f, rotationSpeed * Time.deltaTime, Space.Self);

        Vector2 screenPos = mainCamera.WorldToScreenPoint(Aim);
        image.rectTransform.position = new Vector3(screenPos.x / screenWidthDiff, screenPos.y / screenHeightDiff, 1);
        child.transform.Rotate(0f, 0.0f, rotationSpeed * Time.deltaTime, Space.Self);

        child.transform.localScale = new Vector3(0.75f + bonusSize + size, 0.75f + bonusSize + size, 1);
    }

}