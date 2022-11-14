using Characters.Enemies;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Damage damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out KnockBackable knockBackable))
        {
            knockBackable.Knock(transform.position);
        }

        Attackable attackable = collision.GetComponent<Attackable>();
        if (attackable != null)
        {
            if (attackable.ReceiveDamage(gameObject.layer, damage))
            {
                Destroy(gameObject);
            }
        }

    }

}