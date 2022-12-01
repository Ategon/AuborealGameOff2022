using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSilhouette : MonoBehaviour
{
    private Collider2D collider;
    private List<SpriteRenderer> otherRenderers = new List<SpriteRenderer>();
    private SpriteRenderer sr;

    [SerializeField] private Color shownColor;
    [SerializeField] private Color hiddenColor;

    private bool checking = false;
    private bool hidden = false;
    public bool stopCoroutine = false;

    Coroutine lastCoroutine;
    
    void Start()
    {
        collider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        if(lastCoroutine != null) StopCoroutine(lastCoroutine);
    }

    private void Update()
    {
        if (stopCoroutine)
        {
            StopAllCoroutines();
            return;
        }
        if (checking)
        {
            int amountInFront = 0;
            foreach (SpriteRenderer renderer in otherRenderers)
            {
                if (sr.transform.position.y > renderer.transform.position.y)
                {
                    amountInFront++;
                }
            }

            if (amountInFront == otherRenderers.Count)
            {
                if (!hidden) return;
                hidden = false;
                if (lastCoroutine != null)
                {
                    StopCoroutine(lastCoroutine);
                }
                lastCoroutine = StartCoroutine(FadeColor(sr.color, shownColor, 0.25f));
            }
            else
            {
                if (hidden) return;
                hidden = true;
                if (lastCoroutine != null)
                {
                    StopCoroutine(lastCoroutine);
                }
                lastCoroutine = StartCoroutine(FadeColor(sr.color, hiddenColor, 0.25f));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            checking = true;
            otherRenderers.Add(sr);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (stopCoroutine)
        {
            StopAllCoroutines();
            return;
        }
        SpriteRenderer sr = collision.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            otherRenderers.Remove(sr);
            if (otherRenderers.Count <= 0)
            {
                checking = false;
                if (hidden) return;
                hidden = true;
                if (lastCoroutine != null)
                {
                    StopCoroutine(lastCoroutine);
                }
                lastCoroutine = StartCoroutine(FadeColor(this.sr.color, hiddenColor, 0.25f));
            }
        }
    }

    IEnumerator FadeColor(Color startColor, Color endColor, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            sr.color = Color.Lerp(startColor, endColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sr.color = endColor;
    }
}
