using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateValues
{
    public GameObject enemy;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;
    public EnemyMeleeAttack meleeAttack;
    public ShootProjectile rangedAttack;
    public StateMathematicalValues stateMathValues;
    public StateValues(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyMeleeAttack _meleeAttack, ShootProjectile _rangedAttack, StateMathematicalValues _stateMathValues)
    {
        enemy = _enemy;
        agent = _agent;
        anim = _anim;
        player = _player;
        meleeAttack = _meleeAttack;
        rangedAttack = _rangedAttack;
        stateMathValues = _stateMathValues;
    }
}
