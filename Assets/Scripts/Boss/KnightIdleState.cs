using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("isIdle");
        _controller.animator.SetBool("Idle", true);
    }

    public void Exit()
    {

    }

    public void Update()
    {
        _elpasTime += Time.deltaTime;
        Debug.Log(_elpasTime);

        if (_elpasTime > 3)
        {
           
            _elpasTime = 0;
            _controller.stateMachine.TransitionTo(_controller.stateMachine.moveState);

        }
    }
}