using System.Collections;
using System.Collections.Generic;
using Characters.Enemies;
using UnityEngine;
using UnityEngine.Timeline;

public class MeleeAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform hitboxCenter;
    [Header("Attack Stats")]
    [SerializeField] private float attackSize;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackKnockback;
    [SerializeField] private float attackOffset;
    [Header("Debug")]
    [SerializeField] private bool debugHitboxes;
    [SerializeField] private GameObject hitboxIndicatorPrefab;
     public void Attack(Vector3 target)
    {
        Vector3 attackPosition = FindPointFromOffset(hitboxCenter.position, target, attackOffset);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition, attackSize);
        if (debugHitboxes)
        {
            GameObject indicator = Instantiate(hitboxIndicatorPrefab, attackPosition, Quaternion.identity);
            indicator.transform.localScale = new Vector3(attackSize, attackSize, attackSize);
        }
        foreach (Collider2D collider in colliders)
        {
            Attackable attackable = collider.GetComponent<Attackable>();
            if (attackable != null)
            {
                attackable.ReceiveDamage(gameObject.layer, new Damage(attackDamage, attackKnockback, (collider.transform.position - hitboxCenter.transform.position).normalized));
            }

            if (collider.TryGetComponent(out KnockBackable knockBackable))
            {
                knockBackable.Knock(hitboxCenter.position);
            }
        }
    }

    protected Vector3 FindPointFromOffset(Vector3 hitboxCenterPosition, Vector3 target, float attackOffset)
    {
        Vector2 hitboxToMouse = new Vector2(target.x - hitboxCenterPosition.x, target.y - hitboxCenterPosition.y);
        float mouseAngle = Vector2.SignedAngle(Vector2.right, hitboxToMouse) * Mathf.PI / 180;
        Vector3 directionFromCenter = new Vector2(Mathf.Cos(mouseAngle), Mathf.Sin(mouseAngle));
        return hitboxCenterPosition + directionFromCenter * attackOffset;
    }

}