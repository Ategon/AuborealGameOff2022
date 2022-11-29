using Characters.Enemies;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    public Damage damage;
    public LayerMask targetLayers;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        int layer = collision.gameObject.layer;
        if ((targetLayers.value & 1 << layer) == 1 << layer)
        {
            bool queueForDestruction = false;
            if (collision.TryGetComponent(out KnockBackable knockBackable))
            {
                knockBackable.Knock(transform.position);
                queueForDestruction = true;
            }
            if (collision.TryGetComponent(out Attackable attackable))
            {
                attackable.ReceiveDamage(gameObject.layer, damage);
                queueForDestruction = true;
            }
            if (queueForDestruction)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }

    }

}