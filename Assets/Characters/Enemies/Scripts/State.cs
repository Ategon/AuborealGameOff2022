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
            if (stateValues.stateMathValues.isMeleeAttacking)
                return true;
            if (stateValues.stateMathValues.isRangedAttacking &
                !Physics2D.Linecast(stateValues.agent.transform.position, stateValues.player.transform.position, stateValues.stateMathValues.projectileCollisionLayers))
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
        stateValues.agent.speed = stateValues.stateMathValues.patrolSpeed;
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
        base.Exit();
    }
}

public class Pursue: State
{
    public Pursue(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.PURSUE;
        stateValues.agent.speed = stateValues.stateMathValues.pursueSpeed;
        stateValues.agent.isStopped = false;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isRunning");
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
        attackCooldown = stateValues.stateMathValues.attackCooldown;
        stateValues.agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        if (!CanAttackPlayer() & !stateValues.meleeAttack.isAttacking)
        {
            nextState = new Idle(stateValues);
            stage = EVENT.EXIT;
        }
        if (attackCooldown<=0)
        {
            stateValues.meleeAttack.BeginAttacking(stateValues.player.position);
            attackCooldown = stateValues.stateMathValues.attackCooldown;
        }
        else
        {
            attackCooldown = ReduceCooldown(attackCooldown);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class rangedAttack : State
{
    float attackCooldown;
    public rangedAttack(StateValues _stateValues)
        : base(_stateValues)
    {
        name = STATE.RANGEDATTACK;
    }

    public override void Enter()
    {
        //stateValues.anim.SetTrigger("isShooting");
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
            stateValues.rangedAttack.Fire(stateValues.agent.transform.position, stateValues.player.position);

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
        base.Exit();
    }
}