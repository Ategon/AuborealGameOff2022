using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    private Animator anim;
    private bool flewAway = false;
    private float returnTime = 5f;

    private GameObject shadow;
    private Transform shadowTransform;
    private SpriteRenderer shadowRenderer;

    private Vector3 startScale = new Vector3(0.22f, 0.16f, 0.5f);

    private int collidersInRange = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        shadow = transform.GetChild(0).gameObject;
        shadowTransform = shadow.transform;
        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            collidersInRange++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            collidersInRange--;
        }
    }

    private void FixedUpdate()
    {
        if (!flewAway)
        {
            if (collidersInRange > 0)
            {
                FlyAway();
                flewAway = true;
            }
            if (Random.Range(0, 100) < 1)
            {
                anim.Play("PeckBird");
            }
        }
    }


    private void FlyAway()
    {
        anim.Play("FlyBird");
        Invoke("Return", returnTime);
        StartCoroutine(FlyMovement());
    }

    IEnumerator FlyMovement()
    {
        // go backwards for half a second, then wait in place, then fly away. use dotween
        transform.DOLocalMove(new Vector3(transform.position.x + (0.2f * transform.localScale.x), transform.position.y + 0.2f, 0), 0.5f);
        shadowTransform.DOLocalMove(new Vector3(shadowTransform.localPosition.x, shadowTransform.localPosition.y - 0.2f, 0), 0.5f);
        shadowTransform.DOScale(new Vector3(1.2f * startScale.x, 1.2f * startScale.y, 1.2f * startScale.z), 0.5f);
        shadowRenderer.DOColor(new Color(0, 0, 0, 0.15f), 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOLocalMove(new Vector3(transform.position.x, transform.position.y + 0.05f, 0), 0.1f);
        shadowTransform.DOLocalMove(new Vector3(shadowTransform.localPosition.x, shadowTransform.localPosition.y - 0.05f, 0), 0.1f);
        shadowTransform.DOScale(new Vector3(1.25f * startScale.x, 1.25f * startScale.y, 1.25f * startScale.z), 0.1f);
        shadowRenderer.DOColor(new Color(0, 0, 0, 0.14f), 0.1f);
        yield return new WaitForSeconds(0.1f);

        transform.DOLocalMove(new Vector3(transform.position.x - (1f * transform.localScale.x), transform.position.y + 3f, 0), 1.0f);
        shadowTransform.DOLocalMove(new Vector3(shadowTransform.localPosition.x, shadowTransform.localPosition.y - 3f, 0), 1.0f);
        shadowTransform.DOScale(new Vector3(2f * startScale.x, 2f * startScale.y, 2f * startScale.z), 1.0f);
        shadowRenderer.DOColor(new Color(0, 0, 0, 0f), 1.0f);
    }

    private void Return()
    {
        StartCoroutine(ReturnMovement());
    }

    IEnumerator ReturnMovement()
    {
        transform.DOLocalMove(new Vector3(transform.position.x + (0.8f * transform.localScale.x), transform.position.y - 3.15f, 0), 1.0f);
        shadowTransform.DOLocalMove(new Vector3(shadowTransform.localPosition.x, shadowTransform.localPosition.y + 3.15f, 0), 1.0f);
        shadowTransform.DOScale(new Vector3(1.1f * startScale.x, 1.1f * startScale.y, 1.1f * startScale.z), 1.0f);
        shadowRenderer.DOColor(new Color(0, 0, 0, 0.17f), 1.0f);
        yield return new WaitForSeconds(1.0f);
        transform.DOLocalMove(new Vector3(transform.position.x, transform.position.y - 0.1f, 0), 0.5f);
        shadowTransform.DOLocalMove(new Vector3(shadowTransform.localPosition.x, shadowTransform.localPosition.y + 0.1f, 0), 0.5f);
        shadowTransform.DOScale(new Vector3(1f * startScale.x, 1f * startScale.y, 1f * startScale.z), 0.5f);
        shadowRenderer.DOColor(new Color(0, 0, 0, 0.2f), 0.5f);
        yield return new WaitForSeconds(0.5f);
        anim.Play("IdleBird");
        flewAway = false;
    }
}
