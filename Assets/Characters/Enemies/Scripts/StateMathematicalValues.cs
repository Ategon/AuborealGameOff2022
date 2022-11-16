using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMathematicalValues
{
    public float visDist;
    public float attackDist;
    public int projectileCollisionLayers;
    public float patrolDist;
    public float attackCooldown;
    public float patrolSpeed;
    public float pursueSpeed;
    public List<GameObject> checkpoints;
    public bool isMeleeAttacking;
    public bool isRangedAttacking;

    public StateMathematicalValues(float _visDist, float _attackDist, int _projectileCollisionLayers, float _patrolDist, float _attackCooldown, float _patrolSpeed, float _pursueSpeed, List<GameObject> _checkpoints, bool _isMeleeAttacking, bool _isRangedAttacking)
    {
        visDist = _visDist;
        attackDist = _attackDist;
        projectileCollisionLayers = _projectileCollisionLayers;
        patrolDist = _patrolDist;
        attackCooldown = _attackCooldown;
        patrolSpeed = _patrolSpeed;
        pursueSpeed = _pursueSpeed;
        checkpoints = _checkpoints;
        isMeleeAttacking = _isMeleeAttacking;
        isRangedAttacking = _isRangedAttacking;
    }
}
