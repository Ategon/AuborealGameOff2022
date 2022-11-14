using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    public MeleeAttack meleeAttack;
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
        stateMathValues = new StateMathematicalValues(visDist, attackDist, patrolDist, attackCooldown, patrolSpeed, pursueSpeed, checkpoints, isMeleeAttacking, isRangedAttacking); ;
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = this.GetComponent<Animator>();
        StateValues stateValues = new StateValues(this.gameObject, agent, anim, player, meleeAttack, stateMathValues); ;
        currentState = new Idle(stateValues);
    }
    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}