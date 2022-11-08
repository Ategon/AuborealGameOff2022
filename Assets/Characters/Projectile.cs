using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Damage damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
