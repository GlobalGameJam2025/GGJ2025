using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class KnightBossStateMachine
{
    public IBossState currentState { get; private set; }

    public KnightAttackState attackState;
    public KnightIdleState idleState;
    public KnightMoveState moveState;

    private KnightBossController _controller;
    public KnightBossStateMachine(KnightBossController controller)
    {
        _controller = controller;
        this.moveState = new KnightMoveState(controller);
        this.attackState = new KnightAttackState(controller);
        this.idleState = new KnightIdleState(controller);
    }
    public void Initialize(IBossState startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }
    public void TransitionTo(IBossState nextState)
    {
        Debug.LogError("nextState: "+ nextState.ToString());
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
