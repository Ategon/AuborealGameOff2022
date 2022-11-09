using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private float moveSpeed = 3f;

    private float switchTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void GetRandomDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void Update()
    {
        switchTimer -= Time.deltaTime;

        if (switchTimer <= 0)
        {
            switchTimer = Random.Range(0.5f, 2f);
            GetRandomDirection();
        }

        rb.MovePosition(transform.position + (Vector3)moveDirection * moveSpeed * Time.deltaTime);
    }
}
