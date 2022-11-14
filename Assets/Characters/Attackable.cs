using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private LayerMask attackingLayers;
    [SerializeField] private Health health;

    public bool ReceiveDamage(int layer, Damage damage)
    {
        if ((attackingLayers.value & 1 << layer) == 1 << layer)
        {
            if (rigidbody != null)
            {
                rigidbody.AddForce(damage.knockbackDirection * damage.knockbackMagnitude, ForceMode2D.Impulse);
            }
            if (health != null)
            {
                health.ChangeHealth(-damage.damage);
            }
            return true;
        }
        return false;
    }
}
