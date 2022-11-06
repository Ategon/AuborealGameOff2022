using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinimapIcon : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerMovement pm;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        pm = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if(pm.PlayerDirection != Vector2.zero) transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(pm.PlayerDirection.y, pm.PlayerDirection.x) * Mathf.Rad2Deg - 90);
    }
}
