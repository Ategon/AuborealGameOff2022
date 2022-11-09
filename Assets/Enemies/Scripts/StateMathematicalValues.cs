using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMathematicalValues
{
    public float visDist;
    public float attackDist;
    public float patrolDist;
    public float attackCooldown;
    public List<GameObject> checkpoints;
    public bool isMeleeAttacking;
    public bool isRangedAttacking;

    public StateMathematicalValues(float _visDist, float _attackDist, float _patrolDist, float _attackCooldown, List<GameObject> _checkpoints, bool _isMeleeAttacking, bool _isRangedAttacking)
    {
        visDist = _visDist;
        attackDist = _attackDist;
        patrolDist = _patrolDist;
        attackCooldown = _attackCooldown;
        checkpoints = _checkpoints;
        isMeleeAttacking = _isMeleeAttacking;
        isRangedAttacking = _isRangedAttacking;
    }
}
