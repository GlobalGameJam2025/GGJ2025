using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBossStateMachine
{
    public IBossState currentState { get; private set; }
    private CowBossController _controller;
    CowBossStateMachine(CowBossController controller)
    {
        _controller = controller;
    }

    void TransitionTo(IBossState nextState)
    {
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
    }
}
