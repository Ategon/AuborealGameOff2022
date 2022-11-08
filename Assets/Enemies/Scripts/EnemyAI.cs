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
    private StateMathematicalValues stateMathValues;
    public List<GameObject> checkpoints;

    public float visDist;
    public float attackDist;
    public float patrolDist;
    public float attackCooldown;
    public bool isMeleeAttacking;
    public bool isRangedAttacking;

    public string checkpointsTag;


    State currentState;
    // Start is called before the first frame update
    void Start()
    {
        checkpoints.AddRange(GameObject.FindGameObjectsWithTag(checkpointsTag));
        checkpoints = checkpoints.OrderBy(waypoint => waypoint.name).ToList();

        stateMathValues = new StateMathematicalValues(visDist, attackDist, patrolDist, attackCooldown, checkpoints, isMeleeAttacking, isRangedAttacking);
        agent = this.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        anim = this.GetComponent<Animator>();
        StateValues stateValues = new StateValues(this.gameObject, agent, anim, player, stateMathValues);
        currentState = new Idle(stateValues);
    }
    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}