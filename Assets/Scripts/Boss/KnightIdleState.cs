using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class KnightIdleState : IBossState
{
    private KnightBossController _controller;
    public KnightIdleState(KnightBossController controller)
    {
        _controller = controller;
    }

    float _elpasTime;

    public void Enter()
    {
        _controller.animator.SetBool("Idle", true);
    }

    public void Exit()
    {

    }

    public void Update()
    {
        _elpasTime += Time.deltaTime;

        if (_elpasTime > 3)
        {
           
            _elpasTime = 0;
            _controller.SetStop(false);
            _controller.stateMachine.TransitionTo(_controller.stateMachine.moveState);

        }
    }
}