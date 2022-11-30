using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovementAnimation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    public int direction = 0;
    private float lockDirectionTime;

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (lockDirectionTime > 0)
             lockDirectionTime -= Time.deltaTime;
        if (agent.velocity.magnitude > 0.01f & lockDirectionTime <= 0)
        {
            FaceDirection(agent.velocity);
        }
    }

    public void FaceDirection(Vector2 directionVector)
    {
        int newDirection = 0;
        // Set Direction: 0 is down, left is 1, up is 2, right is 3
        if (directionVector.x > directionVector.y)
        {
            if (directionVector.x > -directionVector.y)
            {
                newDirection = 3;
            }
            else
            {
                newDirection = 0;
            }
        }
        else
        {
            if (directionVector.x > -directionVector.y)
            {
                newDirection = 2;
            }
            else
            {
                newDirection = 1;
            }
        }

        if (newDirection != direction)
        {
            ChangeDirection(newDirection);
        }
    }
    
    public void LockDirection(float duration)
    {
        lockDirectionTime = duration;
    }


    private void ChangeDirection(int newDirection)
    {
        animator.SetInteger("Direction", newDirection);
        direction = newDirection;
        animator.SetTrigger("Turn");
    }

}
