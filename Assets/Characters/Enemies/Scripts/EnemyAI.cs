using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public MeleeAttack meleeAttack;
    public ShootProjectile rangedAttack;
    private StateMathematicalValues stateMathValues;
    public List<GameObject> checkpoints;

    public float visDist;
    public float attackDist;
    public float patrolDist;
    public float attackCooldown;
    public float patrolSpeed;
    public float pursueSpeed;
    public bool isMeleeAttacking;
    public bool isRangedAttacking;


    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        int projectileCollisionLayers = 0;
        if (rangedAttack != null)
        {
            Projectile projectile = rangedAttack.projectilePrefab.GetComponent<Projectile>();
            int projectileLayer = projectile.gameObject.layer;
            LayerMask targetLayers = projectile.targetLayers;
            for (int i = 0; i < 32; i++)
            {
                if (!Physics2D.GetIgnoreLayerCollision(projectileLayer, i))
                {
                    if (!((targetLayers.value & 1 << i) == 1 << i))
                    {
                        projectileCollisionLayers |= 1 << i;

                    }
                }
            }
        }
        Debug.Log(projectileCollisionLayers);
        stateMathValues = new StateMathematicalValues(visDist, attackDist, projectileCollisionLayers, patrolDist, attackCooldown, patrolSpeed,
            pursueSpeed, checkpoints, isMeleeAttacking, isRangedAttacking);
        ;
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = this.GetComponent<Animator>();
        StateValues stateValues = new StateValues(this.gameObject, agent, anim, player, meleeAttack, rangedAttack, stateMathValues);
        currentState = new Idle(stateValues);


    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }

    private void OnDrawGizmos()
    {
        // vision debug visual
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visDist);
    }
}