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
    public StateMathematicalValues stateMathValues;
    public StateValues(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, StateMathematicalValues _stateMathValues)
    {
        enemy = _enemy;
        agent = _agent;
        anim = _anim;
        player = _player;
        stateMathValues = _stateMathValues;
    }
}
