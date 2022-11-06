using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float fireRate = 15f;
    float nttf;
    int combo = 0;

    public bool stunned;

    public Transform attackPos;
    public float attackRangeX;
    public float attackRangeY;
    public LayerMask whatisEnemies;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time>= nttf && !stunned)
        {
            if (Time.time <= nttf + 0.25f)
            {
                if (combo >= 4)
                {
                    combo = 1;
                }
                else
                {
                    combo += 1;
                }
            }
            else
            {
                combo = 1;
            }
            print(combo);
            nttf = Time.time + 1f / fireRate;
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,new Vector2(attackRangeX,attackRangeY),0, whatisEnemies);
            for(int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));
    }
}
