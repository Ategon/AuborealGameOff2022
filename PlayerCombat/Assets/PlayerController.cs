using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D AttackObj;

    float MovementSpeed = 5f;
    Rigidbody2D rb;
    Vector2 MoveDir;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }
    void FixedUpdate()
    {
        Move();
        Vector2 lookDir = mousePos - AttackObj.position;
        float attackAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        AttackObj.rotation = attackAngle;
        AttackObj.position = rb.position;
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveDir = new Vector2(moveX, moveY).normalized;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void Move()
    {
        rb.velocity = new Vector2(MoveDir.x * MovementSpeed, MoveDir.y * MovementSpeed);
    }
}
