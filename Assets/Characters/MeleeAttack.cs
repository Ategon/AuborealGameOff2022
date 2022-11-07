using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected Transform hitboxCenter;
    [Header("Attack Stats")]
    [SerializeField] private float attackSize;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackKnockback;
    [Header("Debug")]
    [SerializeField] private bool debugHitboxes;
    [SerializeField] private GameObject hitboxIndicatorPrefab;
     protected void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackSize);
        if (debugHitboxes)
        {
            GameObject indicator = Instantiate(hitboxIndicatorPrefab, attackPoint.position, Quaternion.identity);
            indicator.transform.localScale = new Vector3(attackSize, attackSize, attackSize);
        }
        foreach (Collider2D collider in colliders)
        {
            Attackable attackable = collider.GetComponent<Attackable>();
            if (attackable != null)
            {
                attackable.ReceiveDamage(gameObject.layer, new Damage(attackDamage, attackKnockback, (collider.transform.position - hitboxCenter.transform.position).normalized));
            }
        }
    }
}
