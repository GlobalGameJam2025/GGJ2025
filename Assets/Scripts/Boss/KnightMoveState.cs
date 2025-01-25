using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnightMoveState : IBossState
{
    private KnightBossController _controller;
    public KnightMoveState(KnightBossController controller)
    {
        _controller = controller;
    }

    float _elpasTime;

    public void Enter()
    {
        _controller.animator.SetBool("Idle", false);
    }

    public void Exit()
    {

    }

    public void Update()
    {
        _controller.GoTarget();
        _elpasTime += Time.deltaTime;

        //if (_controller.GetDistance() < 10)
        //{
        //    _elpasTime = 0;
        //    _controller.stateMachine.TransitionTo(_controller.stateMachine.attackState);
        //}

        if(_elpasTime > 5)
        {
            _elpasTime = 0;
            _controller.SetStop(true);
            _controller.stateMachine.TransitionTo(_controller.stateMachine.idleState);
           
        }
    }
}