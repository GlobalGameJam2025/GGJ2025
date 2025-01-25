using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMoveState : IBossState
{
    private KnightBossController _controller;
    public KnightMoveState(KnightBossController controller)
    {
        _controller = controller;
    }
    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}