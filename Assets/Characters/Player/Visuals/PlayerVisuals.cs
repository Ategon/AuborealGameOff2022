using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Player.Movement;
using DG.Tweening;

public class PlayerVisuals : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerMovement pm;
    private Animator anim;
    private Camera cam;

    private bool facingRight;

    private Coroutine flippingCoroutine;

    [SerializeField] private Transform[] additionalFlippees;
    [SerializeField] private ParticleSystem[] particleFlipees;
    [SerializeField] private bool overrideFlip;

    private float scaleXdata = 1;
    private float scaleYdata = 1;
    private Tween lastTween = null;
    private Tween lastSecondaryTween = null;

    public void TriggerDash()
    {
        if (lastTween != null) lastTween.Kill();
        if (lastSecondaryTween != null) lastSecondaryTween.Kill();

        scaleXdata = 1;
        scaleYdata = 1;

        lastTween = DOTween.To(() => scaleYdata, x => scaleYdata = x, 0.8f, 0.1f).SetEase(Ease.OutQuad);
        lastSecondaryTween = DOTween.To(() => scaleXdata, x => scaleXdata = x, 1.2f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            lastTween = DOTween.To(() => scaleYdata, x => scaleYdata = x, 0.9f, 0.1f).SetEase(Ease.OutQuad);
            lastSecondaryTween = DOTween.To(() => scaleXdata, x => scaleXdata = x, 1.1f, 0.1f).SetEase(Ease.OutQuad);
        });
    }

    public void TriggerDashEnd()
    {
        if (lastTween != null) lastTween.Kill();
        if (lastSecondaryTween != null) lastSecondaryTween.Kill();

        scaleYdata = 0.90f;
        scaleXdata = 1.1f;

        lastTween = DOTween.To(() => scaleYdata, x => scaleYdata = x, 1.05f, 0.1f).SetEase(Ease.OutQuad);
        lastSecondaryTween = DOTween.To(() => scaleXdata, x => scaleXdata = x, 0.95f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            lastTween = DOTween.To(() => scaleYdata, x => scaleYdata = x, 1, 0.1f).SetEase(Ease.OutQuad);
            lastSecondaryTween = DOTween.To(() => scaleXdata, x => scaleXdata = x, 1, 0.1f).SetEase(Ease.OutQuad);
        });
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        pm = GetComponentInParent<PlayerMovement>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (scaleXdata != 1 || scaleYdata != 1)
        {
            transform.localScale = new Vector3(scaleXdata * Mathf.Sign(transform.localScale.x), scaleYdata, 1);
        }

        if (pm.IsBusy())
        {
            if (cam.ScreenToWorldPoint(pm.Aim).x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (cam.ScreenToWorldPoint(pm.Aim).x < transform.position.x && facingRight)
            {
                Flip();
            }
            return;
        }
        
        if (pm.PlayerDirection != Vector2.zero)
        {
            anim.Play("Walk");
            if (pm.PlayerDirection.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (pm.PlayerDirection.x < 0 && facingRight)
            {
                Flip();
            }

            foreach (ParticleSystem ps in particleFlipees)
            {
                if (!ps) continue; 
                ParticleSystem.MainModule main = ps.main;
                main.startRotation = new ParticleSystem.MinMaxCurve(Vector2.SignedAngle(pm.PlayerDirection.normalized, Vector2.right) * Mathf.Deg2Rad * -1);
            }
        }
        else {
            anim.Play("Idle");
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

    private float Angle(Vector2 vector2)
    {
        return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * Mathf.Sign(vector2.x));
    }

    private void Flip()
    {
        if (overrideFlip) return;
        facingRight = !facingRight;
        if (flippingCoroutine != null) StopCoroutine(flippingCoroutine);
        flippingCoroutine = StartCoroutine(GoToScale(facingRight ? -1 : 1, 0.1f));

        foreach (Transform t in additionalFlippees)
        {
            t.localScale = new Vector3(t.localScale.x * -1, t.localScale.y, t.localScale.z);
        }
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
