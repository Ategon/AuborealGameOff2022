using System.Collections;
using System.Collections.Generic;
using Characters.Enemies;
using UnityEngine;

public class BombIndicator : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [HideInInspector] public float explosionDelay;
    [HideInInspector] public float bombBlastRadius;
    [HideInInspector] public int bombDamage;
    private float timeSinceSpawn;

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn > explosionDelay)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombBlastRadius);
            foreach (Collider2D collider in colliders)
            {
                Attackable attackable = collider.GetComponent<Attackable>();
                if (attackable != null)
                {
                    attackable.ReceiveDamage(gameObject.layer, new Damage(bombDamage, 0, Vector2.zero));
                }
            }
            Destroy(gameObject);
        }
    }
}
