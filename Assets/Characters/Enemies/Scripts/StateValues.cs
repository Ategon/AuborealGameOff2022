using System.Collections;
using System.Collections.Generic;
using Assets.Enemies;
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
    public NumAggroedEnemyChangeEvent numAggroedEnemyChangeEvent;
    public StateMathematicalValues stateMathValues;
    public StateValues(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyMeleeAttack _meleeAttack, ShootProjectile _rangedAttack,
        NumAggroedEnemyChangeEvent _numAggroedEnemyChangeEvent, StateMathematicalValues _stateMathValues)
    {
        enemy = _enemy;
        agent = _agent;
        anim = _anim;
        player = _player;
        meleeAttack = _meleeAttack;
        rangedAttack = _rangedAttack;
        numAggroedEnemyChangeEvent = _numAggroedEnemyChangeEvent;
        stateMathValues = _stateMathValues;
    }
}
