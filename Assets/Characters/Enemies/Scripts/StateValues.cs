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
    public MeleeAttack meleeAttack;
    public StateMathematicalValues stateMathValues;
    public StateValues(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, MeleeAttack _meleeAttack, StateMathematicalValues _stateMathValues)
    {
        enemy = _enemy;
        agent = _agent;
        anim = _anim;
        player = _player;
        meleeAttack = _meleeAttack;
        stateMathValues = _stateMathValues;
    }
}