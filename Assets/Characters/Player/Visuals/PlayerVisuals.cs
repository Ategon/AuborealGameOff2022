using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Player.Movement;

public class PlayerVisuals : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerMovement pm;

    private bool facingRight;

    private Coroutine flippingCoroutine;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        pm = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (pm.PlayerDirection != Vector2.zero)
        {
            if (pm.PlayerDirection.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (pm.PlayerDirection.x < 0 && facingRight)
            {
                Flip();
            }
        }
        /* Paper mario style flipping
        if (pm.PlayerDirection.x > 0 && !facingRight)
        {
            facingRight = true;
            if(flippingCoroutine != null) StopCoroutine(flippingCoroutine);
            flippingCoroutine = StartCoroutine(GoToScale(-1, 0.25f));
        } 
        else if (pm.PlayerDirection.x < 0 && facingRight) {
            facingRight = false;
            if (flippingCoroutine != null) StopCoroutine(flippingCoroutine);
            flippingCoroutine = StartCoroutine(GoToScale(1, 0.25f));
        }
        */
    }

    private void Flip()
    {
        facingRight = !facingRight;
        if (flippingCoroutine != null) StopCoroutine(flippingCoroutine);
        flippingCoroutine = StartCoroutine(GoToScale(facingRight ? -1 : 1, 0.1f));
    }

    private IEnumerator GoToScale(float targetScale, float duration)
    {
        var startScale = transform.localScale;
        var time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            var t = time / duration;
            transform.localScale = Vector3.Lerp(startScale, new Vector3(0, transform.localScale.y, transform.localScale.z) + Vector3.right * targetScale, t);
            yield return null;
        }
    }
}
