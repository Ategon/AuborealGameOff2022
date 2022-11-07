using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D AttackObj;

    public bool blocking = false;

    float dazedTime;
    public float startDazedTime;

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
        if (dazedTime <= 0)
        {
            Move();
            Vector2 lookDir = mousePos - AttackObj.position;
            float attackAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            AttackObj.rotation = attackAngle;
            AttackObj.position = rb.position;
            AttackObj.gameObject.GetComponent<PlayerAttack>().stunned = false;
        }
        else
        {
            AttackObj.gameObject.GetComponent<PlayerAttack>().stunned = true;
            dazedTime -= Time.deltaTime;
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        MoveDir = new Vector2(moveX, moveY).normalized;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Parried");
            Collider2D[] ParryHitbox = Physics2D.OverlapCircleAll(AttackObj.position, 1, 0, AttackObj.GetComponent<PlayerAttack>().whatisEnemies);
            for (int i = 0; i < ParryHitbox.Length; i++)
            {
                ParryHitbox[i].GetComponent<EnemyController>().GetParried();
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            blocking = true;
            MovementSpeed = 2.5f;
        }
        else
        {
            blocking = false;
            MovementSpeed = 5f;
        }
    }
    void Move()
    {
        rb.velocity = new Vector2(MoveDir.x * MovementSpeed, MoveDir.y * MovementSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackObj.position, 1);
    }
}
