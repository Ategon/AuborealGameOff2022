using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class State
{
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, MELEEATTACK, RANGEDATTACK
    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };
    public STATE name;
    protected EVENT stage;
    protected State nextState;

    protected StateValues stateValues;

    public State (StateValues _stateValues)
    {
        stateValues = _stateValues;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }
    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = stateValues.player.position - stateValues.enemy.transform.position;
        if (direction.magnitude<stateValues.stateMathValues.visDist)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = stateValues.player.position - stateValues.enemy.transform.position;
        if (direction.magnitude<stateValues.stateMathValues.attackDist)
        {
            return true;
        }
        return false;
    }

    public bool startPatroling()
    {
        {
            Vector3 direction = stateValues.player.position - stateValues.enemy.transform.position;
            if (direction.magnitude < stateValues.stateMathValues.patrolDist)
            {
                return true;
            }
            return false;
        }
    }

    public float ReduceCooldown(float cooldown)
    {
        return cooldown - Time.deltaTime;
    }
}


public class Idle: State
{
    public Idle(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isIdle");
        Debug.Log("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(stateValues);
            stage = EVENT.EXIT;
        }
        else if (startPatroling())
        {    
            nextState = new Patrol(stateValues);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //stateValues.anim.ResetTrigger("isIdle");
        Debug.Log("isIdleReset");
        base.Exit();
    }
}
public class Patrol: State
{
    int currentIndex = -1;
    public Patrol (StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.PATROL;
        stateValues.agent.speed = 2;
        stateValues.agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDis = Mathf.Infinity;
        for (int i = 0; i <stateValues.stateMathValues.checkpoints.Count; i++)
        {
            GameObject thisWasypoint = stateValues.stateMathValues.checkpoints[i];
            float distance = Vector3.Distance(stateValues.enemy.transform.position, thisWasypoint.transform.position);
            if (distance<lastDis)
            {
                currentIndex = i-1;
                lastDis = distance;
            }
        }
        //stateValues.anim.SetTrigger("isWalking");
        Debug.Log("isWalking");
        base.Enter();
    }

    public override void Update()
    {
        if (stateValues.agent.remainingDistance<1)
        {
            if (currentIndex>= stateValues.stateMathValues.checkpoints.Count-1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            stateValues.agent.SetDestination(stateValues.stateMathValues.checkpoints[currentIndex].transform.position);
        }
        if (CanSeePlayer())
        {
            nextState = new Pursue(stateValues);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        //stateValues.anim.ResetTrigger("isWalking");
        Debug.Log("isWalkingReset");
        base.Exit();
    }
}

public class Pursue: State
{
    public Pursue(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.PURSUE;
        stateValues.agent.speed = 5;
        stateValues.agent.isStopped = false;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isRunning");
        Debug.Log("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        stateValues.agent.SetDestination(stateValues.player.position);
        if (stateValues.agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                if (stateValues.stateMathValues.isMeleeAttacking)
                {
                    nextState = new meleeAttack(stateValues);
                    stage = EVENT.EXIT;
                }
                if (stateValues.stateMathValues.isRangedAttacking)
                {
                    nextState = new rangedAttack(stateValues);
                    stage = EVENT.EXIT;
                }
            }
            else
            {
                if (!CanSeePlayer())
                {
                    nextState = new Idle(stateValues);
                    stage = EVENT.EXIT;
                }
            }
        }
    }

    public override void Exit()
    {
        //stateValues.anim.ResetTrigger("isRunning");
        Debug.Log("isRunningReset");
        base.Exit();
    }
}

public class Attack : State
{
    public Attack(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.ATTACK;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isShooting");
        Debug.Log("isShooting");
        stateValues.agent.isStopped=true;
        base.Enter();
    }

    public override void Update()
    {
        if (!CanAttackPlayer())
        {
            nextState = new Idle(stateValues);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        //stateValues.anim.ResetTrigger("isShooting");
        Debug.Log("isShootingReset");
        base.Exit();
    }
}

public class meleeAttack : State
{
    float attackCooldown;
    public meleeAttack(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.MELEEATTACK;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isShooting");
        Debug.Log("isMeleeAttacking");
        attackCooldown = stateValues.stateMathValues.attackCooldown;
        stateValues.agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        if (!CanAttackPlayer())
        {
            nextState = new Idle(stateValues);
            stage = EVENT.EXIT;
        }
        if (attackCooldown<=0)
        {
            Debug.Log("Damage");
            attackCooldown = stateValues.stateMathValues.attackCooldown;
        }
        else
        {
            attackCooldown = ReduceCooldown(attackCooldown);
        }
    }

    public override void Exit()
    {
        //stateValues.anim.ResetTrigger("isShooting");
        Debug.Log("isMeleeAttackingReset");
        base.Exit();
    }
}

public class rangedAttack : State
{
    float attackCooldown;
    Transform playerWhenAttack = null;
    public rangedAttack(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.RANGEDATTACK;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isShooting");
        Debug.Log("isRangedAttacking");
        attackCooldown = stateValues.stateMathValues.attackCooldown;
        stateValues.agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        if (!CanAttackPlayer())
        {
            nextState = new Idle(stateValues);
            stage = EVENT.EXIT;
        }
        if (attackCooldown <= 0)
        {
            if (playerWhenAttack!=null)
            {
                if (Vector3.Distance(playerWhenAttack.position, stateValues.player.position) < 3f)
                    Debug.Log("Damage");
            }
            playerWhenAttack = stateValues.player;
            Debug.Log("RangedAttack");

            attackCooldown = stateValues.stateMathValues.attackCooldown;
        }
        else
        {
            attackCooldown = ReduceCooldown(attackCooldown);
        }
    }

    public override void Exit()
    {
        //stateValues.anim.ResetTrigger("isShooting");
        Debug.Log("isRangedAttackingReset");
        base.Exit();
    }
}