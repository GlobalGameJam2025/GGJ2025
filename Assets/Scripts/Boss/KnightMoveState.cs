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

        float magnitude = (_controller.player.transform.position - _controller.transform.position).magnitude;
        Debug.Log("플레ㅇ니ㅏ러: "+magnitude);
        if (_controller.onPattern3CoolTime && magnitude > _controller.pattern3.attackArea)
        {
            _controller.onPattern3 = true;
            _controller.stateMachine.TransitionTo(_controller.stateMachine.attackState);
            
        }

        if (_controller.onPattern1and2CoolTime && magnitude < _controller.pattern1.attackArea)
        {
            if (Random.value < 0f)
            {
                _controller.onPattern1 = true;
                _controller.stateMachine.TransitionTo(_controller.stateMachine.attackState);
            }
            else
            {
                _controller.onPattern2 = true;
                _controller.stateMachine.TransitionTo(_controller.stateMachine.attackState);
            }                
        }


        if (magnitude < _controller.patternNormal.attackArea)
        {
            _controller.onPatternNormal = true;
            _controller.stateMachine.TransitionTo(_controller.stateMachine.attackState);
            
        }
    }
}